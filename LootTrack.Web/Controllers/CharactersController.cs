using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using System.Web.Mvc;
using LootTrack.Domain.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using WowDotNetAPI;
using WowDotNetAPI.Models;
using LootTrack.Web.Foundation.Extensions;
using Character = WowDotNetAPI.Models.Character;
using Item = LootTrack.Domain.Models.Item;

namespace LootTrack.Web.Controllers
{
	public class CharactersController : Controller
	{
        private readonly IRepository<Domain.Models.Character> _characterRepository;
	    private readonly IRepository<Item> _itemRepository;
	    private readonly IRepository<Loot> _lootRepository;
	    private readonly IUnitOfWorkAsync _unitOfWork;

	    public CharactersController(IRepository<Domain.Models.Character> characterRepository, IRepository<Item> itemRepository, IRepository<Loot> lootRepository, IUnitOfWorkAsync unitOfWork)
	    {
	        _characterRepository = characterRepository;
	        _itemRepository = itemRepository;
	        _lootRepository = lootRepository;
	        _unitOfWork = unitOfWork;
	    }

	    public ActionResult Index()
	    {
	        var characters = _characterRepository.Query(x => x.IsActive);
	        return View(characters);
		}

	    public ActionResult Details(int id)
	    {
	        var character = _characterRepository.Query(x => x.Id == id).Include(x => x.Class).Select().First();

	        return View(character);
	    }

	    public ActionResult Get()
	    {
	        var characters = _characterRepository.Query(x => x.IsActive)
	            .Include(x => x.Class)
                .Include(x => x.Guild)
	            .OrderBy(x => x.OrderBy(y => y.Class.Name).ThenBy(y => y.Name))
                .Select()
	            .GroupBy(x => x.Class.Name);
	        return View(characters);
	    }

	    public async Task<ActionResult> Update(int id)
	    {
            _unitOfWork.BeginTransaction();

            var c = _characterRepository.Query(x => x.Id == id).Include(x => x.Loots.Select(loot => loot.Item)).Select().First();

            var cacheFilename = string.Format(@"{0}-{1}.json", "Greymane", c.Name);
            var fileName = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Cache"), cacheFilename);
            var fileInfo = new FileInfo(fileName);

            var explorer = new WowExplorer(Region.US);

            if (!System.IO.File.Exists(fileName) || fileInfo.LastWriteTime < DateTime.Now.AddDays(-1))
            {

                var character = explorer.GetCharacter("Greymane", c.Name, CharacterOptions.GetItems | CharacterOptions.GetFeed | CharacterOptions.GetGuild | CharacterOptions.GetProgression);
                var json = JsonConvert.SerializeObject(character);

                // Cache the file ...
                System.IO.File.WriteAllText(fileName, json);
            }

            var characterJson = JsonConvert.DeserializeObject<Character>(System.IO.File.ReadAllText(fileName));

            c.EquippedItemLevel = characterJson.Items.AverageItemLevelEquipped;
            c.Level = characterJson.Level;

            // Check for items that are equipped
	        var backItem = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Back.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item2 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Chest.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item3 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Feet.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item4 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Finger1.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item5 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Finger2.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item6 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Hands.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item7 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Head.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item8 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Legs.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item9 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.MainHand.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item10 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Neck.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item11 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.OffHand.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item12 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Ranged.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item13 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Shirt.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item14 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Shoulder.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item15 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Trinket1.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item16 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Trinket2.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item17 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Waist.IfNotNull(item => item.Id))).Select().FirstOrDefault();
            var item18 = _itemRepository.Query(new FindItemByItemId(characterJson.Items.Wrist.IfNotNull(item => item.Id))).Select().FirstOrDefault();

            // Add items that are equipped
	        if (backItem == null) backItem = CreateNewItem(explorer.GetItem((characterJson.Items.Back.IfNotNull(item => item.Id).ToString())));
	        if (item2 == null) item2 = CreateNewItem(explorer.GetItem((characterJson.Items.Chest.IfNotNull(item => item.Id).ToString())));
	        if (item3 == null) item3 = CreateNewItem(explorer.GetItem((characterJson.Items.Feet.IfNotNull(item => item.Id).ToString())));
	        if (item4 == null) item4 = CreateNewItem(explorer.GetItem((characterJson.Items.Finger1.IfNotNull(item => item.Id).ToString())));
	        if (item5 == null) item5 = CreateNewItem(explorer.GetItem((characterJson.Items.Finger2.IfNotNull(item => item.Id).ToString())));
	        if (item6 == null) item6 = CreateNewItem(explorer.GetItem((characterJson.Items.Hands.IfNotNull(item => item.Id).ToString())));
	        if (item7 == null) item7 = CreateNewItem(explorer.GetItem((characterJson.Items.Head.IfNotNull(item => item.Id).ToString())));
	        if (item8 == null) item8 = CreateNewItem(explorer.GetItem((characterJson.Items.Legs.IfNotNull(item => item.Id).ToString())));
	        if (item9 == null) item9 = CreateNewItem(explorer.GetItem((characterJson.Items.MainHand.IfNotNull(item => item.Id).ToString())));
	        if (item10 == null) item10 = CreateNewItem(explorer.GetItem((characterJson.Items.Neck.IfNotNull(item => item.Id).ToString())));
	        if (item11 == null) item11 = CreateNewItem(explorer.GetItem((characterJson.Items.OffHand.IfNotNull(item => item.Id).ToString())));
	        if (item12 == null) item12 = CreateNewItem(explorer.GetItem((characterJson.Items.Ranged.IfNotNull(item => item.Id).ToString())));
	        if (item13 == null) item13 = CreateNewItem(explorer.GetItem((characterJson.Items.Shirt.IfNotNull(item => item.Id).ToString())));
	        if (item14 == null) item14 = CreateNewItem(explorer.GetItem((characterJson.Items.Shoulder.IfNotNull(item => item.Id).ToString())));
	        if (item15 == null) item15 = CreateNewItem(explorer.GetItem((characterJson.Items.Trinket1.IfNotNull(item => item.Id).ToString())));
	        if (item16 == null) item16 = CreateNewItem(explorer.GetItem((characterJson.Items.Trinket2.IfNotNull(item => item.Id).ToString())));
	        if (item17 == null) item17 = CreateNewItem(explorer.GetItem((characterJson.Items.Waist.IfNotNull(item => item.Id).ToString())));
	        if (item18 == null) item18 = CreateNewItem(explorer.GetItem((characterJson.Items.Wrist.IfNotNull(item => item.Id).ToString())));
            
	        if (backItem != null) c.AddLoot(backItem, null, true);
	        if (item2 != null) c.AddLoot(item2, null, true);
	        if (item3 != null) c.AddLoot(item3, null, true);
	        if (item4 != null) c.AddLoot(item4, null, true);
	        if (item5 != null) c.AddLoot(item5, null, true);
	        if (item6 != null) c.AddLoot(item6, null, true);
	        if (item7 != null) c.AddLoot(item7, null, true);
	        if (item8 != null) c.AddLoot(item8, null, true);
	        if (item9 != null) c.AddLoot(item9, null, true);
	        if (item10 != null) c.AddLoot(item10, null, true);
	        if (item11 != null) c.AddLoot(item11, null, true);
	        if (item12 != null) c.AddLoot(item12, null, true);
	        if (item13 != null) c.AddLoot(item13, null, true);
	        if (item14 != null) c.AddLoot(item14, null, true);
	        if (item15 != null) c.AddLoot(item15, null, true);
	        if (item16 != null) c.AddLoot(item16, null, true);
	        if (item17 != null) c.AddLoot(item17, null, true);
	        if (item18 != null) c.AddLoot(item18, null, true);
	        
            // Loop over LOOT feed
	        foreach (CharacterFeed characterFeed in characterJson.Feed.Where(x => x.Type == "LOOT"))
            {
                var wowItem = explorer.GetItem(characterFeed.ItemId.ToString());
                var item = _itemRepository.Query(x => x.ItemId == wowItem.Id).Select().FirstOrDefault();

                if (item == null)
                {
                    item = new Item(wowItem.Name, wowItem.Id, wowItem.ItemLevel, wowItem.Quality);
                    _itemRepository.Insert(item);
                }

                c.AddLoot(item, characterFeed.Timestamp.ToDateTime(), false);
            }

            c.UpdatedAt = DateTime.Now;
            _characterRepository.Update(c);

	        var changes = await _unitOfWork.SaveChangesAsync();

	        _unitOfWork.Commit();

	        return RedirectToAction("Details", new { id });
	    }

	    private Item CreateNewItem(WowDotNetAPI.Models.Item wowItem)
	    {
	        if (wowItem == null) return null;

            var item = new Item(wowItem.Name, wowItem.Id, wowItem.ItemLevel, wowItem.Quality);
	        _itemRepository.Insert(item);
	        return item;
	    }
	}

    public class FindItemByItemId : IQueryObject<Item>
    {
        private readonly int _itemId;

        public FindItemByItemId(int itemId)
        {
            _itemId = itemId;
        }

        public Expression<Func<Item, bool>> Query()
        {
            return x => x.ItemId == _itemId;
        }

        public Expression<Func<Item, bool>> And(Expression<Func<Item, bool>> query)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Item, bool>> Or(Expression<Func<Item, bool>> query)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Item, bool>> And(IQueryObject<Item> queryObject)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Item, bool>> Or(IQueryObject<Item> queryObject)
        {
            throw new NotImplementedException();
        }
    }
}
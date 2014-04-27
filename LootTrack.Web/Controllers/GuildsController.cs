using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LootTrack.Domain.Models;
using LootTrack.Web.Foundation.Extensions;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using WowDotNetAPI;

namespace LootTrack.Web.Controllers
{
    public class GuildsController : Controller
    {
        private readonly IRepository<Guild> _guildRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GuildsController(IRepository<Guild> guildRepository, IRepository<Item> itemRepository, IUnitOfWork unitOfWork)
        {
            _guildRepository = guildRepository;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Guilds/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Guilds/Details/5
        public ActionResult Details(int id)
        {
            var guild = _guildRepository.Query(x => x.Id == id).Include(x => x.Characters);

            return View();
        }

        //
        // GET: /Guilds/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Guilds/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Guilds/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Guilds/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var guild = _guildRepository.Query(x => x.Id == id).Include(x => x.Characters).Select().FirstOrDefault();

                var client = new WowExplorer(Region.US);

                var apiGuild = client.GetGuild("Greymane", "Solution", GuildOptions.GetNews);

                foreach (var element in apiGuild.News.Where(x => x.Type.ToUpper() == "ITEMLOOT"))
                {
                    var itemId = element.ItemID;
                    var characterName = element.Character;

                    if (!guild.Characters.Any(x => x.Name == characterName)) continue;

                    var item = _itemRepository.Query(x => x.ItemId == itemId).Select().FirstOrDefault();

                    if (item == null)
                    {
                        var apiItem = client.GetItem(itemId.ToString());
                        item = new Item(apiItem.Name, apiItem.Id, apiItem.ItemLevel, apiItem.Quality);
                        _itemRepository.Insert(item);
                    }

                    var character = guild.Characters.FirstOrDefault(x => x.Name == characterName);

                    character.AddLoot(item, element.Timestamp.ToDateTime(), false);

                    
                }

                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Guilds/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Guilds/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

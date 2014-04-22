//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Web;
//using System.Web.Http;
//using LootTrack.Web.Foundation.Extensions;
//using Newtonsoft.Json;
//using WowDotNetAPI;
//using WowDotNetAPI.Models;
//
//namespace LootTrack.Web.Controllers.Api
//{
//    using System.Linq;
//    using EntityFramework.Patterns;
//    using Domain.Models;
//
//    public class CharactersController : ApiController
//    {
//        public CharactersController(IRepository<Character> characterRepository, IRepository<Item> itemRepository, IUnitOfWork unitOfWork)
//        {
//            _characterRepository = characterRepository;
//            _itemRepository = itemRepository;
//            _unitOfWork = unitOfWork;
//        }
//
//        // GET api/values
//        public IEnumerable<Character> Get()
//        {
//            var characters = _characterRepository.GetAll().Where(x => x.IsActive).ToList();
//            return characters;
//        }
//
//        // GET api/values/5
//        public string Get(int id)
//        {
//            return "value";
//        }
//
//        // POST api/values
//        public void Post([FromBody]string value)
//        {
//        }
//
//        // PUT api/values/5
//        public void Put(int id, [FromBody]string value)
//        {
//            var c = _characterRepository.Single(x => x.Id == id, x => x.Loots);
//
//            var cacheFilename = string.Format(@"{0}-{1}.json", "Greymane", c.Name);
//            var fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Cache"), cacheFilename);
//            var fileInfo = new FileInfo(fileName);
//
//            var explorer = new WowExplorer(Region.US);
//
//            if (!File.Exists(fileName) || fileInfo.LastWriteTime < DateTime.Now.AddDays(-1))
//            {
//                
//                var character = explorer.GetCharacter("Greymane", c.Name, CharacterOptions.GetItems | CharacterOptions.GetFeed | CharacterOptions.GetGuild | CharacterOptions.GetProgression);
//                var json = JsonConvert.SerializeObject(character);
//
//                // Cache the file ...
//                File.WriteAllText(fileName, json);
//            }
//
//            var characterJson = JsonConvert.DeserializeObject<WowDotNetAPI.Models.Character>(File.ReadAllText(fileName));
//
//            c.EquippedItemLevel = characterJson.Items.AverageItemLevelEquipped;
//            c.Level = characterJson.Level;
//
//            foreach (CharacterFeed characterFeed in characterJson.Feed.Where(x => x.Type == "LOOT"))
//            {
//                var wowItem = explorer.GetItem(characterFeed.ItemId.ToString());
//                var item = new Item(wowItem.Name, wowItem.Id, wowItem.ItemLevel, wowItem.Quality);
//
//                if (!_itemRepository.Find(x => x.ItemId == wowItem.Id).Any())
//                    _itemRepository.Insert(item);
//
//                c.AddLoot(item, characterFeed.Timestamp.ToDateTime(), true);
//            }
//
//            c.UpdatedAt = DateTime.Now;
//            _characterRepository.Update(c);
//            _unitOfWork.Commit();
//        }
//
//        // DELETE api/values/5
//        public void Delete(int id)
//        {
//        }
//    }
//}

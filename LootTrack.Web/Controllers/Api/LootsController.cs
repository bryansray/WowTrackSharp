using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using LootTrack.Domain.Models;
using LootTrack.Web.ViewModels;
using Repository.Pattern.Repositories;

namespace LootTrack.Web.Controllers.Api
{
    public class LootsController : ApiController
    {
        private readonly IRepository<Character> _characterRepository;
        private readonly IRepository<Loot> _lootRepository;

        public LootsController(IRepository<Character> characterRepository, IRepository<Loot> lootRepository)
        {
            _characterRepository = characterRepository;
            _lootRepository = lootRepository;
        }

        public IEnumerable<LootItemViewModel> Get()
        {
            var total = 50;
            var loots = _lootRepository.Query().Include(x => x.Character).Include(x => x.Character.Class).Include(x => x.Item).OrderBy(x => x.OrderByDescending(y => y.ReceivedAt)).SelectPage(1, 50, out total);

            
            var lootItemViewModels = loots.Select(
                x =>
                    new LootItemViewModel()
                    {
                        ItemId = x.Item.Id,
                        ItemLevel = x.Item.Level,
                        Quality = x.Item.Quality,
                        Name = x.Item.Name,
                        Character = x.Character,
                        ReceivedAt = x.ReceivedAt.Value.ToString("MM/dd/yyyy")
                    });
            
            return lootItemViewModels;
        } 

        // GET api/loots/{characterId}
        public IEnumerable<LootItemViewModel> Get(int characterId)
        {
            var character = _characterRepository.Query(x => x.Id == characterId).Include(x => x.Loots.Select(loot => loot.Item)).Select().First();

            return character.Loots.OrderByDescending(x => x.ReceivedAt).Select(loot => new LootItemViewModel() { ItemId = loot.Item.ItemId, Name = loot.Item.Name, ReceivedAt = loot.ReceivedAt.HasValue ? loot.ReceivedAt.Value.ToString("MM/dd/yyyy hh:mm tt") : string.Empty, ItemLevel = loot.Item.Level, Character = loot.Character, Quality = loot.Item.Quality }).ToList();
        }

//        // GET api/loots/5
//        public string Get(int id)
//        {
//            return "value";
//        }
//
//        // POST api/loots
//        public void Post([FromBody]string value)
//        {
//        }
//
//        // PUT api/loots/5
//        public void Put(int id, [FromBody]string value)
//        {
//        }
//
//        // DELETE api/loots/5
//        public void Delete(int id)
//        {
//        }
    }
}

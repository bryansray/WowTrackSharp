using LootTrack.Domain.Models;

namespace LootTrack.Web.ViewModels
{
    public class LootItemViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int ItemLevel { get; set; }
        public int Quality { get; set; }
        public string ReceivedAt { get; set; }
        public Character Character { get; set; }
    }
}
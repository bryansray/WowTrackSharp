using System.Collections.ObjectModel;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;

namespace LootTrack.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class Character : Entity
    {
        public Character()
        {
            Loots = new Collection<Loot>();
        }

        public int Id { get; set; }

        public Guild Guild { get; set; }

        public string Name { get; set; }

        public string Server { get; set; }

        public int Level { get; set; }

        public int EquippedItemLevel { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Loot> Loots { get; set; }

        public Class Class { get; set; }

        public void AddLoot(Item item, DateTime? receivedAt, bool isEquipped)
        {
            var loot = new Loot(this, item, receivedAt, isEquipped);
            
            if (!Loots.Contains(loot))
                Loots.Add(loot);
        }
    }
}
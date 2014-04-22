using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace LootTrack.Domain.Models
{
    public class Item : Entity
    {
        public Item() {}
        public Item(string name, int itemId, int level, int quality)
        {
            Name = name;
            ItemId = itemId;
            Level = level;
            Quality = quality;
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public int Level { get; protected set; }

        public int Quality { get; protected set; }

        public int ItemId { get; protected set; }

        public DateTime? CreatedAt { get; protected set; }

        public ICollection<Loot> Loots { get; protected set; }

        protected bool Equals(Item other)
        {
            return ItemId == other.ItemId && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ItemId*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}
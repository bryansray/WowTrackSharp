using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;

namespace LootTrack.Domain.Models
{
    using System;

    public class Loot : Entity
    {
        public Loot() {}
        public Loot(Character character, Item item, DateTime? receivedAt, bool isEquipped)
        {
            Character = character;
            Item = item;
            ReceivedAt = receivedAt;
            IsEquipped = isEquipped;

            ObjectState = ObjectState.Added;
        }

        public int Id { get; protected set; }

        public bool IsEquipped { get; protected set; }

        public bool IsUpdatedManually { get; protected set; }

        public DateTime? ReceivedAt { get; protected set; }

        public Character Character { get; protected set; }

        public Item Item { get; protected set; }

        protected bool Equals(Loot other)
        {
            return ReceivedAt.Equals(other.ReceivedAt) && Character.Equals(other.Character) && Item.Equals(other.Item);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Loot) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ReceivedAt.GetHashCode();
                hashCode = (hashCode*397) ^ (Character != null ? Character.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Item != null ? Item.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
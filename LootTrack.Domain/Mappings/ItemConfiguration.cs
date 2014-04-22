using System.Data.Entity.ModelConfiguration;
using LootTrack.Domain.Models;

namespace LootTrack.Domain.Mappings
{
    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            Property(x => x.Level).HasColumnName("ItemLevel");

            HasMany(x => x.Loots);
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace LootTrack.Domain.Mappings
{
    using System.Data.Entity.ModelConfiguration;

    using LootTrack.Domain.Models;

    public class LootConfiguration : EntityTypeConfiguration<Loot>
    {
        public LootConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using LootTrack.Domain.Models;

namespace LootTrack.Domain.Mappings
{
    public class CharacterConfiguration : EntityTypeConfiguration<Character>
    {
        public CharacterConfiguration()
        {
            Property(x => x.Id);

            HasRequired(x => x.Class);

            HasMany(x => x.Loots);
        }
    }

    public class ClassConfiguration : EntityTypeConfiguration<Class>
    {
        public ClassConfiguration()
        {
            HasKey(x => x.Id);

            HasMany(x => x.Characters);
        }
    }

    public class GuildConfiguration : EntityTypeConfiguration<Guild>
    {
        public GuildConfiguration()
        {
            HasMany(x => x.Characters);
        }
    }
}
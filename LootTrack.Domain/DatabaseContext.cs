using System.Data.Entity;
using LootTrack.Domain.Mappings;
using LootTrack.Domain.Models;
using Repository.Pattern.Ef6;

namespace LootTrack.Domain
{
    public class DatabaseContext : DataContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
        }

        public DbSet<Character> Characters { get; set; }
        public IDbSet<Class> Classes { get; set; }
        public IDbSet<Item> Items { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GuildConfiguration());
            modelBuilder.Configurations.Add(new CharacterConfiguration());
            modelBuilder.Configurations.Add(new LootConfiguration());
            modelBuilder.Configurations.Add(new ItemConfiguration());
            modelBuilder.Configurations.Add(new ClassConfiguration());
        }
    }
}

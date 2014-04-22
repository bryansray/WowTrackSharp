using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace LootTrack.Domain.Models
{
    public class Guild : Entity
    {
        public int Id { get; protected set; }

        public ICollection<Character> Characters { get; set; }
    }
}
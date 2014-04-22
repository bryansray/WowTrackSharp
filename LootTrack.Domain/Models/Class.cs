using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace LootTrack.Domain.Models
{
    public class Class : Entity
    {
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public string Code { get; protected set; }

        public string ClassColor { get; protected set; }

        public DateTime CreatedAt { get; protected set; }
        
        public ICollection<Character> Characters { get; set; }
    }
}
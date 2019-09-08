using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();

        public Team()
        {

        }

        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

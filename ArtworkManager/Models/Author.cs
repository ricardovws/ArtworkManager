using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public Team Team { get; set; }
        public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

        public Author()
        {

        }

        public Author(int id, string name, string user, string email, Team team)
        {
            Id = id;
            Name = name;
            User = user;
            Email = email;
            Team = team;
        }


    }
}

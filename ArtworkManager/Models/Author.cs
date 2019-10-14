using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
        public MasterOfAllArtworks Master { get; set; }

        public Author()
        {
        }

        public Author(int id, string name, string user, string email, Team team, MasterOfAllArtworks master)
        {
            Id = id;
            Name = name;
            User = user;
            Email = email;
            Team = team;
            Master = master;
        }

        public void LoadCodePack(Author author, MasterOfAllArtworks master)
        {
            try
            {
                for (int i = 0; i < 25; i++)
                {
                    var artwork = master.ALotOfArtworks.First(obj => obj.Status == Models.Enums.ArtworkStatus.FreeToUse);
                    artwork.OwnerID = author.Id;
                    Artworks.Add(artwork);
                    master.ALotOfArtworks.Remove(artwork);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred!");
                Console.WriteLine(e.Message);
            }
        }
    }
}


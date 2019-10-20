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
        public int TeamId { get; set; }
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

        public void LoadCodePack(List<string> listOfArtworks)
        {
            try
            {
                for (int i = 0; i < 2500; i++)
                {
                    var artworkCode = listOfArtworks.First();
                    listOfArtworks.RemoveAt(0);
                    string[] artwork = artworkCode.Split(',');
                    int artworkId = int.Parse(artwork[0]);
                    string artworkNumber = artwork[1];
                    Artwork artworkx = new Artwork(artworkId, artworkNumber);
                    artworkx.OwnerID = Id;
                    Artworks.Add(artworkx);

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


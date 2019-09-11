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

        public void LoadCodePack(Author author)
        {
            
            try
            {
                string sourcepath = @"c:\teste1\RS85424.csv";
                string[] lines = File.ReadAllLines(sourcepath);
                using (StreamWriter sw = File.AppendText(sourcepath))
                    foreach (string line in lines)
                    {
                        string[] numbers = line.Split(",");
                        int id = int.Parse(numbers[0]);
                        string code = numbers[1];
                        Artwork artwork = new Artwork(id, code, author);
                        Artworks.Add(artwork);
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


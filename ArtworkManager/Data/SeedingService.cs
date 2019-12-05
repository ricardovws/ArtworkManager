using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using System.IO;

namespace ArtworkManager.Data
{
    public class SeedingService
    {
        private ArtworkManagerContext _context;

        public SeedingService(ArtworkManagerContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Team.Any() || _context.Author.Any() || _context.Artwork.Any() || _context.ArtworkCode.Any())
            {
                return; // DB has been seeded
            }

            Team t0 = new Team(3, "John Deere");
            Team t1 = new Team(1, "John Deere Luedtke");
            Team t2 = new Team(2, "N.E.I.V.A.");


            //Rafael Neves Poersch - Manager
            Author a5 = new Author(5, "Rafael Neves Poersch", "RP46668", "PoerschRafaelN@JohnDeere.com", t1);
            User u5 = new User();
            u5.Login = "rafael";
            u5.Name = "Rafael Neves Poersch";
            u5.Password = "rafael";
            u5.OwnerId = 5;
            u5.Admin = true;
            u5.Id = 5;


            //Ricardo dos Santos
            Author a1 = new Author(1, "Ricardo dos Santos", "RS85424", "SantosRicardoV@johndeere.com", t1);
            User u1 = new User();
            u1.Login = "ricardo";
            u1.Name = "Ricardo dos Santos";
            u1.Password = "ricardo";
            u1.OwnerId = 1;
            u1.Admin = false;
            u1.Id = 1;


            //Lucas Rech
            Author a2 = new Author(2, "Lucas Rech", "LR35023", "RechLucasP@johndeere.com", t1);
            User u2 = new User();
            u2.Login = "lucas";
            u2.Name = "Lucas Rech";
            u2.Password = "lucas";
            u2.OwnerId = 2;
            u2.Admin = false;
            u2.Id = 2;


            //Tassiane Strassburger
            Author a3 = new Author(3, "Tassiane Strassburger", "TS95756", "StrassburgerTassiane@johndeere.com", t1);
            User u3 = new User();
            u3.Login = "tassiane";
            u3.Name = "Tassiane Strassburger";
            u3.Password = "tassi";
            u3.OwnerId = 3;
            u3.Admin = false;
            u3.Id = 3;

            //Kaizan Kolbek
            Author a4 = new Author(4, "Kaizan F Kolbek", "KK69021", "KolbekKaizanF@johndeere.com", t1);
            User u4 = new User();
            u4.Login = "kaizan";
            u4.Name = "Kaizan F Kolbek";
            u4.Password = "kaizan";
            u4.OwnerId = 4;
            u4.Admin = false;
            u4.Id = 4;


            
            _context.Team.AddRange(t0, t1, t2);
            _context.Author.AddRange(a1, a2, a3, a4, a5);
            _context.User.AddRange(u1, u2, u3, u4, u5);

            // Creating artworks...

            
            string x = "BML";
            int n = 100; //Total of artworks!!!
            List<string> listOfArtworks = new List<string>();
            int num = 0;
            string number = num.ToString();

            for (int i = 000001; i <= n; i++) // "i" represents the first parameter code to begin the range!
            {
                int id = i;
                string q1code = number + i;
                string code = q1code.ToString().PadLeft(6, '0');
                if (code.Length > 6)
                {
                    double codex = double.Parse(code);
                    code = codex.ToString();
                }
                string codexx = x + code;
                string artwork = i + "," + codexx;
                listOfArtworks.Add(artwork);
                
            }


           
            a1.LoadCodePack(listOfArtworks);
            a2.LoadCodePack(listOfArtworks);
            a3.LoadCodePack(listOfArtworks);
            a4.LoadCodePack(listOfArtworks);
            a5.LoadCodePack(listOfArtworks);


            _context.SaveChanges();

            var listOfArtworkJustCodes = listOfArtworks;

            foreach (string objArtwork in listOfArtworkJustCodes)
            {
                var artworkWithCode = objArtwork;
                string[] artwork = artworkWithCode.Split(',');
                int artworkId = int.Parse(artwork[0]);
                string artworkNumber = artwork[1];
                ArtworkCode artworkCode = new ArtworkCode(artworkId, artworkNumber);
                _context.ArtworkCode.Add(artworkCode);

                
            }

            _context.SaveChanges();

        }
    }
}

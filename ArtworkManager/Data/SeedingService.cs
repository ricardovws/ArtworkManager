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
            if (_context.Team.Any() || _context.Author.Any() || _context.Artwork.Any())
            {
                return; // DB has been seeded
            }

            Team t0 = new Team(3, "John Deere");
            Team t1 = new Team(1, "John Deere Luedtke");
            Team t2 = new Team(2, "N.E.I.V.A.");
            
           
            //Ricardo dos Santos
            Author a1 = new Author(1, "Ricardo dos Santos", "RS85424", "SantosRicardoV@johndeere.com", t1);
            User u1 = new User();
            u1.Login = "ricardo";
            u1.Name = "ricardo";
            u1.Password = "ricardo";
            u1.OwnerId = 1;
            u1.Admin = true;
            u1.Id = 1;




            //Lucas Rech
            Author a2 = new Author(2, "Lucas Rech", "LR35023", "RechLucasP@johndeere.com", t1);
            User u2 = new User();
            u2.Login = "lucas";
            u2.Name = "lucas";
            u2.Password = "lucas";
            u2.OwnerId = 2;
            u2.Admin = false;
            u2.Id = 2;


            //Tassiane Strassburger
            Author a3 = new Author(3, "Tassiane Strassburger", "TS95756", "StrassburgerTassiane@johndeere.com", t1);
            User u3 = new User();
            u3.Login = "TASSI";
            u3.Name = "tassi";
            u3.Password = "tassi";
            u3.OwnerId = 3;
            u3.Admin = false;
            u3.Id = 3;


            Author a4 = new Author(4, "Kaizan F Kolbek", "KK69021", "KolbekKaizanF@johndeere.com", t1);

           

          





            _context.Team.AddRange(t0, t1, t2);
            _context.Author.AddRange(a1, a2, a3, a4);
            _context.User.AddRange(u1, u2, u3);

            // Creating artworks...

            
            string x = "BM";
            int n = 10000; //Total of artworks!!!
            List<string> listOfArtworks = new List<string>();
            int num = 0;
            string number = num.ToString();

            for (int i = 0; i <= n; i++)
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



            //

            //int idArtwork = 0;
            //while (idArtwork < 10000)
            //{
            //idArtwork++;
            //string codigoArtwork = "BM" + idArtwork.ToString().PadLeft(6, '0');
            //Artwork item = new Artwork(idArtwork, codigoArtwork);
            //if (idArtwork < 2600)
            //{
            //item.OwnerID = 1;
            //}
            //    else if (idArtwork < 5100)
            //{
            //item.OwnerID = 2;
            //}
            //    else if (idArtwork < 7600)
            //{
            //item.OwnerID = 3;
            //}
            //    else
            //{
            //item.OwnerID = 4;
            //}
            //_context.Artwork.Add(item);
            //  }

           
            a1.LoadCodePack(listOfArtworks);
            a2.LoadCodePack(listOfArtworks);
            a3.LoadCodePack(listOfArtworks);
            a4.LoadCodePack(listOfArtworks);


            _context.SaveChanges();
            


        }
    }
}

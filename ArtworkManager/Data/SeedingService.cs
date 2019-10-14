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

            Team t1 = new Team(1, "John Deere Luedtke");
            Team t2 = new Team(2, "N.E.I.V.A.");
            
            MasterOfAllArtworks master = new MasterOfAllArtworks(1);

            Author a1 = new Author(1, "Ricardo dos Santos", "RS85424", "SantosRicardoV@johndeere.com", t1, master);
            Author a2 = new Author(2, "Lucas Rech", "LR35023", "RechLucasP@johndeere.com", t1, master);
            Author a3 = new Author(3, "Tassiane Strassburger", "TS95756", "StrassburgerTassiane@johndeere.com", t1, master);
            Author a4 = new Author(4, "Kaizan F Kolbek", "KK69021", "KolbekKaizanF@johndeere.com", t1, master);

            User u1 = new User();
            u1.Login = "ricardo";
            u1.Name = "ricardo";
            u1.Password = "ricardo";
            u1.OwnerId = 1;
            u1.Admin = true;
            u1.Id = 1;
            _context.Team.AddRange(t1, t2);
            _context.Master.Add(master);
            _context.Author.AddRange(a1, a2, a3, a4);
            _context.User.Add(u1);


            //

            int idArtwork = 0;
            while(idArtwork < 10000)
            {
                idArtwork++;
                string codigoArtwork = "BM" + idArtwork.ToString().PadLeft(6, '0');
                Artwork item = new Artwork(idArtwork, codigoArtwork);
                if (idArtwork < 2600)
                {
                    item.OwnerID = 1;
                }
                else if (idArtwork < 5100)
                {
                    item.OwnerID = 2;
                }
                else if (idArtwork < 7600)
                {
                    item.OwnerID = 3;
                }
                else
                {
                    item.OwnerID = 4;
                }
                _context.Artwork.Add(item);
            }

            //master.CreateAllArtworks();
            //a1.LoadCodePack(a1, master);
            //a2.LoadCodePack(a2, master);
            //a3.LoadCodePack(a3, master);
            //a4.LoadCodePack(a4, master);


            _context.SaveChanges();
            
        }
    }
}

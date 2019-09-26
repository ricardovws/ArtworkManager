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
            

            Author a1 = new Author(1, "Ricardo dos Santos", "RS85424", "SantosRicardoV@johndeere.com", t1);
            a1.LoadCodePack(a1);
            Author a2 = new Author(2, "Lucas Rech", "LR35023", "RechLucasP@johndeere.com", t1);
            a2.LoadCodePack(a2);
            Author a3 = new Author(3, "Tassiane Strassburger", "TS95756", "StrassburgerTassiane@johndeere.com", t1);
            a3.LoadCodePack(a3);
            Author a4 = new Author(4, "Kaizan F Kolbek", "KK69021", "KolbekKaizanF@johndeere.com", t1);
            a4.LoadCodePack(a4);



            _context.Team.AddRange(t1, t2);
            _context.Author.AddRange(a1, a2, a3, a4);
            
            
            _context.SaveChanges();


        }
    }
}

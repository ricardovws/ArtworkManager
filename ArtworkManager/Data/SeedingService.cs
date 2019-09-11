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
            Team t3 = new Team(3, "Jujubeira");

            Author a1 = new Author(1, "Ricardo dos Santos", "RS85424", "SantosRicardoV@johndeere.com", t1);
            a1.LoadCodePack(a1);
            Author a2 = new Author(2, "Lucas Rech", "LS000000000", "RechLucas@johndeere.com", t1);
            Author a3 = new Author(3, "Jujubeira", "0000", "jaeras", t1);


            
            _context.Team.AddRange(t1, t2, t3);
            _context.Author.AddRange(a1, a2, a3);
            
            
            _context.SaveChanges();


        }
    }
}

using ArtworkManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Services
{
    public class AuthorService
    {
        private readonly ArtworkManagerContext _context;

        public AuthorService(ArtworkManagerContext context)
        {
            _context = context;
        }

        public List<Author> ShowAllAuthors()
        {
            return _context.Author.ToList();
        }

        public Author FindAuthorById (int id)
        {
            return _context.Author.First(obj => obj.Id == id);
            
        }

        public Artwork GetACode (Author owner)
        {
            return _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

        }

        public void UseCode (Author owner)
        {
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);
            var obj1 = objj;
            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            obj1.Status = Models.Enums.ArtworkStatus.Used;
            obj1.BirthDate = DateTime.Now;
            
            _context.Artwork.Add(obj1);
            _context.SaveChanges();
        }
    }
}

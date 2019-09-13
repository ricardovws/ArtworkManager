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
    }
}

using ArtworkManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Services
{
    public class ArtworkService
    {
        private readonly ArtworkManagerContext _context;


        public ArtworkService(ArtworkManagerContext context)
        {
            _context = context;
        }

  
        public List<Artwork> FindByDate(DateTime? minDate, DateTime? maxDate)
        {

            var result = _context.Artwork.Where(b => b.BirthDate >= minDate.Value && b.BirthDate <= maxDate.Value).ToList();

       

            return result;

        }
    }
}

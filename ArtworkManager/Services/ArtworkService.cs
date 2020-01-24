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
            var result = from obj in _context.Artwork select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.BirthDate >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.BirthDate <= maxDate.Value);
            }

            return result
                .Include(x => x.Owner)                
                .ToList();
        }
    }
}

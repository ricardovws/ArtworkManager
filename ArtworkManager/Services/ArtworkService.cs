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

  
        public async Task<List<Artwork>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
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
            return await result
                .Include(x => x.Owner)                
                .ToListAsync();
        }
    }
}

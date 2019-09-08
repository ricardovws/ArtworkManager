using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ArtworkManager.Models
{
    public class ArtworkManagerContext : DbContext
    {
        public ArtworkManagerContext (DbContextOptions<ArtworkManagerContext> options)
            : base(options)
        {
        }

        public DbSet<ArtworkManager.Models.Team> Team { get; set; }
    }
}

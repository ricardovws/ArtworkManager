using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArtworkManager.Models;

namespace ArtworkManager.Models
{
    public class ArtworkManagerContext : DbContext
    {
        public ArtworkManagerContext (DbContextOptions<ArtworkManagerContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Artwork> Artwork { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ArtworkCode> ArtworkCode { get; set; }
    }
}

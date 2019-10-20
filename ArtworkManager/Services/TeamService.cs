using ArtworkManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Services
{
    public class TeamService
    {
        private readonly ArtworkManagerContext _context;

        public TeamService(ArtworkManagerContext context)
        {
            _context = context;
        }

        public List<Team> FindAll()
        {
            return _context.Team.OrderBy(x => x.Name).ToList();
        }





    }
}

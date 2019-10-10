using ArtworkManager.Models;
using ArtworkManager.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Services
{
    public class UserService
    {
        private readonly ArtworkManagerContext _context;

        public UserService(ArtworkManagerContext context)
        {
            _context = context;
        }
        
        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.User
                //.Include(x => x.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

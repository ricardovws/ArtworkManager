using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models.ViewModels
{
    public class AuthorFormViewModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public ICollection<Team> Teams { get; set; }
        public int TeamId { get; set; }
        
    }
}

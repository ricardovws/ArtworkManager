using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models.ViewModels
{
    public class UserFormViewModel
    {
       
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<Author> Owners { get; set; }
        public int OwnerId { get; set; }
        public bool Admin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models.ViewModels
{
    public class UserFormViewModel
    {
        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string Password { get; set; }
        public ICollection<Author> Owners { get; set; }
        public int OwnerId { get; set; }
        public bool Admin { get; set; }
    }
}

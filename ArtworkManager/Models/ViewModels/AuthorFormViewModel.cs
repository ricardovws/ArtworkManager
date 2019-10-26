using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models.ViewModels
{
    public class AuthorFormViewModel
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string User { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string Email { get; set; }
        public ICollection<Team> Teams { get; set; }
        public int TeamId { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models.ViewModels
{
    public class UpdateAuthorViewModel
    {
        public Author Author { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}

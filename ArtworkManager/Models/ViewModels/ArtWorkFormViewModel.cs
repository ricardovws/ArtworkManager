using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models.Enums;

namespace ArtworkManager.Models.ViewModels
{
    public class ArtWorkFormViewModel
    {
        public Artwork Artwork { get; set; }
        public string PublicationCode { get; set; }
        public string LastArtworkCodeUsed { get; set; }

        [Display(Name = "Sure, it is!")]
        public bool TypeOfArtwork { get; set; }
    }
}

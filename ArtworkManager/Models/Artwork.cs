using ArtworkManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Author Owner { get; set; }
        public int OwnerID { get; set; }
        public string PublicationCode { get; set; }
        public ArtworkStatus Status { get; set; }
        public DateTime BirthDate { get; set; }
        public bool TypeOfArtwork { get; set; }
        public TypeOfArtwork Type { get; set; }





        public Artwork()
        {

        }

        public Artwork(int id, string code)
        {
            Id = id;
            Code = code;
            Status = ArtworkStatus.FreeToUse;
        }

        public Artwork(int id, string code, Author owner, int ownerID)
        {
            Id = id;
            Code = code;
            Owner = owner;
            PublicationCode = "Not Specified";
            Status = ArtworkStatus.FreeToUse;
            OwnerID = ownerID;
            
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models
{
    public class ArtworkCode
    {

        public int Id { get; set; }
        public string ArtworkCodeCode { get; set; }

        public ArtworkCode(int id, string artworkCodeCode)
        {
            Id = id;
            ArtworkCodeCode = artworkCodeCode;
        }
    }


}

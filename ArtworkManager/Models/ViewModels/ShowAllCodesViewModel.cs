using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models.ViewModels
{
    public class ShowAllCodesViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string PublicationCode { get; set; }
        public string Status { get; set; }
        public string BirthDate { get; set; }
        public string TypeOfArwork { get; set; }
    }
}

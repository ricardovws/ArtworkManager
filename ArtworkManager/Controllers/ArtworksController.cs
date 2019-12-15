using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using ArtworkManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkManager.Controllers
{
    public class ArtworksController : Controller
    {

        private readonly ArtworkService _artworkService;
        private readonly AuthorService _authorService;

        public ArtworksController(ArtworkService artworkService, AuthorService authorService)
        {
            _artworkService = artworkService;
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleReport(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }


            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = minDate.Value.ToString("yyyy-MM-dd");
            var result = _artworkService.FindByDate(minDate, maxDate);

            foreach (Artwork art in result.ToList())
            {
                var artwork = result.First(b => b.BirthDate >= minDate.Value);
                var artworkfull = artwork;
                result.Remove(artwork);
                var artworkId = artwork.OwnerID;
                artworkfull.Owner = _authorService.FindAuthorById(artworkId);
                artworkfull.Owner.Team = _authorService.FindTeamByAuthorId(artworkId);
                if(artworkfull.TypeOfArtwork == true)
                {
                    artworkfull.Type = Models.Enums.TypeOfArtwork.Advanced;
                }
                else
                {
                    artworkfull.Type = Models.Enums.TypeOfArtwork.Basic;
                }
                
                result.Add(artworkfull);
            }

            return View(result);
        }



    }
}
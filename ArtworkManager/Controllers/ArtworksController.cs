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

        public ArtworksController(ArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleReport(DateTime? minDate, DateTime? maxDate)
        {

            var result = _artworkService.FindByDate(minDate, maxDate);

            

            return View(result);
        }

    }
}
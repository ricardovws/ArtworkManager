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

        public async Task<IActionResult> SimpleReport(DateTime? minDate, DateTime? maxDate)
        {
            var result = await _artworkService.FindByDateAsync(minDate, maxDate);
            return View(result);

        }
    }
}
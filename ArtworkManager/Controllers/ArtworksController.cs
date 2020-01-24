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
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _artworkService.FindByDateAsync(minDate, maxDate);
            return View(result);

        }
    }
}
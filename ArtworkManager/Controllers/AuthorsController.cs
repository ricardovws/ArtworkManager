using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkManager.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            var list = _authorService.ShowAllAuthors();
            return View(list);
        }
    }
}
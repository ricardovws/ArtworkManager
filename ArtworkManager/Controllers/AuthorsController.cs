using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
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

         public IActionResult AddPublicationCode()
        {
            return View();
        }

        public IActionResult AskAboutCode()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPublicationCode(string publicationcode, int id)
        {
            var author = _authorService.FindAuthorById(id);
            _authorService.AddPublicationCode(author, publicationcode);
           return RedirectToAction(nameof(GetCode2), new { id = id});
        }
        



        public IActionResult GetCode(int id)
        {
            var author = _authorService.FindAuthorById(id);
            var obj = _authorService.GetACode(author);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCode(Author author)
        {
            _authorService.UseCode(author);
            return RedirectToAction(nameof(GetCode));
        }




        public IActionResult GetCode2(int id)
        {
            var author = _authorService.FindAuthorById(id);
            var obj = _authorService.GetACode(author);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCode2(int id, Author author)
        {
            _authorService.UseCode2(author);
            return RedirectToAction(nameof(GetCode), new { id=id});
        }

        public IActionResult ShowAllCodes(Author author)
        {
            var list = _authorService.ShowAllArtworks(author);
            return View(list);

        }

        public IActionResult Edit(int id)
        {
            var obj = _authorService.FindArtworkById(id);
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Artwork artwork)
        {
            var obj = _authorService.FindArtworkById(id);
            obj.PublicationCode = artwork.PublicationCode;
            _authorService.Update(id, obj);
            return RedirectToAction(nameof(ShowAllCodes), new { id=obj.OwnerID});
        }



    }
}
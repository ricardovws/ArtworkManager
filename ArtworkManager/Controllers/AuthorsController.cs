﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using ArtworkManager.Services;
using Microsoft.AspNetCore.Mvc;
using PagedList.Mvc;
using PagedList;
using ArtworkManager.Models.ViewModels;
using ArtworkManager.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ArtworkManager.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;
        private readonly UserService _userService;
        private readonly TeamService _teamService;

        public AuthorsController(AuthorService authorService, UserService userService, TeamService teamService)
        {
            _authorService = authorService;
            _userService = userService;
            _teamService = teamService;
        }


        public async Task<IActionResult> Index()
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = await _userService.FindByIdAsync(idUser);
            if (user == null || user.Id == 0)
            {
                RedirectToAction("Login", "Users");
            }
            var owner = _authorService.FindAuthorByUser(user);



            if (user.Admin == false)
            {
                var list2 = _authorService.ShowOnlyAAuthor(owner);

                return View(list2);
            }

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
            return RedirectToAction(nameof(GetNewCode), new { id = id });
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




        public IActionResult GetNewCode(int id)
        {
            var author = _authorService.FindAuthorById(id);
            var obj = _authorService.GetACode(author);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetNewCode(int id, Author author)
        {
            _authorService.UseNewCode(author);
            return RedirectToAction(nameof(GetCode), new { id = id });
        }

        public IActionResult ShowAllCodes(Author author)
        {
            var list = new List<Artwork>(); // _authorService.ShowAllArtworks(author);
            return View(list);
        }

        public IActionResult LoadData(int id)
        {
            try
            {
                var author = _authorService.FindAuthorById(id);

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                // getting all Customer data  
                if (sortColumn == "id")
                {
                    sortColumn = "status";
                    sortColumnDirection = "desc";
                }
                var list = _authorService.ShowAllArtworks(author, sortColumn, sortColumnDirection, searchValue, skip, pageSize, out recordsTotal);
                var listArtworks = new List<ShowAllCodesViewModel>();
                foreach (var item in list)
                {
                    var itemLista = new ShowAllCodesViewModel();
                    itemLista.Id = item.Id;
                    itemLista.Code = item.Code;
                    itemLista.PublicationCode = item.PublicationCode;
                    if (item.BirthDate != DateTime.MinValue)
                    {
                        itemLista.BirthDate = item.BirthDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    if (item.Status == Models.Enums.ArtworkStatus.FreeToUse)
                    {
                        itemLista.Status = "Free to use";
                    }
                    else
                    {
                        itemLista.Status = "Used";
                    }
                    listArtworks.Add(itemLista);
                }
                //Paging   
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = listArtworks });

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IActionResult Edit(int id)
        {
            var obj = _authorService.FindArtworkById(id);
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Artwork artwork)
        {
            var obj = _authorService.FindArtworkById(id);
            obj.PublicationCode = artwork.PublicationCode;
            _authorService.Update(id, obj);
            return RedirectToAction(nameof(ShowAllCodes), new { id = obj.OwnerID });
        }


        public IActionResult Create()
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);
            if (user.Admin == false)
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            var teams = _teamService.FindAll();
            var viewModel = new AuthorFormViewModel { Teams = teams };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorFormViewModel obj)
        {
            Author author = new Author();
            author.Id = _authorService.GetIdFree();         
            author.Name = obj.Name;
            author.TeamId = obj.TeamId;
            author.User = obj.User;
            author.Email = obj.Email;
            _authorService.Pot(author);
            _authorService.InsertAuthor(author);



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _authorService.FindAuthorById(id);
            if (obj == null)
            {
                return NotFound();
            }

            List<Team> teams = _teamService.FindAll();
            UpdateAuthorViewModel viewModel = new UpdateAuthorViewModel { Author = obj, Teams = teams };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update (int id, UpdateAuthorViewModel obj)
        {
            if (id != obj.Author.Id)
            {
                return BadRequest();
            }
          
              

                _authorService.UpdateAuthor(obj.Author);
                return RedirectToAction(nameof(Index));
          
        }




    }
}
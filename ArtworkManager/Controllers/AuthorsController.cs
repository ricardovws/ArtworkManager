using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using ArtworkManager.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ArtworkService _artworkService;


        public AuthorsController(AuthorService authorService, UserService userService, TeamService teamService, ArtworkService artworkService)
        {
            _authorService = authorService;
            _userService = userService;
            _teamService = teamService;
            _artworkService = artworkService;
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

   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPublicationCode(string publicationcode, int id)
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);

            if (user.OwnerId != id)
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            var author = _authorService.FindAuthorById(id);
            _authorService.AddPublicationCode(author, publicationcode);
            return RedirectToAction(nameof(GetNewCode), new { id = id });
        }




        public IActionResult GetCode(int id)
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);
          
                if (user.OwnerId != id)
                {
                    return RedirectToAction("AccessDenied", "Users");
                }
           

            var author = _authorService.FindAuthorById(id);
            string publicationcode = _authorService.ShowLastPublicationCode(author);
            string artworkcode = _authorService.ShowLastArtworkUsed(author);
            
            var artwork = _authorService.GetACode(author);

            var cookieValue = false;
            if(Request.Cookies["ChooseType_"] != null)
            {
                cookieValue = bool.Parse(Request.Cookies["ChooseType_"]);
            }
            

            var obj = new ArtWorkFormViewModel { Artwork = artwork, PublicationCode = publicationcode, LastArtworkCodeUsed = artworkcode, TypeOfArtwork = cookieValue};
            

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCode(Author author, bool typeofartwork)
        {
         

            _authorService.UseCode(author, typeofartwork);

            Response.Cookies.Append("ChooseType_", typeofartwork.ToString());

            return RedirectToAction(nameof(GetCode));
        }




        public IActionResult GetNewCode(int id)
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);
           
                if (user.OwnerId != id)
                {
                    return RedirectToAction("AccessDenied", "Users");
                }
           
            var author = _authorService.FindAuthorById(id);
            string publicationcode = _authorService.ShowLastPublicationCode(author);
            string artworkcode = _authorService.ShowLastArtworkUsed(author);
            var artwork = _authorService.GetACode(author);
            var obj = new ArtWorkFormViewModel { Artwork = artwork, PublicationCode = publicationcode, LastArtworkCodeUsed=artworkcode };
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetNewCode(int id, Author author, bool typeofartwork)
        {

            _authorService.UseNewCode(author, typeofartwork);
            return RedirectToAction(nameof(GetCode), new { id = id , typeofartwork = typeofartwork});
        }

        public IActionResult ShowAllCodes(Author author)
        {
            var list = new List<Artwork>(); // _authorService.ShowAllArtworks(author);
            return View(list);
        }

        public IActionResult LoadData(int id)

        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);
            if(user.Admin != true)
            {
                if (user.OwnerId != id)
                {
                    return RedirectToAction("AccessDenied", "Users");
                }
            }
            
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
                    if (item.TypeOfArtwork == false && item.Status == Models.Enums.ArtworkStatus.Used)
                    {
                        itemLista.TypeOfArtwork = "Basic";
                    }
                    if (item.TypeOfArtwork == true && item.Status == Models.Enums.ArtworkStatus.Used)
                    {
                        itemLista.TypeOfArtwork = "Advanced";
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
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);
            var obj = _authorService.FindArtworkById(id);
            var ownerId = obj.OwnerID;

            if (user.OwnerId != ownerId)
                {
                    return RedirectToAction("AccessDenied", "Users");
                }
            
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Artwork artwork)
        {
            var obj = _authorService.FindArtworkById(id);
            if (obj.Status == Models.Enums.ArtworkStatus.Used)
            {
                obj.PublicationCode = artwork.PublicationCode;
                obj.TypeOfArtwork = artwork.TypeOfArtwork;
                _authorService.Update(id, obj);
                return RedirectToAction(nameof(ShowAllCodes), new { id = obj.OwnerID });
            }
                return RedirectToAction(nameof(YouCannotDoThat));

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
            _authorService.InsertAuthor(author);
            _authorService.Pot(author);
           



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {

            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);
          
                if (user.Admin != true)
                {
                    return RedirectToAction("AccessDenied", "Users");
                }
           


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

        public IActionResult YouCannotDoThat()
        {
            return View();
        }




    }
}
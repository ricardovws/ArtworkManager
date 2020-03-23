using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtworkManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ArtworkManager.Services;
using ArtworkManager.Models.ViewModels;

namespace ArtworkManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly ArtworkManagerContext _context;
        private readonly AuthorService _authorservice;

        public UsersController(ArtworkManagerContext context, AuthorService authorservice)
        {
            _context = context;
            _authorservice = authorservice;
        }

       

        // GET: Users
        
        public async Task<IActionResult> Index()
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorservice.FindUserById(idUser);
            if (user.Admin == false)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var authors = _authorservice.ShowAllAuthors();
            var viewModel = new UserFormViewModel { Owners = authors };

            return View(viewModel);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel obj)
        {
            User user = new User();
            
            user.Name = obj.Name;
            user.Login = obj.Login;
            user.Password = obj.Password;
            user.OwnerId = obj.OwnerId;
            user.Admin = obj.Admin;


            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Login,Password,Admin")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Remove(user);
                    await _context.SaveChangesAsync();
                    user.OwnerId = id;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User usuario)
        {
            var userLogado = await _context.User
                .FirstOrDefaultAsync(m => m.Login == usuario.Login && m.Password == usuario.Password);
            if (userLogado == null)
            {
                ViewData["erro"] = "Login e/ou Senha estão incorretos";
                return View();
            }
            //Bota na sessão
            HttpContext.Session.SetString("LoginUsuario", usuario.Login);
            //Bota no cookie
            Response.Cookies.Append("UltimoLogin", DateTime.Now.ToString());
            //Bota na viewbag
            ViewBag.LoginUsuario = usuario.Login;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Usuario"),
                new Claim("IdUsuario", userLogado.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.

                RedirectUri = @"Home/Index"
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }



        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

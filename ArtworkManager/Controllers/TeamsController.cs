using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkManager.Controllers
{
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {

            List<Team> list = new List<Team>();
            list.Add(new Team { Id = 1, Name = "John Deere Luedtke" });
            list.Add(new Team { Id = 2, Name = "N.E.I.V.A." });


            return View(list);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
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

        public IActionResult SimpleReport(int iD, DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            TempData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            TempData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");



            var result = _artworkService.FindByDate(minDate, maxDate);

            foreach (Artwork art in result.ToList())
            {
                var artwork = result.First(b => b.BirthDate >= minDate.Value);
                var artworkfull = artwork;
                result.Remove(artwork);
                var artworkId = artwork.OwnerID;
                artworkfull.Owner = _authorService.FindAuthorById(artworkId);
                artworkfull.Owner.Team = _authorService.FindTeamByAuthorId(artworkId);
                if (artworkfull.TypeOfArtwork == true)
                {
                    artworkfull.Type = Models.Enums.TypeOfArtwork.Advanced;
                }
                else
                {
                    artworkfull.Type = Models.Enums.TypeOfArtwork.Basic;
                }

                result.Add(artworkfull);
            }

            ExportExcel(iD, result, minDate, maxDate);

            return View(result);

        }
        private void ExportExcel(int iD, List<Artwork> result, DateTime? minDate, DateTime? maxDate)
        {


            //Create file's name and a directory.

            var path = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports"));

            var reportName = iD.ToString() + "___" + minDate.ToString() + maxDate.ToString() + "___" + DateTime.Now.ToString();


            //Create a .xls file

            //*******
            try
            {
                var testPath = @"C:\Users\Ricardo\OneDrive\Documentos\-practicing-c-sharp\ArtworkManager\wwwroot\lib\jquery-validation-unobtrusive";

                FileInfo fileInfo = new FileInfo(testPath + "\\LICENSE.txt");
                fileInfo.CopyTo(path + "\\LICENSE________________________.txt");
            }
            catch (IOException)
            {
                System.IO.File.SetAttributes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports") + "\\LICENSE________________________.txt", FileAttributes.Normal);

                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports") + "\\LICENSE________________________.txt");

                //System.IO.Directory.Delete(@"C:\Users\Ricardo\OneDrive\Documentos\-practicing-c-sharp\ArtworkManager\wwwroot\reports", true);


                var testPath = @"C:\Users\Ricardo\OneDrive\Documentos\-practicing-c-sharp\ArtworkManager\wwwroot\lib\jquery-validation-unobtrusive";
                var path1 = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports"));
                FileInfo fileInfo = new FileInfo(testPath + "\\LICENSE.txt");
                fileInfo.CopyTo(path1 + "\\LICENSE________________________.txt");

            }



        }

        public ActionResult DownloadFile(int iD)
        {
            //Download the only file in the directory called "reports"" 



            var path = Path.Combine(
                  Directory.GetCurrentDirectory(),
                  "wwwroot", "reports");

            var files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            var file = files.FirstOrDefault().Split('\\'); //get the file to download as soon as possible
            var _file = file.Last();

            var fullPath = path + "\\" + _file;
            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(fullPath).ToLowerInvariant();
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(fullPath));

          
           // return View();
        }


    }
}
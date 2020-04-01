
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using ArtworkManager.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

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
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);

            if (user.Admin != true)
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            return View();
        }

        public IActionResult SimpleReport(DateTime? minDate, DateTime? maxDate)
        {
            var idUser = Int32.Parse(User.FindFirst("IdUsuario")?.Value);
            var user = _authorService.FindUserById(idUser);

            if (user.Admin != true)
            {
                return RedirectToAction("AccessDenied", "Users");
            }
           

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
           
                ExportExcel(result, minDate, maxDate);
                return View(result);
          
        
        }
        private void ExportExcel(List<Artwork> result, DateTime? minDate, DateTime? maxDate)
        {


            //Create file's name and a directory.

            var path = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports"));

            //var reportName = iD.ToString() + "___" + minDate.Value.DayOfYear.ToString() + maxDate.Value.DayOfYear.ToString() + "___";

            var reportName = "Report___.xlsx";


            //Create a .xls file

            //*******

            //ExcelPackage
            

            using (var excelPackage = new ExcelPackage(new FileInfo("MyWorkbook.xlsx")))
            {
                excelPackage.Workbook.Properties.Author = "Artwork Manager"; 
                excelPackage.Workbook.Properties.Title = "The Report"; 

                // Below I create a file
                var sheet = excelPackage.Workbook.Worksheets.Add("###");
                sheet.Name = "###";



                //Header
                //
                var i = 1;
                var titles = new String[] { "Name", "User", "Code", "BirthDate", "Type", "PublicationCode" }; // Header titles
                foreach (var title in titles)
                {
                    sheet.Cells[1, i++].Value = title;
                }
                sheet.Cells["A1:F1"].Style.Font.Bold = true; // turns header bold

                //objects


                List<String[]> listToExport = new List<string[]>();

             

                foreach (var line in result)
                {
                    var finalList = new String[] { line.Owner.Name.ToString(), line.Owner.User.ToString(), line.Code.ToString()
                        , line.BirthDate.ToString("dd/MM/yyyy HH:mm:ss")
                        , line.Type.ToString()
                        , line.PublicationCode.ToString()

                    };
                    listToExport.Add(finalList);
                }

                //Values
                i = 2;
                int j = 1;

                var values = listToExport;

                foreach (var value in values)
                {

                    foreach (var val in value)
                    {
                        sheet.Cells[i, j++].Value = val;

                    }
                    i++;
                    j = 1;
                }

                string finalPath = path +"\\"+reportName; //file path
             
                excelPackage.SaveAs(new FileInfo(finalPath));

            }

          
        }
        public ActionResult DownloadFile()
        {
            //Download the only file in the directory called "reports"" 



            var path = Path.Combine(
                  Directory.GetCurrentDirectory(),
                  "wwwroot", "reports");

            var files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            var file = files.FirstOrDefault().Split('\\');
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
            

        }
    }
}

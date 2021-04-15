using GameCo.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameCo.Web.Controllers.ExtendedLogic;
using System.IO.Compression;

namespace GameCo.Web.Controllers
{

    public class FileController : Controller
    {
        private const string folderToUpload = "UploadedFiles";
        private const string folderToUnzip = "ExtractZipFolder";
        private string filePath;
        private bool isUploaded;
        private readonly IFileProvider fileProvider;

        public FileController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }


        [HttpGet]
        public async Task<IActionResult> UnityData(string filePath)
        {
            
            try
            {
                filePath = @"C:\Users\GOGARSKY\Desktop\BinaryTest.txt";
                string[] lines = System.IO.File.ReadAllLines(filePath);

                List<string> linesToBinary = new List<string>();              

                foreach (var line in lines)
                {
                    //GOD BLESS OOP
                    string lineToBinary =  GameCo.Web.Controllers.ExtendedLogic.ToBinary.ToBinaryClass(line, false);
                   
                   
                    linesToBinary.Add(lineToBinary);

                }
                System.IO.File.WriteAllLines(@"C:\Users\GOGARSKY\Desktop\WriteFileStream.txt",  linesToBinary);
                //TOO LAZY TO MAKE VIEW - 4 am vibes
                Console.WriteLine(string.Join(',', linesToBinary));
                 
            }

            catch (Exception)
            {
                throw new FileNotFoundException();
            }

            return Redirect("~/Home");
        }

        [HttpGet]
        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile someFile)
        {
            await Upload(someFile);
            TempData["msg"] = "The selected file was successfully uploaded";
            return View();
        }

        public async Task<bool> Upload(IFormFile someFile)
        {
            filePath = "";
            isUploaded = false;

            try
            {
                if (someFile.Length > 0)
                {
                    string fileName = Path.GetFileName(someFile.FileName);
                    filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), folderToUpload));


                    //DO NOT FORGET TO CLOSE THE STREAM!!!
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await someFile.CopyToAsync(fileStream);
                        fileStream.Close();
                    }
                    isUploaded = true;
                }

                else
                {
                    isUploaded = false;
                }
            }
            catch (Exception)
            {

                throw new FileNotFoundException();
            }

            return isUploaded;
        }

        public IActionResult ListAllFiles()
        {
            var model = new FilesViewModel();
            foreach (var file in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(new FileDetails { Name = file.Name, Path = file.PhysicalPath });
            }

            return View(model);
        }

        public async Task<IActionResult> Download(string someFile)
        {
            if (someFile == null)
            {
                return View("NotFoundError");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", someFile);
            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                stream.Close();
            }
            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public async Task<IActionResult> Delete(string someFile)
        {
            try
            {
                if (System.IO.File.Exists(Path.Combine(folderToUpload, someFile)))
                {
                    System.IO.File.Delete(Path.Combine(folderToUpload, someFile));
                }
            }
            catch (Exception)
            {
                throw new FileNotFoundException();
            }

            return Redirect("~/Home");
        }

      
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}

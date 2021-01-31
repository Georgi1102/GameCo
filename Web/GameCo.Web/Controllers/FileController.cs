using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameCo.Web.Controllers
{
    
    public class FileController : Controller
    {
        private const string folderToUpload = "UploadedFiles";

        private string filePath;
        private bool isUploaded;
        

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
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace GameCo.Web.Controllers
{
    public class UnityDataController : Controller
    {
        
        private IWebHostEnvironment Environment;
       

        public UnityDataController(IWebHostEnvironment environment)
        {
            this.Environment = environment;
        }

        public IActionResult Index(string gameName)
        {
         
            string filePath = Path.Combine(this.Environment.WebRootPath, $"Games/{gameName}/Build/");
            string fileName = "DATA.data";

            // Path.GetFileName($"Games/{gameName}/DATA.data") != fileName

            if (string.IsNullOrEmpty(gameName) || SafeString(gameName) != gameName )
            {
                throw new ArgumentNullException("error");
            }

            else
            {
                string absolutePath = Path.Combine(filePath, fileName);

                byte[] fileBytes = System.IO.File.ReadAllBytes(absolutePath);

                return File(fileBytes, "application/octet-stream", fileName);
            }
            
        }

        private static string SafeString(string str)
        {
             return Regex.Replace(str, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
        }

     
    }
}

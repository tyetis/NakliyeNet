using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TransportationApp.Web.Utils
{
    public static class WebUtils
    {
        public static string SaveFile(IFormFile file, IWebHostEnvironment environment)
        {
            string filePath = $"/uploads/{Guid.NewGuid()}.{file.FileName.Split('.')[1]}";
            using (Stream fileStream = new FileStream($"{environment.WebRootPath}{filePath}", FileMode.Create))
            {
                file.CopyTo(fileStream);
                return filePath;
            }
        }
    }
}

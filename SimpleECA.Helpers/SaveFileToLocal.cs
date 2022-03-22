using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleECA.Helpers
{
    public class SaveFileToLocal: ISaveFileToLocal
    {
        private IHostingEnvironment _environment;

        public SaveFileToLocal(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public List<string> SaveFiles(List<IFormFile> postedFiles)
        {
            string wwwPath = _environment.WebRootPath;
            string contentPath = _environment.ContentRootPath;

            string path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add($"{path}\\{fileName}");
                }
            }
            return uploadedFiles;
        }
    }
}

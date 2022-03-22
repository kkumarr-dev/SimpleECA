using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleECA.Helpers
{
    public interface ISaveFileToLocal
    {
        List<string> SaveFiles(List<IFormFile> postedFiles);
    }
}

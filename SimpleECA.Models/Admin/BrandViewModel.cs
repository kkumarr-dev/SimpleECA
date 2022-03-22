using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models.Admin
{
    public class BrandViewModel
    {
        public int brandid { get; set; }
        public string brandname { get; set; }
        public string branddescription { get; set; }
        public IFormFile brandlogo { get; set; }
        public string brandlogopath { get; set; }
        public bool isactive { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}

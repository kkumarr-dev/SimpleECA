using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models.Admin
{
    public class CategoryViewModel
    {
        public int categoryid { get; set; }
        public string categoryname { get; set; }
        public bool isactive { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}

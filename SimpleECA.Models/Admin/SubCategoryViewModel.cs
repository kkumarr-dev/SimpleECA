using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models.Admin
{
    public class SubCategoryViewModel
    {
        public int subcategoryid { get; set; }
        public string subcategoryname { get; set; }
        public int categoryid { get; set; }
        public string categoryname { get; set; }
        public bool isactive { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public List<CategoryViewModel> CategoryViewModel { get; set; }
    }
}

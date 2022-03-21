﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models
{
    public class ProductViewModel
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public float price { get; set; }
        public float discount { get; set; }
        public string shortdescription { get; set; }
        public string longdescription { get; set; }
        public bool iswishlist { get; set; }
        public bool iscart { get; set; }
        public List<ProductImages> ProductImages { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int subcatId { get; set; }
        public string subcatName { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
    public class ProductImages
    {
        public int productid { get; set; }
        public string imagename { get; set; }
        public string imageurl { get; set; }
        public bool isbanner { get; set; }
        public bool isthumbnail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tblproductmaster")]
    public class TblProductMaster
    {
        [Key]
        public int productid { get; set; }
        public string productname { get; set; }
        public double price { get; set; }
        public double discount { get; set; }
        public bool isactive { get; set; }
        public bool isinoffer { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

    }
}

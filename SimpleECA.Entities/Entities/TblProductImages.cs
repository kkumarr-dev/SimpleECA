using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tblproductimages")]
    public class TblProductImages
    {
        [Key]
        public int imageid { get; set; }
        public int productid { get; set; }
        public string imagename { get; set; }
        public string imageurl { get; set; }
        public bool isactive { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public bool isbanner { get; set; }
        public bool isthumbnail { get; set; }
    }
}

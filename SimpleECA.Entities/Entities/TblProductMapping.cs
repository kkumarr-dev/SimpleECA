using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("TblProductMapping")]
    public class TblProductMapping
    {
        [Key]
        public int mapid { get; set; }
        public int productid { get; set; }
        public int brandid { get; set; }
        public int categoryid { get; set; }
        public int subcatid { get; set; }
        public bool isactive { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

    }
}

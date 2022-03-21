using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tblbrandmaster")]
    public class TblBrandMaster
    {
        [Key]
        public int brandid { get; set; }
        public string brandname { get; set; }
        public string branddescription { get; set; }
        public string? brandlogo { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tblproductdescription")]
    public class TblProductDescription
    {
        [Key]
        public int pdid { get; set; }
        public int productid { get; set; }
        public string shortdescription { get; set; }
        public string longdescription { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}

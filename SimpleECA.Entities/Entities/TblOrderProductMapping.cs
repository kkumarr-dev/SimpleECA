using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tblorderproductmapping")]
    public class TblOrderProductMapping
    {
        [Key]
        public int mappingid { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public bool isremoved { get; set; }
        public DateTime createdat { get; set; }
        public double? price { get; set; }

    }
}

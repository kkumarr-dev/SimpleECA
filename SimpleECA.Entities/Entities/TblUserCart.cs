using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("TblUserCart")]
    public class TblUserCart
    {
        [Key]
        public int cartid { get; set; }
        public int userid { get; set; }
        public int productid { get; set; }
        public DateTime createdat { get; set; }
        public bool isactive { get; set; }
        public bool isordered { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("TblProducts")]
    public class TblProducts
    {
        [Key]
        public int productid { get; set; }
        public int productname { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public int isactive { get; set; }
        public int isinoffer { get; set; }
        public int createdat { get; set; }
        public int updatedat { get; set; }

    }
}

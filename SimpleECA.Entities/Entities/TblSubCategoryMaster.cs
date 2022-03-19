using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("TblSubCategoryMaster")]
    public class TblSubCategoryMaster
    {
        [Key]
        public int subcatid { get; set; }
        public int categoryid { get; set; }
        public int subcategoryname { get; set; }
        public int createdat { get; set; }
        public int updatedat { get; set; }
        public int isactive { get; set; }
    }
}

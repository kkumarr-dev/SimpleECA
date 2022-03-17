using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tblroles")]
    public class TblRoles
    {
        [Key]
        public int roleid { get; set; }
        public string rolename { get; set; }
        public bool isactive { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
    }
}

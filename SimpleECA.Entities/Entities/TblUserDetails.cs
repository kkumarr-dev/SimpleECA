using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tbluserdetails")]
    public class TblUserDetails
    {
        [Key]
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string mobilenumber { get; set; }
        public string rpassword { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public bool isactive { get; set; }
        public int userroleid { get; set; }
    }
}

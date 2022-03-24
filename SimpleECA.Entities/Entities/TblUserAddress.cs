using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("TblUserAddress")]
    public class TblUserAddress
    {
        [Key]
        public int addressid { get; set; }
        public int userid { get; set; }
        public string addressline1 { get; set; }
        public string? addressline2 { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string? contactnumber { get; set; }
        public int pincode { get; set; }
        public bool isremoved { get; set; }
        public bool isactive { get; set; }
        public bool? istemp { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models
{
    public class UserAddressViewModel
    {
        public int userid { get; set; }
        public int addressid { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string contactnumber { get; set; }
        public int pincode { get; set; }
        public bool istemp { get; set; }
    }
}

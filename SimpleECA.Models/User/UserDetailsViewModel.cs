using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models
{
    public class UserDetailsViewModel
    {
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string mobilenumber { get; set; }
        public string rpassword { get; set; }
        public bool isactive { get; set; }
        public int userroleid { get; set; }
    }
}

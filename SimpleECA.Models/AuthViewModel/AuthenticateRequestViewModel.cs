using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleECA.Models
{
    public class AuthenticateRequestViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

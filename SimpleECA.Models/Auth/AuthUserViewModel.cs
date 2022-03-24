using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SimpleECA.Models
{
    public class AuthUserViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RoleName { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}

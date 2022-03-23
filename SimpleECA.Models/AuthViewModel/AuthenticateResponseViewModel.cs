using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models
{
    public class AuthenticateResponseViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }


        public AuthenticateResponseViewModel(AuthUserViewModel user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Username = user.Username;
            Email = user.Email;
            Token = token;
            RoleId = user.RoleId;
        }
    }
}

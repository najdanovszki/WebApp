using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPILibrary.Models
{
    public class UserModel : IUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
}

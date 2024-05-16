using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
    }

    public class UserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}
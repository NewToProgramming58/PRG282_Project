using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2x2_Project.User_Classes
{
    internal class User
    {
        string username;
        string password;

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        override
        public string ToString()
        {
            return $"{Username},{Password}\n";
        }
    }
}

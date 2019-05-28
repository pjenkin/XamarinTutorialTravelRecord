using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecord.Model
{
    // class to represent a user
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User()
        {
            // blank constructor with no parameters
        }

        /// <summary>
        ///  Constructor for User object
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRecommendationsBackend.Models
{
    public class Registration
    {
        public Registration(string email, string name, string surname)
        {
            Email = email;
            Name = name;
            Surname = surname;
        }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
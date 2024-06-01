using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementApp.Data.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string ImageUrl { get; set; }
    }
}

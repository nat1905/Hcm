using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hcm.Core.Database;
using Hcm.Database.Models;

namespace Hcm.Web.Models
{
       
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public string UserId { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Role")]
        public Roles Role { get; set; }

    }
}

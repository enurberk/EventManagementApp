using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.UI.Models.Identity
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Please enter your email !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your email !")]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.UI.Models.Identity
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Name Surname")]
        [Required(ErrorMessage = "Please write your name and surname !")]
        public string NameSurname { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password !")]
        public string Password { get; set; }

        [Display(Name = "Verify Password")]
        [Required(ErrorMessage = "Passwords dont match. Try again")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email !")]
        public string Mail { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter a username !")]
        public string UserName { get; set; }
    }
}

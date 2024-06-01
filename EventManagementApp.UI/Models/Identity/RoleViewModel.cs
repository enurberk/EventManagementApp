using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.UI.Models.Identity
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Please enter role !")]
        public string Name { get; set; }
    }
}

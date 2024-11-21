using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models.ViewModels
{
    public class loginViewModel
  
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Email or User Name is required ")]
        [DisplayName("user Name or Email")]
        public string UserNameOrEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5 characters allowed.  ")]
        [DataType(DataType.Password)]

        public string Password { get; set; } = string.Empty;
    }
}

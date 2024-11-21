using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models.ViewModels
{
    public class RegistrationViewModel
    {
      
      
       
            [Key]
            public int Id { get; set; }
            [Required]
            [MaxLength(50, ErrorMessage = "Max 50 Caractaers allowed ")]
            public string Name { get; set; } = string.Empty;
            [Required]
            [MaxLength(50, ErrorMessage = "Max 50 Caractaers allowed")]
            public string LastName { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.PhoneNumber)]

            public string Tel = string.Empty;
            [Required]
            [MaxLength(20, ErrorMessage = "Max 20 Caractaers allowed ")]
            public string UserName { get; set; } = string.Empty;
           public role Role { get; set; } =new  role { Id = 1, Name = "User" };
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5 characters allowed.  ")]
            [DataType(DataType.Password)]

            public string Password { get; set; } = string.Empty;
            [Required]
            [Compare("Password", ErrorMessage = "Please Confirm your password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = string.Empty;
    }
}



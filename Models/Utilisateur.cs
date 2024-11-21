using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace First_Project.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class Utilisateur
    {
       
        
        [Key]
        public int Id { get;  set; }

        [ForeignKey("role")]
        public int RoleId { get; set; }
        public role role { get; set; }

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
        [Required]

        public string Email { get; set; } = string.Empty;
        [Required]

        public string Password { get; set; } = string.Empty;
        public String ImageUrl { get; set; } = "";

        public List<Commande> Commandes { get; set; } = new List<Commande>();





    }
}

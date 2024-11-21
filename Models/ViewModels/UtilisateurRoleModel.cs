using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models.ViewModels
{
    public class UtilisateurRoleModel
    {
        
        public int IdUser { get;  set; }
        public int RoleId { get;  set; } = 0;
        
        public List<role> Roles { get; set; } =new List<role>();
        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; } = "";
        public  IFormFile file { get; set; }
        public string Imageurl { get; set; }
        public string Name { get; set; } = String.Empty;
        public string UserName {  get; set; }



    }
}

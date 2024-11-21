using System.ComponentModel.DataAnnotations;

namespace First_Project.Models
{
    public class role
    {
       
        [Key]
        public  int Id { get; set; }
        public string Name { get;  set; } = "";

      
    }
}

using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models
{
	public class Contact
	{
		[Key]
		public string idContact { get; set; } = Guid.NewGuid().ToString();
        [Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Subject { get; set; }
		[Required]
        public string message {  get; set; }

        public DateTime Date { get; set; } 
    }
}

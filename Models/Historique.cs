using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models
{
    public class Historique
    {
      
            [Key]
            public int Id { get; set; }

            // Other fields can be added, e.g., for metadata
            public DateTime CreatedAt { get; set; } = DateTime.Now;
             public string Note = String.Empty;
        [Required]
             public int QteChange { get; set; }  // Quantity changed
        [ForeignKey("Stock")]
              public int StockId { get; set; }
              public Stock Stock { get; set; } 
    }
}

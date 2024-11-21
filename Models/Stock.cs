using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        
        
        [Required]
        public int Qte { get; set; }
        public DateTime Date { get; set; }
        
        
        // Collection of Historique entries (one stock can have many historique entries)
        public List<Historique> Historiques { get; set; } = new List<Historique>();
        public Produit Produit { get; set; }
    }
}

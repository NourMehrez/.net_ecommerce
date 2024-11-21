using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models
{
    public class CommandeProduit
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to Commande
        [ForeignKey("Commande")]
        public int CommandeId { get; set; }

        public Commande Commande { get; set; } = new Commande();

        // Foreign key to Produit
        [ForeignKey("Produit")]
        public int ProduitId { get; set; }

        public Produit Produit { get; set; } = new Produit();

        // Quantity of the product in this order
        [Required]
        public int Quantite { get; set; } 
         public int prix { get; set; }
    }
}

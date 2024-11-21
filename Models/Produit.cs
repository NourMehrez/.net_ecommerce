using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models
{
    public class Produit
    {
        [Key]
        public int IdProduit { get; set; }
  

        [Required]
        
        public string LibelleProduit { get; set; } = "";

        [Required]

        public string ImageProduit { get; set; } = "";
        [ForeignKey("Categorie")]
        public int idCategorie { get; set; }
        public int prix {  get; set; }

        public Categorie Categorie { get; set; }=new Categorie();

        // Single stock associated with the product (one-to-one relationship)
        // Foreign key for Stock
        public int? StockId { get; set; } // Nullable if Stock is optional
        public Stock Stock { get; set; } // Navigation property
    }
}

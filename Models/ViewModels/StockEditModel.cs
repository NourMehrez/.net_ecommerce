using System.ComponentModel.DataAnnotations;

namespace First_Project.Models.ViewModels
{
    public class StockEditModel
    {
        public int Id { get; set; }

        [Required]
        public int Qte { get; set; } // Quantité de stock

        public DateTime Date { get; set; } // Date de la dernière modification

        public Produit Produit { get; set; } = new Produit();// Produit lié au stock

        public string Note { get; set; } = String.Empty;
    }
}

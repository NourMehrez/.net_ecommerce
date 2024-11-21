using System.ComponentModel.DataAnnotations;

namespace First_Project.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; } = string.Empty;
    }
  

}

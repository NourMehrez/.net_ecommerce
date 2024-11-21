using System.ComponentModel.DataAnnotations;

namespace First_Project.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Description { get; set; } = "";
        [Required]
        [StringLength(20)]
        public string NomCategorie { get; set; } = "";
        
        ICollection<Produit> ProduitList { get; set;} = new List<Produit>();
}
}

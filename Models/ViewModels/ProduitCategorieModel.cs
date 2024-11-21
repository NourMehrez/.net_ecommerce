using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models.ViewModels
{
    public class ProduitCategorieModel
    {
        
        public int IdProduit { get; set; }

        public string LibelleProduit { get; set; } = "";
        public int prix { get; set; }
        public int idCategorie { get; set; }
        public String ImageUrl { get; set; } = "";
        public IFormFile file { get; set; }
        public ICollection<Categorie> categories { get; set; } = new List<Categorie>();
      
    }
}

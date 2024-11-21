
using First_Project.Models.Data;
using First_Project.Models.repository.Interface;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace First_Project.Models.repository
{
    public class ProduitRepository : IProduitRepository
    {
        private readonly FirstProjectDBContext _db;
        
            
        public ProduitRepository(FirstProjectDBContext context)
        {
            _db = context;
        }

        public void Add(Produit entity)
        {
            // Ajouter le produit et son stock associé
            _db.Products.Add(entity);
            // Sauvegarder les changements dans la base de données
            _db.SaveChanges();
        }
        public Produit? Find(int id)
        {
            return _db.Products
                .Include(p => p.Stock)  // Inclure le stock
                .Include(p => p.Categorie)  // Inclure la catégorie
                .FirstOrDefault(p => p.IdProduit == id);
        }


        public Produit FindProduit(int id) {
            return _db.Products.Include(u=>u.Categorie).FirstOrDefault(p => p.IdProduit == id);
        }

        public void Delete(int id)
        {
            var produit = _db.Products.FirstOrDefault(x => x.IdProduit == id); 
            if (produit!=null) { _db.Products.Remove(produit);
                _db.SaveChanges();
            }
            
            
        }
        public IList<Produit> list()
        {
            return _db.Products
         .Include(p => p.Categorie) // Include the category
         .Include(p=>p.Stock)
         .ToList();
        }

        public void Update(int id, Produit entity)
        {
            var produit = Find(id);
            produit.idCategorie = entity.idCategorie;
            produit.LibelleProduit= entity.LibelleProduit;
            produit.ImageProduit= entity.ImageProduit;
            
        }

        public ICollection<Produit> GetAll(string term)
        {
            var result = _db.Products.Where(b => b.Categorie.NomCategorie.Contains(term)
                     || b.LibelleProduit.Contains(term)).ToList();

            return result;
        }
    }
}

using First_Project.Models.Data;
using First_Project.Models.repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Windows.Input;

namespace First_Project.Models.repository
{
    public class CommandeRepository : ICommandeRepository
    {
        private readonly FirstProjectDBContext _db;
        private readonly IStockRepository _stockRepository;
        private readonly IProduitRepository produitRepository;
        private readonly IRoleRepository RoleDbRepository;
        public CommandeRepository(FirstProjectDBContext db, IStockRepository stockRepository, IProduitRepository produitRepository, IRoleRepository roleDbRepository)
        {
            _db = db;
            _stockRepository = stockRepository;
            this.produitRepository = produitRepository;
            RoleDbRepository = roleDbRepository;
        }

        public void Add(Commande commande, ICollection<CommandeProduit> Produits)
        {
            commande.DateCommande = DateTime.Now;
            // Assurez-vous que l'utilisateur existe et a un RoleId valide
            if (commande.UtilisateurId == null || RoleDbRepository.Find(commande.Utilisateur.RoleId)==null)
            {
                throw new Exception("Invalid user or user role");
            }
            
            _db.Commandes.Add(commande);

            foreach (var detail in Produits)
            {
                // Assurez-vous de ne pas créer un nouveau produit ou stock
                var produit = produitRepository.Find(detail.ProduitId);

                if (produit != null)
                {
                    _stockRepository.UpdateStockCommande(detail.ProduitId, detail.Quantite);
                    detail.CommandeId = commande.Id;
                    detail.Commande = commande;
                    detail.Produit = produit;
                    detail.ProduitId = produit.IdProduit;
                    _db.CommandeProduits.Add(detail);
                }
                else
                {
                    throw new InvalidOperationException("Produit introuvable pour la commande.");
                }
            }

            _db.SaveChanges();
        }


        public Commande? FindById(int id)
        {
            return _db.Commandes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ICollection<Commande>> listAsync()
        {
          return await  _db.Commandes.Include(u=>u.CommandeProduits).Include(u=>u.Utilisateur).ToListAsync();
        }
    }
}

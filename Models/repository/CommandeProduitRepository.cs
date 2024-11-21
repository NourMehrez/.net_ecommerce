using First_Project.Models.Data;
using First_Project.Models.repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace First_Project.Models.repository
{
    public class CommandeProduitRepository : ICommandeProduitRepository
    {
        private readonly FirstProjectDBContext _db;
        private readonly IStockRepository _stockRepository;

        public CommandeProduitRepository(FirstProjectDBContext db, IStockRepository stockRepository)
        {
            _db = db;
            _stockRepository = stockRepository;
        }

        public void Add(CommandeProduit CommandeProduit)
        {
                
                _db.CommandeProduits.Add(CommandeProduit);
         
            _db.SaveChanges();
        }




        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommandeProduit> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommandeProduit> GetByCommandeId(int commandeId)
        {
           
                return _db.CommandeProduits
                               .Include(cp => cp.Produit)
                                .Where(cp => cp.CommandeId == commandeId)// Optionnel : inclut les détails du produit
                               .ToList();
           

        }

        public CommandeProduit GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CommandeProduit commandeProduit)
        {
            throw new NotImplementedException();
        }
    }
}

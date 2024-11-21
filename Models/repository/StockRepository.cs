using First_Project.Models.Data;
using First_Project.Models.repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Models.repository
{
    public class StockRepository : IStockRepository
    {
        private readonly FirstProjectDBContext _db;
        private readonly IHistoriqueRepository _historiqueRepository;
        private readonly IProduitRepository produitRepository;
        public StockRepository(FirstProjectDBContext db, IHistoriqueRepository historiqueRepository, IProduitRepository _produitRepository)
        {
            _db = db;
            this._historiqueRepository=historiqueRepository;
           produitRepository= _produitRepository;
        }

        public void Add(Stock entity)
        {
            _db.Stocks.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var Role = Find(id);

            if (Role != null)
            {   _db.Stocks.Remove(Role);
                _db.SaveChanges(true);
            }
           
        }

        public Stock? Find(int id)
        {
            var stock = _db.Stocks
       .Include(p => p.Produit) 
       .SingleOrDefault(x => x.Id == id);
            return stock;
        }
       

        public async Task<IList<Stock>> list()
        {
            return await _db.Stocks.Include(u => u.Produit)
                .Include(u=>u.Historiques).ToListAsync(); // Utilisez ToListAsync pour une opération asynchrone
        }


        public void UpdateStock(int produitId, int newQuantity, string note)
        {
            var produit = produitRepository.Find(produitId);

            if (produit != null && produit.Stock != null)
            {
                // Add an entry to the historical record
                var historique = new Historique
                {
                    QteChange = newQuantity,
                    CreatedAt = DateTime.Now,
                    Note = note,
                    StockId = produit.Stock.Id,
                    
                };
                _historiqueRepository.Add(historique);

                // Update the stock
                produit.Stock.Qte += newQuantity;
                produit.Stock.Date = DateTime.Now;

                _db.SaveChanges();
            }
        }

      

        public void UpdateStockCommande(int produitId, int quantiteCommandee)
        {
            var produit = produitRepository.Find(produitId);

            if (produit != null && produit.Stock != null)
            {
                if (produit.Stock.Qte >= quantiteCommandee)
                {
                    // Mise à jour du stock
                    produit.Stock.Qte -= quantiteCommandee;
                    produit.Stock.Date = DateTime.Now;

                    // Ajouter une entrée dans l'historique
                    var historique = new Historique
                    {
                        QteChange = -quantiteCommandee,
                        CreatedAt = DateTime.Now,
                        Note = "Commande",
                        StockId = produit.Stock.Id,
                        Stock=produit.Stock
                    };
                    _historiqueRepository.Add(historique);

                    _db.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Quantité en stock insuffisante pour ce produit.");
                }
            }
            else
            {
                throw new InvalidOperationException("Produit ou stock introuvable.");
            }
        }


    }
}

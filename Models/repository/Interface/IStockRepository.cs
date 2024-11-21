namespace First_Project.Models.repository.Interface
{
    public interface IStockRepository
    {
        Task<IList<Stock>> list();
        Stock? Find(int id);
        void Add(Stock entity);
        void Delete(int id);
        void UpdateStock(int produitId, int newQuantity, string note);
        void UpdateStockCommande(int produitId, int quantite);
    }
}

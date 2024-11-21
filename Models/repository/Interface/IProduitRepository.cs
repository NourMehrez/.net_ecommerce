namespace First_Project.Models.repository.Interface
{
    public interface IProduitRepository
    {
        IList<Produit> list();
        Produit? Find(int id);
        void Add(Produit entity);
        void Delete(int id);
        void Update(int id, Produit entity);
        ICollection<Produit> GetAll(String term);
    }
}

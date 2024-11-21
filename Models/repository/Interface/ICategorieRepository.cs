namespace First_Project.Models.repository.Interface
{
    public interface ICategorieRepository
    {
        IList<Categorie> list();
        Categorie? Find(int id);
        void Add(Categorie entity);
        void Delete(int id);
        void Update(int id, Categorie entity);
        ICollection<Categorie> GetAll(String term);
    }
}

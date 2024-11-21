namespace First_Project.Models.repository.Interface
{
    public interface IRoleRepository
    {
        IList<role> list();
        role Find(int id);
        role? FindUser();
        void Edit(int id,role role);
        void Add(role entity);
        void Delete(int id);
    }
}

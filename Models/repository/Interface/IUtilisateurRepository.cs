using First_Project.Models.ViewModels;

namespace First_Project.Models.repository.Interface
{
    public interface IUtilisateurRepository
    {
        IList<Utilisateur> list();
        Utilisateur Find(int id);
        void Add(Utilisateur entity);
        void Delete(int id);
        void Update(int id, Utilisateur entity);
        List<Utilisateur> Search(string term);
        Utilisateur? FindByEmail(string email);
        Utilisateur? login(loginViewModel model);
    }
}

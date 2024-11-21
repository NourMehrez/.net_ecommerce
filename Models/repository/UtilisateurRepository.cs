using System.Data;
using First_Project.Models.repository.Interface;
using First_Project.Models.ViewModels;

namespace First_Project.Models.repository
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        IList<Utilisateur> users;
        public UtilisateurRepository()
        {
            users = new List<Utilisateur>
            {
                new Utilisateur
                {
                    Id = 0,
                    Password="hdbjsdb",
                    Email="njdskh@gmail.com",
                   RoleId=0,
                    ImageUrl="real.JPG"

                }
            };
        }

        public void Add(Utilisateur entity)
        {   
            entity.Id=users.Max(x => x.Id)+1;
            users.Add(entity);
        }

        public void Delete(int id)
        {
            var user = Find(id);
            if (user != null)
            {
                users.Remove(user);
            }
        }

        public Utilisateur? Find(int id)
        {
            var user = users.SingleOrDefault(x => x.Id == id);
            return user;
        }

        public Utilisateur? FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IList<Utilisateur> list()
        {
           return users;
        }

        public Utilisateur? login(loginViewModel model)
        {
            throw new NotImplementedException();
        }

        public List<Utilisateur> Search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Utilisateur entity)
        {
           var user= Find(id);

            if (user != null)
            {
                user.Password = entity.Password;
                user.Email = entity.Email;
                user.RoleId = entity.RoleId;
                user.role.Id = entity.RoleId;
                user.role.Name = entity.role.Name;
                user.ImageUrl = entity.ImageUrl;
                user.UserName = entity.UserName;
                

            }

        }
    }
}

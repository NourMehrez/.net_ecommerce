using First_Project.Models.Data;
using First_Project.Models.repository.Interface;
using First_Project.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models.repository
{

    public class UtilisateurDbRepository : IUtilisateurRepository
    {
        FirstProjectDBContext db;
        public UtilisateurDbRepository(FirstProjectDBContext _db)
        {
            db = _db;
        }

        public void Add(Utilisateur entity)
        {

            db.Users.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = Find(id);

            db.Users.Remove(user);
            db.SaveChanges();

        }

        public Utilisateur Find(int id)
        {
            var user = db.Users.Include(x=>x.role).SingleOrDefault(x => x.Id == id);
            return user;
        }

        public IList<Utilisateur> list()
        {
            return db.Users.Include(u => u.role).Include(u=>u.Commandes).ToList();
        }

        public void Update(int id, Utilisateur entity)
        {

            db.Users.Update(entity);
            db.SaveChanges();



        }
        public List<Utilisateur>? Search(string term)
        {
            var result = db.Users.Include(u => u.role).Where(b => b.role.Name.Contains(term)
                        || b.Email.Contains(term)).ToList();

            return result;
        }
        public Utilisateur? FindByEmail(string NameOremail)
        {
            var result=db.Users.Include(u => u.role).FirstOrDefault(u => u.Email == NameOremail || u.UserName == NameOremail); ;
            return result;
        }
        public Utilisateur? login(loginViewModel model)
        {
            var user = db.Users.Include(u => u.role).FirstOrDefault(u => u.Email == model.UserNameOrEmail || u.UserName == model.UserNameOrEmail);

            return user;
        }
    }
}


using First_Project.Models.Data;
using First_Project.Models.repository.Interface;

namespace First_Project.Models.repository
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly FirstProjectDBContext _db;


        public CategorieRepository(FirstProjectDBContext context)
        {
            _db = context;
        }
        public void Add(Categorie entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
           
        }

        public void Delete(int id)
        {
            var category = Find(id);

            if (category != null)
            {
                _db.Remove(category);
                _db.SaveChanges();
            }
        }

        public Categorie? Find(int id)
        {
            var categorie=_db.Categories.SingleOrDefault(c => c.Id == id);
            return categorie;
        }

        public ICollection<Categorie> GetAll(string term)
        {
            var result = _db.Categories.Where(b => b.NomCategorie.Contains(term)).ToList();


            return result;
        }

        public IList<Categorie> list()
        {
            return _db.Categories.ToList();
        }

        public void Update(int id, Categorie entity)
        {
            var categorie = Find(id);
            categorie.Description = entity.Description;


            
        }
    }
}

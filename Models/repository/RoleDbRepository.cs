using First_Project.Models.Data;
using First_Project.Models.repository.Interface;

namespace First_Project.Models.repository
{

    public class RoleDbRepository : IRoleRepository
        {
        private readonly FirstProjectDBContext _db;
        

            public RoleDbRepository(FirstProjectDBContext context)
            {
              _db=context;
            }

            public void Add(role entity)
            {
            _db.Roles.Add(entity);
            _db.SaveChanges();
            }

            public void Delete(int id)
            {
                var Role = Find(id);

                if (Role != null)
                {
                    _db.Roles.Remove(Role);
                _db.SaveChanges();
            }

            }
        public void Edit(int id,role role) { _db.Roles.Update(role);
            _db.SaveChanges();
        }
            public role? Find(int id)
            {
                var role = _db.Roles.SingleOrDefault(x => x.Id == id);
                return role;
            }
        public role? FindUser()
        {
            var role = _db.Roles.SingleOrDefault(x => x.Name== "User");
            return role;
        }

        public IList<role> list()
            {
                return _db.Roles.ToList();

            }

        }
    }


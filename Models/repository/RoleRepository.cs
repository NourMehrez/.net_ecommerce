
using First_Project.Models.repository.Interface;
using static System.Reflection.Metadata.BlobBuilder;

namespace First_Project.Models.repository
{
    public class RoleRepository : IRoleRepository
    {
        IList<role> Roles;

        public RoleRepository()
        {
            Roles = new List<role>() {
                new role
                {
                    Id=1,
                    Name="User"
                },
                 new role
                {
                    Id=2,
                    Name="Admin"
                }
            };
        }

        public void Add(role entity)
        {
            Roles.Add(entity);
        }
      
        public void Delete(int id)
        {
            var Role = Find(id);

            if (Role != null)
            {
                Roles.Remove(Role);
            }
            
        }

        public void Edit(int id, role role)
        {
            throw new NotImplementedException();
        }

        public role Find(int id)
        {
            var role = Roles.SingleOrDefault(x => x.Id == id);
            return role;
        }

        public role? FindUser()
        {
            throw new NotImplementedException();
        }

        public IList<role> list()
        {
            return Roles;
        }
    }
}

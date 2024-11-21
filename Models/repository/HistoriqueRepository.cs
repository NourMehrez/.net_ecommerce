using First_Project.Models.Data;
using First_Project.Models.repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Models.repository
{
    public class HistoriqueRepository : IHistoriqueRepository
    {
        private readonly FirstProjectDBContext _db;


        public HistoriqueRepository(FirstProjectDBContext context)
        {
            _db = context;
        }

        public  void Add(Historique entity)
        {
            _db.Historique.Add(entity);
            _db.SaveChanges();
        }



        public async Task<IList<Historique>> list()
        {
            return await _db.Historique.ToListAsync();
        }
        public async Task<IList<Historique>> FindListHistoryAsync(int id)
        {
            return await _db.Historique.Include(u=>u.Stock)
                .Where(h => h.StockId == id)
                .ToListAsync();
        }


    }
}

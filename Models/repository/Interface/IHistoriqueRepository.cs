namespace First_Project.Models.repository.Interface
{
    public interface IHistoriqueRepository
    {
        Task<IList<Historique>> list();
       
        void  Add(Historique entity);
        Task<IList<Historique>> FindListHistoryAsync(int id);


    }
}

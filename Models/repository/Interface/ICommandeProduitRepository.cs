namespace First_Project.Models.repository.Interface
{
    public interface ICommandeProduitRepository
    {
        IEnumerable<CommandeProduit> GetAll();
        CommandeProduit GetById(int id);
        void Add(CommandeProduit commandeProduit);
        void Update(CommandeProduit commandeProduit);
        void Delete(int id);
        IEnumerable<CommandeProduit> GetByCommandeId(int commandeId);
    }
}

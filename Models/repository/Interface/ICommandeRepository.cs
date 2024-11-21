using System.Configuration;

namespace First_Project.Models.repository.Interface
{
    public interface ICommandeRepository
    {
        
        void Add(Commande commande, ICollection<CommandeProduit> Produits);
        Commande? FindById(int id);
        Task<ICollection<Commande>> listAsync();


    }
}

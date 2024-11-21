using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace First_Project.Models
{
    public class Commande
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateCommande { get; set; }

        // Foreign key to Utilisateur (One-to-Many relationship: One User can have many Commandes)
        [ForeignKey("Utilisateur")]
        public int UtilisateurId { get; set; }
        public Decimal prix {  get; set; }

        public Utilisateur Utilisateur { get; set; } = new Utilisateur();

        // One-to-Many relationship with CommandeProduit
        public List<CommandeProduit> CommandeProduits { get; set; } = new List<CommandeProduit>();
    }
}

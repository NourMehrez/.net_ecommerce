using Microsoft.EntityFrameworkCore;
using First_Project.Models.ViewModels;
using First_Project.Models.repository;

namespace First_Project.Models.Data
{
    public class FirstProjectDBContext : DbContext
    {
        public FirstProjectDBContext(DbContextOptions<FirstProjectDBContext> options) : base(options)
        {

        }
        public DbSet<role> Roles { get; set; }
        public DbSet<Utilisateur> Users { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Produit> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Historique> Historique { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CommandeProduit> CommandeProduits { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Contact>Contact { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between Commande and Produit via CommandeProduit
            modelBuilder.Entity<CommandeProduit>()
                .HasOne(cp => cp.Commande)
                .WithMany(c => c.CommandeProduits)
                .HasForeignKey(cp => cp.CommandeId);


            modelBuilder.Entity<Produit>()
         .HasOne(p => p.Categorie)
         .WithMany()
         .HasForeignKey(p => p.idCategorie);
            modelBuilder.Entity<Utilisateur>()
       .HasOne(p => p.role)
       .WithMany()
       .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Produit>()
            .HasOne(p => p.Stock)
            .WithOne(s => s.Produit)
            .HasForeignKey<Produit>(p => p.StockId);


            modelBuilder.Entity<Stock>()
        .HasMany(s => s.Historiques)
        .WithOne(h => h.Stock)
        .HasForeignKey(h => h.StockId)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Produit)
                .WithMany()
                .HasForeignKey(sci => sci.ProduitId);





        }
       
    }
}

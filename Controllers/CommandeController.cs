using First_Project.Models.repository;
using First_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using First_Project.Models.repository.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace First_Project.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ICommandeRepository _commandeRepository;
        private readonly ICommandeProduitRepository _commandeProduitRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public CommandeController(ICommandeRepository commandeRepository, ICommandeProduitRepository commandeProduitRepository, ShoppingCart shoppingCart, IUtilisateurRepository utilisateurRepository)
        {
            _commandeRepository = commandeRepository;
            _commandeProduitRepository = commandeProduitRepository;
            _shoppingCart = shoppingCart;
             _utilisateurRepository= utilisateurRepository;
        }


        [HttpPost]
        public IActionResult Checkout()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
                return View();
            }

            string userName = User.Identity.Name;
            var user = _utilisateurRepository.FindByEmail(userName);

            if (user != null)
            {
                ICollection<CommandeProduit> Produits = items.Select(item => new CommandeProduit
                {
                    ProduitId = item.Produit.IdProduit,
                    Quantite = item.Quantity,
                    prix = item.Produit.prix // Assurez-vous que prix est correctement défini
                }).ToList();
                var commande = new Commande
                {
                    UtilisateurId = user.Id,
                    Utilisateur = user,
                    DateCommande = DateTime.Now,
                    prix = _shoppingCart.GetShoppingCartTotal(),


                };
                

                _commandeRepository.Add(commande, Produits);

                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            else
            {
                // Gérer le cas où l'utilisateur n'est pas trouvé
                throw new Exception("User not found.");
            }
        }


        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order!";
            return View();
        }
        public async Task<ActionResult> Index()
        {
            // Utilisation de la méthode asynchrone pour récupérer les commandes
            ICollection<Commande> commandes = await _commandeRepository.listAsync();
            var comm=commandes.OrderByDescending(Item => Item.DateCommande).ToList();
            return View(comm); // Passez les commandes à la vue
        }





    }
}


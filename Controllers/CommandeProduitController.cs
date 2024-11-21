using First_Project.Models.repository.Interface;
using First_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using First_Project.Models.repository;

namespace First_Project.Controllers
{
    public class CommandeProduitController : Controller
    {
        private readonly ICommandeProduitRepository _commandeProduitRepository;
        private readonly ICommandeRepository _commandeRepository;
        public CommandeProduitController(ICommandeProduitRepository commandeProduitRepository, ICommandeRepository commandeRepository)
        {
            _commandeProduitRepository = commandeProduitRepository;
            _commandeRepository = commandeRepository;
        }

        public IActionResult Index(int commandeId)
        {
            var commandeProduits = _commandeProduitRepository.GetByCommandeId(commandeId);
            return View(commandeProduits);
        }

        public IActionResult Details(int id)
        {
            var commandeProduit = _commandeProduitRepository.GetById(id);
            if (commandeProduit == null)
            {
                return NotFound();
            }
            return View(commandeProduit);
        }

        public IActionResult Create(int commandeId)
        {
            // Crée un nouveau modèle CommandeProduit avec la commandeId préremplie
            var model = new CommandeProduit { CommandeId = commandeId };
            return View(model);
        }


        [HttpPost]
        public IActionResult Create(CommandeProduit commandeProduit)
        {
            if (!ModelState.IsValid)
            {
                // La validation a échoué, renvoyer le modèle avec les erreurs à la vue
                return View(commandeProduit);
            }

            try
            {
                // Assurez-vous que la commande existe avant d'ajouter le produit
                var commande = _commandeRepository.FindById(commandeProduit.CommandeId);
                if (commande == null)
                {
                    ModelState.AddModelError("", "Commande not found.");
                    return View(commandeProduit);
                }

                // Ajouter le produit de commande
                _commandeProduitRepository.Add(commandeProduit);

                // Rediriger vers la vue de l'index avec l'ID de la commande
                return RedirectToAction("Index", new { commandeId = commandeProduit.CommandeId });
            }
            catch (Exception ex)
            {
                // En cas d'erreur, afficher l'erreur dans la vue
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(commandeProduit);
            }
        }



        public IActionResult Edit(int id)
        {
            var commandeProduit = _commandeProduitRepository.GetById(id);
            if (commandeProduit == null)
            {
                return NotFound();
            }
            return View(commandeProduit);
        }

        [HttpPost]
        public IActionResult Edit(CommandeProduit commandeProduit)
        {
            if (ModelState.IsValid)
            {
                _commandeProduitRepository.Update(commandeProduit);
                return RedirectToAction("Index", new { commandeId = commandeProduit.CommandeId });
            }
            return View(commandeProduit);
        }

        public IActionResult Delete(int id)
        {
            var commandeProduit = _commandeProduitRepository.GetById(id);
            if (commandeProduit == null)
            {
                return NotFound();
            }
            return View(commandeProduit);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var commandeProduit = _commandeProduitRepository.GetById(id);
            _commandeProduitRepository.Delete(id);
            return RedirectToAction("Index", new { commandeId = commandeProduit.CommandeId });
        }
    }
}


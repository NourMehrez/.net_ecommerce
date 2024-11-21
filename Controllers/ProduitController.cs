using First_Project.Models;
using First_Project.Models.repository;
using First_Project.Models.repository.Interface;
using First_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace First_Project.Controllers
{

    public class ProduitController : Controller
    {
        private readonly IProduitRepository produitRepository;
        private readonly ICategorieRepository CategorieRepository;
        private readonly IStockRepository stockRepository;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;
        [Obsolete]
        public ProduitController(IProduitRepository produitRepository, IStockRepository stockRepository, ICategorieRepository CategorieRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            this.produitRepository = produitRepository;
            this.hosting = hosting;
            this.CategorieRepository = CategorieRepository;
            this.stockRepository = stockRepository;
        }

        // GET: ProduitController
      
        public ActionResult Index( )
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            var products = produitRepository.list();
            return View(products);
        }

        public ActionResult Stock()
        {
            var products = produitRepository.list();
            return View(products);
        }

        // GET: ProduitController/Details/5
        public ActionResult Details(int id)
        {var products = produitRepository.Find(id);
            return View(products);
        }

        // GET: ProduitController/Create
        public ActionResult Create()
        {
            var model = new ProduitCategorieModel
            {
                categories = CategorieRepository.list().ToList(),
            };
            return View(model);

        }

        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProduitCategorieModel model)
        {
            try
            {
                string fileName = string.Empty;
                if (model.file != null)
                {
                    String uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    fileName = model.file.FileName;
                    string fullpath = Path.Combine(uploads, fileName);
                    model.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                var categorie = CategorieRepository.Find(model.idCategorie);
                model.ImageUrl=fileName;


                Stock stock = new Stock
                {
                    Qte = 0,
                    Date = DateTime.Now
                };
                stockRepository.Add(stock);
                Produit Prod = new Produit
                {
                    IdProduit = model.IdProduit,
                    idCategorie = model.idCategorie,
                    Categorie = categorie,
                    LibelleProduit = model.LibelleProduit,
                    ImageProduit = fileName,
                     StockId = stock.Id,
                     prix=model.prix

                };

                produitRepository.Add(Prod);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer le modèle initial avec les données
                ModelState.AddModelError("", "An error occurred while creating the product. Please try again.");
                model.categories = CategorieRepository.list().ToList(); // Recharger la liste des catégories
                return View(model); // Retourner le modèle au lieu de l'exception
            }

    
        }

        public ActionResult Edit(int id)
        {
            var produit = produitRepository.Find(id);
            if (produit == null)
            {
                return NotFound();
            }

            var model = new ProduitCategorieModel
            {
                IdProduit = produit.IdProduit,
                idCategorie = produit.idCategorie,
                LibelleProduit = produit.LibelleProduit,
                ImageUrl = produit.ImageProduit,
                prix = produit.prix,
                categories = CategorieRepository.list().ToList()
            };

            return View(model);
        }
        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProduitCategorieModel model)
        {
            try
            {
                var produit = produitRepository.Find(id);
                if (produit == null)
                {
                    return NotFound();
                }

                if (model.file != null)
                {
                    string fileName = model.file.FileName;
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    string fullpath = Path.Combine(uploads, fileName);
                    model.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                    produit.ImageProduit = fileName;
                }

                produit.idCategorie = model.idCategorie;
                produit.LibelleProduit = model.LibelleProduit;
                produit.prix = model.prix;

                produitRepository.Update(id, produit);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while editing the product. Please try again.");
                model.categories = CategorieRepository.list().ToList(); // Recharger la liste des catégories
                return View(model);
            }
        }

        // GET: ProduitController/Delete/5
        public ActionResult Delete(int id)
        {
            var produit = produitRepository.Find(id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // POST: ProduitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                produitRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the product. Please try again.");
                return View();
            }
        }


    }
}

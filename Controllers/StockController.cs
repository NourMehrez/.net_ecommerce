using First_Project.Models;
using First_Project.Models.repository.Interface;
using First_Project.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository stockRepository;
        private readonly IHistoriqueRepository _historiqueRepository;

        public StockController(IStockRepository stockRepository, IHistoriqueRepository historiqueRepository)
        {
            this.stockRepository = stockRepository;
          _historiqueRepository = historiqueRepository;
        }

        // GET: StockController
        public async Task<ActionResult> Index()
        {
            var stocks = await stockRepository.list(); // Assurez-vous que list() est une méthode asynchrone
            return View(stocks); // Passez les stocks à la vue
        }

        // GET: StockController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StockController/Edit/5
        public ActionResult Edit(int id)
        {
            var stock = stockRepository.Find(id);
           

            var produit = stock.Produit;
            // Charger le modèle pour l'édition, inclure les historiques si nécessaire
            var model = new StockEditModel
            {
                Id = stock.Id,
                Qte = stock.Qte,
                Date = stock.Date,
                Produit = produit,
               
            };
            return View(model);
           
        }

        // POST: StockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StockEditModel model)
        {
           
            try
                {if (model != null)
                {
                    var stock = stockRepository.Find(id);
                    stockRepository.UpdateStock(stock.Produit.IdProduit, model.Qte, model.Note);
                    return RedirectToAction(nameof(Index));
                }
                else{return View(Index);}
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the stock.");
                return View(model); 
                }
            

           


        }

        // GET: StockController/Delete/5
        public ActionResult Delete(int id)
        {   
            return View();
        }

        // POST: StockController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Historique(int id)
        {
            // Récupération des éléments d'historique associés à l'ID donné
            var history = await _historiqueRepository.FindListHistoryAsync(id);

            // Tri des éléments par date de création dans l'ordre décroissant
            var sortedHistory = history.OrderByDescending(item => item.CreatedAt).ToList();
            return View(sortedHistory); // Passez l'historique à la vue
        }


    }
}

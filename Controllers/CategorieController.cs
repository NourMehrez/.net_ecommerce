using First_Project.Models;
using First_Project.Models.repository;
using First_Project.Models.repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Project.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ICategorieRepository categorieRepository;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;
        [Obsolete]
        public CategorieController(ICategorieRepository repository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            categorieRepository = repository;
            this.hosting = hosting;
        }

        // GET: CategorieController
        public ActionResult Index()
        {var categories=categorieRepository.list();
            return View(categories);
        }

        // GET: CategorieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategorieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorie categorie)
        {
            try
            {
                categorieRepository.Add(categorie);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategorieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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


        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categorieRepository.Find(id);
            if (category == null)
            {
                return NotFound(); // Handle the case where the category is not found
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Categorie category)
        {
            try
            {
                categorieRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Log the error or handle it accordingly
                return View(category); // Return the view with the same model to handle errors
            }
        }



    }
}

using First_Project.Models;
using First_Project.Models.repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace First_Project.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository roleRepository;
        List<role> roles;
        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }


        // GET: RoleController
        public ActionResult Index()
        {var Roles=roleRepository.list();
            return View(Roles);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            var role = roleRepository.Find(id);
            return View(role);
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
           
            return View();
            
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(role role)
        {
            try
            {
                roleRepository.Add(role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public ActionResult Edit(int id)
        {
        
            return View();
        }

        // POST: RoleController/Edit/5
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
            var role = roleRepository.Find(id);
            return View(role);
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, role role)
        {
            try
            {
                roleRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

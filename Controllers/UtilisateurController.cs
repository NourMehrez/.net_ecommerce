using First_Project.Models;
using First_Project.Models.repository;
using First_Project.Models.repository.Interface;
using First_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IO;
using System.Security.Claims;

namespace First_Project.Controllers
{
    public class UtilisateurController : Controller
    { private readonly IUtilisateurRepository utilisateurRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ICommandeRepository CommandeRepository;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;

        [Obsolete]
        public UtilisateurController(IUtilisateurRepository utilisateurRepository, IRoleRepository roleRepository, ICommandeRepository CommandeRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        { this.utilisateurRepository = utilisateurRepository;
            this.roleRepository = roleRepository;
            this.hosting = hosting;
            this.CommandeRepository=CommandeRepository;
        }

        // GET: UtilisateurController
        public ActionResult Index()
        { var users = utilisateurRepository.list();
            return View(users);
        }

        // GET: UtilisateurController/Details/5
        public ActionResult Details(int id)
        { var user = utilisateurRepository.Find(id);
         
            var Commandes =CommandeRepository.listAsync();
          
            return View(user);
        }

        // GET: UtilisateurController/Create
        public ActionResult Create(int id)
        {
            var model = new UtilisateurRoleModel
            {
                Roles = roleRepository.list().ToList(),
            };
            return View(model);
        }

        // POST: UtilisateurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(UtilisateurRoleModel model)
        {
            try
            {
                string fileName = string.Empty;
                if (model.file != null) {
                    String uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    fileName = model.file.FileName;
                    string fullpath = Path.Combine(uploads, fileName);
                    model.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                var role = roleRepository.Find(model.RoleId);
                if (role == null)
                {
                    ModelState.AddModelError("", "Role not found");
                    return View(model);
                }

                Utilisateur user = new Utilisateur
                {
                    Id = model.IdUser,
                    RoleId = model.RoleId,
                    role = role,
                    Email = model.Email,
                    Password = model.Password,
                    ImageUrl = fileName,
                    Name=model.Name,
                    UserName = model.UserName,
                   
                };
                utilisateurRepository.Add(user);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: UtilisateurController/Edit/5
        public ActionResult Edit(int id)
        {
            var user = utilisateurRepository.Find(id);
            if (user != null)
            {
                var model = new UtilisateurRoleModel
                {
                    IdUser = user.Id,
                    RoleId = user.RoleId,
                    Email = user.Email,
                    Password = user.Password,
                    Roles = roleRepository.list().ToList(),
                    Imageurl = user.ImageUrl,
                    Name=user.Name,
                    UserName=user.UserName
                };
                return View(model);
            } else { return View(); }
        }




        // POST: UtilisateurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(UtilisateurRoleModel model)
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
                var role = roleRepository.Find(model.RoleId);
                model.RoleId = role.Id;
                
                // Trouver le rôle correspondant
                var _role = roleRepository.Find(model.RoleId);
                Utilisateur uti = utilisateurRepository.FindByEmail(model.Email);
                uti.ImageUrl = fileName;
                uti.UserName=model.UserName;
                utilisateurRepository.Update(model.IdUser,uti );
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // En cas d'erreur, revenir à la vue avec le modèle
                ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
                return View(model);
            }
        }

        // GET: UtilisateurController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = utilisateurRepository.Find(id);
            return View(user);
        }

        // POST: UtilisateurController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id, Utilisateur user)
        {
            try
            {
                utilisateurRepository.Delete(id);



                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }


         
           

        }
        public string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;

        }
        public ActionResult Search(String term)
        {
            var result = utilisateurRepository.Search(term);
            return View("Index", result);
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)

        {
            var role = roleRepository.Find(model.Role.Id);
            if (ModelState.IsValid)
            {
                Utilisateur account = new Utilisateur();
                account.Email = model.Email;
                account.Password = model.Password;
                account.LastName = model.LastName;
                account.UserName = model.UserName;
                account.role = role;
                account.RoleId = role.Id;
                try
                {
                    utilisateurRepository.Add(account);
                   

                    ModelState.Clear();
                    ViewBag.Message = $"{account.Name} registred successfuly.please login";
                    return View();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "please enter unoque email or password");
                    return View(model);
                }

            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(loginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = utilisateurRepository.login(model);

                if (user != null && user.Password == model.Password)
                {
                    // Création des claims
                    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Ajout de l'ID utilisateur
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("ImageUrl", user.ImageUrl ?? string.Empty),
        new Claim("Id", user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.role.Name), // Utilisation d'un rôle par défaut si `role` est null
        // Ajouter d'autres claims selon vos besoins
    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Produit");
                }
                else
                {
                    ModelState.AddModelError("", "please enter unique email or password");
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();


        }



    }
}


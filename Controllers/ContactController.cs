using First_Project.Models.repository.Interface;
using First_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Project.Controllers
{
    public class ContactController : Controller
    {
       
            private readonly IContactRepository _contactRepository;

            public ContactController(IContactRepository contactRepository)
            {
                _contactRepository = contactRepository;
            }

            // GET: Contact
            public IActionResult Index()
            {
                var contacts = _contactRepository.List();
                return View(contacts);
            }

            // GET: Contact/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Contact/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Contact contact)
            {
                if (ModelState.IsValid)
            {
                contact.idContact = Guid.NewGuid().ToString();
                    contact.Date = DateTime.Now; // Set the current date and time
                    _contactRepository.addContact(contact);
                    return RedirectToAction(nameof(Index));
                }
                return View(contact);
            }

            // GET: Contact/Details/{id}
            public IActionResult Details(string id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var contact = _contactRepository.List().FirstOrDefault(c => c.idContact == id);
                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
        }

        
}

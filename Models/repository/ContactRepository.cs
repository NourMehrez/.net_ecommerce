using First_Project.Models.Data;
using First_Project.Models.repository.Interface;

namespace First_Project.Models.repository
{
	public class ContactRepository : IContactRepository
	{
		private readonly FirstProjectDBContext _db;

		public ContactRepository(FirstProjectDBContext db)
		{
			_db = db;
		}

		public void addContact(Contact contact) { 
		
		_db.Contact.Add(contact);
			_db.SaveChanges();
		}
		public IList<Contact> List() { 
		return _db.Contact.ToList();
		}
	}
}

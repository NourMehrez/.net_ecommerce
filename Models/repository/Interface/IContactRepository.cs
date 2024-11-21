namespace First_Project.Models.repository.Interface
{
	public interface IContactRepository
	{
		void addContact(Contact contact);
		IList<Contact> List();
	}
}

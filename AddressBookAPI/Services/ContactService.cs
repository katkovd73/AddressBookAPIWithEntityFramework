using AddressBookAPI.DTOs;
using AddressBookAPI.Mappers;
using AddressBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly MyMapper _mymapper;
        private readonly dbcontext _dbcontext;

        public ContactService(MyMapper mymapper, dbcontext context)
        {
            _mymapper = mymapper;
            _dbcontext = context;
        }

        public async Task<List<ContactDTO>> GetAllContacts()
        {
            var contacts = await _dbcontext.Contacts.ToListAsync();

            return _mymapper.ToContactDTOList(contacts);
        }

        public async Task<ContactDTO> GetContactById(int id)
        {
            var contact = await _dbcontext.Contacts.FirstOrDefaultAsync(contact => contact.Id == id);

            if (contact != null)
            {
                return _mymapper.ToContactDTO(contact);
            }

            return null;
        }

        public async Task<ContactDTO> CreateContact(ContactDTO newContact)
        {
            var contact = new Contact();
            contact.FirstName = newContact.FirstName;
            contact.LastName = newContact.LastName;
            contact.Phone = newContact.PhoneNumber;
            contact.Address = newContact.Address;

            _dbcontext.Contacts.Add(contact);
            await _dbcontext.SaveChangesAsync();

            return _mymapper.ToContactDTO(contact);
        }

        public async Task<ContactDTO> UpdateContact(ContactDTO updateContact)
        {
            var contact =await _dbcontext.Contacts.FirstOrDefaultAsync(contact => contact.Id == updateContact.Id);
            if (contact != null)
            {
                contact.FirstName = updateContact.FirstName;
                contact.LastName = updateContact.LastName;
                contact.Phone = updateContact.PhoneNumber;
                contact.Address = updateContact.Address;

                _dbcontext.Contacts.Update(contact);
                await _dbcontext.SaveChangesAsync();

                return _mymapper.ToContactDTO(contact);
            }

            return null;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = _dbcontext.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (contact == null)
            {
                return false;
            }

            _dbcontext.Contacts.Remove(contact);
            await _dbcontext.SaveChangesAsync();

            return true;
        }

    }
}

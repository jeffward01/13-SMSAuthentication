using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApp.Classes
{
    class ContactService
    {
        //Properties
        public static ObservableCollection<Contact> Contacts = new ObservableCollection<Contact>();
        public int IdCount = Contacts.Count + 1;

        //Methods
        public static Contact GetById(int id)
        {
            foreach (Contact c in Contacts)
            {
                if (c.Id == id)
                    return c;
            }
            return null;
        }

        public static void Delete(Contact c)
        {
            if (c != null)
            {
                Contacts.Remove(c);
            }
        }

        public static void Add(Contact e)
        {
            if (e != null)
            {
                e.Id = ContactService.Contacts.Count + 1;
                Contacts.Add(e);
            }
        }      
    }
}

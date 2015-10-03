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
        //Added Category
        public string Category;


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

        
     /*


       // Working on dispalaying items by category
        public static IEnumerable<List<Contact>> Split(this IEnumerable<Contact> source, string SearchString)
        {
            List<Contact> Contacts = new List<Contact>();
            foreach (var item in source)
            {
                if(Contacts.Count > 0 && item.Category == SearchString)
                {
                    yield return Contacts;
                    Contacts = new List<Contact>();
                }
                Contacts.Add(item);
            }
            if(Contacts.Count > 0)
            {
                yield return Contacts;
            }
        }


        */
    }
}

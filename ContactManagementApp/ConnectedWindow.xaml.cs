using ContactManagementApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactManagementApp
{
    /// <summary>
    /// Interaction logic for ConnectedWindow.xaml
    /// </summary>
    public partial class ConnectedWindow : Window
    {
        public ConnectedWindow()
        {
            InitializeComponent();
        }

        //Properties
        //Declare new contact
        private Contact currenctContact;



        //Prepare Entry to be saved
        public void LoadEntry(Contact entry)
        {
            entry = currenctContact;

            entry.Id = ContactService.Contacts.Count + 1;
            entry.FirstName = textBox_FirstName.Text;
            entry.LastName = textBox_LastName.Text;
            entry.EmailAddress = textBox_Email.Text;
            entry.TelephoneNumber = textBox_TelephoneNumber.Text;
            entry.Address1 = textBox_Address1.Text;
            entry.Address2 = textBox_Address2.Text;
            entry.City = textBox_City.Text;
            entry.Zip = textBox_Zip.Text;
            entry.Birthdate = DateTime.Parse(textBox_DOB.Text);
        }

        //Save Button Click
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            LoadEntry(currenctContact);
           SaveEntry(currenctContact);

        }

        private void SaveEntry(Contact entry)
        {
            entry = currenctContact;


            //Add validation later to not rewrite on ID's
            ContactService.Add(entry);
        }
    }
}

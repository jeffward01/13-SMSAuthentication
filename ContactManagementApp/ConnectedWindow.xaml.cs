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
        private Contact currentContact;
        public bool IsEditMode = true;

       
       public void LoadEntry(Contact entry)
        {
           //Save instance to class Varible
                currentContact = entry;

            //Load user interface to entry that is passed in
            textBox_FirstName.Text = entry.FirstName;
            textBox_LastName.Text = entry.LastName;
            textBox_Address1.Text = entry.Address1;
            textBox_Address2.Text = entry.Address2;
            textBox_City.Text = entry.City;
            textBox_Email.Text = entry.EmailAddress;
            textBox_State.Text = entry.State;
            textBox_TelephoneNumber.Text = entry.State;
            textBox_Zip.Text = entry.State;
            

            textBox_DOB.Text = entry.Birthdate.ToShortDateString();
        }

        public void saveEntry(Contact entry)
        {
            entry = currentContact;

         

                 
        }

        //Save Button Click
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            
            SaveEntry(currentContact);

        }

        private void SaveEntry(Contact entry)
        {
            // string date = DateTime.Parse(textBox_DOB.Text).ToShortDateString();



            currentContact = entry;

            entry.Address1 = textBox_Address1.Text;
            entry.Address2 = textBox_Address2.Text;
            entry.Birthdate = DateTime.Parse(textBox_DOB.Text);
            entry.City = textBox_City.Text;
            entry.EmailAddress = textBox_Email.Text;
            entry.FirstName = textBox_FirstName.Text;
            entry.LastName = textBox_LastName.Text;
            entry.State = textBox_State.Text;
            entry.TelephoneNumber = textBox_TelephoneNumber.Text;
            entry.Zip = textBox_Zip.Text;



            //Add validation later to not rewrite on ID's
            if (IsEditMode == true)
            {
                // get a reference to the main window
                var mainwindow = (MainWindow)Owner;


                // trigger a refresh
                mainwindow.dataGrid_ContactList.Items.Refresh();
            }
            else
            {
                ContactService.Add(entry);
            }
            

            //Close Window
            Close();

        }
    }
}

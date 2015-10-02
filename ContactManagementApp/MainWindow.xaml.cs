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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Constructor
        public MainWindow()
        {
            InitializeComponent();

            //Data Grid
            dataGrid_ContactList.ItemsSource = ContactService.Contacts;

            //Initialize Load name in menu bar
           //// Label_fileName.Content = fileName;
        }

        //Properties
        public string fileName = "Untitled";
        public string filePath;
        private Contact ContactList;


        //Methods
        public void Button_AddContact(object sender, RoutedEventArgs e)
        {
            //Open Window
            var window = new ConnectedWindow { Owner = this };

            window.IsEditMode = false;

            //Load Entry here optional
            Contact newContact = new Contact();

            window.LoadEntry(newContact);

            //Open Dialog
            window.ShowDialog();
        }

        //Double Click Hanlder || Turned Off
        private void doubleClickDataGrid(object sender, SelectionChangedEventArgs e)
        {
            doubleClickEvent();
        }

        //Edit Selected Content
        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            doubleClickEvent();
        }
        public void doubleClickEvent()
        {
            try
            {
                //Cast
                //Create a refrence Object, save the refrence object as the selected item
                Contact selected = (Contact)dataGrid_ContactList.SelectedItem;

                //Open Window
                var window = new ConnectedWindow { Owner = this };
                window.IsEditMode = true;
                window.LoadEntry(selected);
                window.ShowDialog();
            }
            catch(Exception)
            {
                MessageBox.Show("Please ensure that you have selected a contact to edit.  Contact System Admin of problem persists");
            }
        }
    }
}

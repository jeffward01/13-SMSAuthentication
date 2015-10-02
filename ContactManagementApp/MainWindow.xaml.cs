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

            //Add new contact || Edit mode turned off
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


        //Edit mode turned on
        //Open edit mode window
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

            //If no selection is made, or error occurs...
            catch(Exception)
            {
                MessageBox.Show("Please ensure that you have selected a contact to edit.  Contact System Admin of problem persists");
            }
        }



        /// <summary>
        /// Menu Click Handlers below
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_MenuItem_New_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainPage_MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainPage_MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainPage_MenuItem_SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainPage_MenuItem_Print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainPage_MenutItem_Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

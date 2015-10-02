using ContactManagementApp.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void Mainpage_DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Cast
                //Create a refrence Object, save the refrence object as the selected item
                Contact selected = (Contact)dataGrid_ContactList.SelectedItem;

                if (selected == null)
                {
                    MessageBox.Show("Please select a contact to delete from the grid");
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Are you sure you want to Erase this entry?", "Erase Entry", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    ContactService.Delete(selected);
                }

                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Make sure you have selected an Item.  For any other errors contact System Admin");
            }
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

        //Experimental Open Method
        private void MainPage_MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {

            //OPen open file dialog
            OpenFileDialog dialog = new OpenFileDialog();

            bool? openFile = dialog.ShowDialog();

            if(openFile == true)
            {
                //Get the name of the file selected by user
                fileName = dialog.FileName;

                //open the file and read the contents into a string
                string myFile = File.ReadAllText(fileName);

                try
                {
                    //Save file from a JSON object into a ContactService Class
                    Contact newContactList = JsonConvert.DeserializeObject<Contact>(myFile);

                    //Set newContactList as THE new ContactList
                    ContactList = newContactList;

                    //Refresh grid
                    dataGrid_ContactList.ItemsSource = null;
                }
                catch
                {
                    MessageBox.Show("error opening file, please contact sysytem ADMIN");

                }

            }


        }

    

        private void MainPage_MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            //If file name has not been rewritten
           if(fileName == "Untitled")
            {
                OpenSaveAs();
            }

            try
            {

                //Check contents of FileName
                string filenameConetents = File.ReadAllText(filePath);

                //check contents of current contactlist
                string myConents = Newtonsoft.Json.JsonConvert.SerializeObject(ContactList, Formatting.Indented);

                if (filenameConetents == myConents)
                {
                    return;
                }
                File.WriteAllText(filePath, myConents);
            }
           catch(Exception)
            {
                MessageBox.Show("Error occured while saving.  Please use SAVE AS and contact Sysytem Admin");
            }
        }

        //Menu Item || Save AS
        private void MainPage_MenuItem_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            OpenSaveAs();
        }

        //Menu Item || Printer
        private void MainPage_MenuItem_Print_Click(object sender, RoutedEventArgs e)
        {
            SendToPrint();
        }

        //Menu Item Exit Button
        private void MainPage_MenutItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Quit?", "Exit Prompt", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            return;
        }


        //Print Function
        public void SendToPrint()
        {

            try
            {
                PrintDialog printdialog = new PrintDialog();
                printdialog.ShowDialog();
            }
            catch(Exception)
            {
                MessageBox.Show("An error has occured, please contact system admin");
            }
        }
      

        //Open SaveAs Code
        public void OpenSaveAs()
        {
            //Opens Save File Dialog box
            SaveFileDialog savefiledialog1 = new SaveFileDialog();

            //only allows .txt files to be saved
            savefiledialog1.Filter = "Text Files|*.txt";

            //User Choice to act with save Dialog
            bool? saveFileAs = savefiledialog1.ShowDialog();

            //If User does press Save
            if(saveFileAs == true)
            {
                //File name/Path of user choice
                string fileNameNew = savefiledialog1.FileName;

                //Save file Path ro varible
                filePath = fileNameNew;

                //change label of top bar
                //Example of instatnation
                FileInfo fi = new FileInfo(savefiledialog1.FileName);
                string text = fi.Name;


                //Save Contents to a string
                string myContents = Newtonsoft.Json.JsonConvert.SerializeObject(ContactList, Formatting.Indented);

                //Writes the contents (text) to the file
                File.WriteAllText(fileNameNew, myContents);

                //Display File Name
                // label_fileName.Content = text,

                //change global filename
                fileName = text;
            }
            return;
        }

        //***********Under Construction
        //Open File Code
        public void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            bool? openFile = dialog.ShowDialog();

            if (openFile == true)
            {
                //Get the file name the user chose
                fileName = dialog.FileName;

                //open the file and read the contents into a string
                string myFile = File.ReadAllText(fileName);

                try
                {
                    Contact NewList = JsonConvert.DeserializeObject<Contact>(myFile);

                    ContactList = NewList;

                    //Refresh the DataGrid
                    dataGrid_ContactList.ItemsSource = null;
                    dataGrid_ContactList.ItemsSource = ContactService.Contacts;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error opening file, please contact system Admin");
                }
            }
        }
    }
}

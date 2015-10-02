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

        //Open and Save Properties
        //Properties
        public string fileName = "Untitled";
        public string filePath;
        private Contact ContactList;
        public string myCategory;



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

        //Save Button Click
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            
            SaveEntry(currentContact);

        }

        //Save Entry || Edit mode Bool
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

            //Category

           // string myCategory = 

            //Add validation later to not rewrite on ID's
            //If in Editmode, Refresh
            if (IsEditMode == true)
            {
                // get a reference to the main window
                var mainwindow = (MainWindow)Owner;


                // trigger a refresh
                mainwindow.dataGrid_ContactList.Items.Refresh();
            }
            //If not in edit Mode, add new entry
            else
            {
                ContactService.Add(entry);
            }
            //Close Window
            Close();

        }
        //Read category 
      ///  public string ReadCategory()
      //  {
         //   string mySelection = 




     //   }

       
        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            //If file name has not been rewritten
            if (fileName == "Untitled")
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
            catch (Exception)
            {
                MessageBox.Show("Error occured while saving.  Please use SAVE AS and contact Sysytem Admin");
            }
        }
    

        private void MenuItem_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            OpenSaveAs();
        }

        private void MenuItem_Print_Click(object sender, RoutedEventArgs e)
        {
            SendToPrint();
        }

        private void MenutItem_Exit_Click(object sender, RoutedEventArgs e)
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
            catch (Exception)
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
            if (saveFileAs == true)
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

    }
}

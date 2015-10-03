using ContactManagementApp.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            //Load State info

            if (entry.State == null)
            {
                ConnectedWindow_ComboBox_State.SelectedIndex = 0;
            }
            else if (entry.State != null)
            {
                
                ConnectedWindow_ComboBox_State.SelectedItem = entry.State;
                MessageBox.Show(entry.State.ToString());
            }
            textBox_State.Text = entry.State;
            textBox_TelephoneNumber.Text = entry.TelephoneNumber;
            textBox_Zip.Text = entry.Zip;
            textBox_DOB.Text = entry.Birthdate;


            //get category from Object
            //If entry catagory has a value
            if (entry.Category == null)
            {
                entry.Category = "None";
                ConnectedWinow_radioButton_None.IsChecked = true;
            }

            if (entry.Category != null)
            {

                string contactCategory = entry.Category;


                string generalCategory = "General";
                string noneCategory = "None";
                string workCategoy = "Work";
                string personalCategory = "Personal";

                if (generalCategory == entry.Category)
                {
                    ConnectedWindow_radioButton_General.IsChecked = true;
                    return;
                }
                else if (noneCategory == entry.Category)
                {
                    ConnectedWinow_radioButton_None.IsChecked = true;
                    return;
                }
                else if (workCategoy == entry.Category)
                {
                    ConnectedWindow_radioButton_Work.IsChecked = true;
                    return;
                }
                else if (personalCategory == entry.Category)
                {
                    ConnectedWindow_radioButton_Personal.IsChecked = true;
                    return;
                }
                ConnectedWinow_radioButton_None.IsChecked = true;
            }
        }


        //Save Button Click
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {

            SaveEntry(currentContact);

        }

        //Save Entry || Edit mode Bool
        private void SaveEntry(Contact entry)
        {


            //Check input on required textboxes
            if(!checkTextBoxInput())
            {
                return;
            }


            //Grab input from textboxes
            currentContact = entry;

            entry.Address1 = textBox_Address1.Text;
            entry.Address2 = textBox_Address2.Text;

            //Format Date correctly on Save
            entry.Birthdate = textBox_DOB.Text.ToString();
            entry.City = textBox_City.Text;
            entry.EmailAddress = textBox_Email.Text;
            entry.FirstName = textBox_FirstName.Text;
            entry.LastName = textBox_LastName.Text;
            //Get State
            entry.State = ConnectedWindow_ComboBox_State.SelectedItem.ToString();
            entry.TelephoneNumber = textBox_TelephoneNumber.Text;
            entry.Zip = textBox_Zip.Text;


            //Save Category
            if (ConnectedWinow_radioButton_None.IsChecked == true)
            {

                string input = "None";
                entry.Category = input;
                MessageBox.Show(entry.Category);
            }
            else if (ConnectedWindow_radioButton_Work.IsChecked == true)
            {
                string input = "Work";
                entry.Category = input;
                MessageBox.Show(entry.Category);

            }
            else if (ConnectedWindow_radioButton_General.IsChecked == true)
            {
                string input = "General";
                entry.Category = input;
                MessageBox.Show(entry.Category);

            }
            else if (ConnectedWindow_radioButton_Personal.IsChecked == true)
            {
                string input = "Personal";
                entry.Category = input;
                MessageBox.Show(entry.Category);
            }
            else
            {
                string input = "None";
                entry.Category = input;
                MessageBox.Show(entry.Category);

            }
            //Save catgory goes above 


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


        //DOB input textbox only allows Digits to be entered
        private void textBox_DOB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9/-]+");
            e.Handled = regex.IsMatch(e.Text);
            textBox_DOB.MaxLength = 10;
            textBox_TelephoneNumber.MaxLength = 15;
        }

        //Clear Textboxes on Focus
        private void onFocusClear(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = String.Empty;
        }

        //Adding Formatting to Telephone Number
        private void textBox_TelephoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string myInput = textBox_TelephoneNumber.Text;
          textBox_TelephoneNumber.Text =  Regex.Replace(myInput, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
        }

        //Check require textbox input
        public bool checkTextBoxInput()
        {
           
                string firstName = textBox_FirstName.Text;
                string lastName = textBox_LastName.Text;
                string phone = textBox_TelephoneNumber.Text;


                if ((firstName == "" || lastName == "" || phone == ""))
                {
                    MessageBox.Show("Please enter the required values in the textboxes");
                         return false; 
                }
            return true;
           

        }

        //State Picker ComboBox Load options
        private void ConnectedWindow_ComboBox_State_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> states = new List<string>();
            states.Add("Pick your State...");
            states.Add("AL");
            states.Add("AK");
            states.Add("AZ");
            states.Add("AR");
            states.Add("CA");
            states.Add("CO");
            states.Add("CT");
            states.Add("DE");
            states.Add("FL");
            states.Add("GA");
            states.Add("HI");
            states.Add("ID");
            states.Add("IL");
            states.Add("IN");
            states.Add("IA");
            states.Add("KS");
            states.Add("KY");
            states.Add("LA");
            states.Add("ME");
            states.Add("MD");
            states.Add("MA");
            states.Add("MI");
            states.Add("MN");
            states.Add("MS");
            states.Add("MO");
            states.Add("MT");
            states.Add("NE");
            states.Add("NV");
            states.Add("NH");
            states.Add("NJ");
            states.Add("NM");
            states.Add("NY");
            states.Add("NC");
            states.Add("ND");
            states.Add("OH");
            states.Add("OK");
            states.Add("OR");
            states.Add("PA");
            states.Add("RI");
            states.Add("SC");
            states.Add("SD");
            states.Add("TN");
            states.Add("TX");
            states.Add("UT");
            states.Add("VT");
            states.Add("VA");
            states.Add("WA");
            states.Add("WV");
            states.Add("WI");
            states.Add("WY");
            


            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = states;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        //State Picker Combo Box, Select an Option
        private void ConnectedWindow_ComboBox_State_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            string value = comboBox.SelectedItem as string;
            this.Title = "Selected: " + value;
        }
    }
}

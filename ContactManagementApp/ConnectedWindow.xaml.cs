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
        public string myState;

        public void isLoaded()
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
            var comboBox = ConnectedWindow_ComboBox_State;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = states;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;

        }

        public void LoadEntry(Contact entry)
        {
            currentContact = entry;
        }


        //Save Button Click
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            SavecurrentContact(currentContact);
        }

        //Save currentContact || Edit mode Bool
        private void SavecurrentContact(Contact currentContact)
        {


            //Check input on required textboxes
            if (!checkTextBoxInput())
            {
                return;
            }




            currentContact.Address1 = textBox_Address1.Text;
            currentContact.Address2 = textBox_Address2.Text;

            //Format Date correctly on Save
            currentContact.Birthdate = textBox_DOB.Text.ToString();
            currentContact.City = textBox_City.Text;
            currentContact.EmailAddress = textBox_Email.Text;
            currentContact.FirstName = textBox_FirstName.Text;
            currentContact.LastName = textBox_LastName.Text;
            //Get State
            currentContact.State = ConnectedWindow_ComboBox_State.SelectedItem.ToString();
            currentContact.TelephoneNumber = textBox_TelephoneNumber.Text;
            currentContact.Zip = textBox_Zip.Text;


            //Save Category
            if (ConnectedWinow_radioButton_None.IsChecked == true)
            {

                string input = "None";
                currentContact.Category = input;
                MessageBox.Show(currentContact.Category);
            }
            else if (ConnectedWindow_radioButton_Work.IsChecked == true)
            {
                string input = "Work";
                currentContact.Category = input;
                MessageBox.Show(currentContact.Category);

            }
            else if (ConnectedWindow_radioButton_General.IsChecked == true)
            {
                string input = "General";
                currentContact.Category = input;
                MessageBox.Show(currentContact.Category);

            }
            else if (ConnectedWindow_radioButton_Personal.IsChecked == true)
            {
                string input = "Personal";
                currentContact.Category = input;
                MessageBox.Show(currentContact.Category);
            }
            else
            {
                string input = "None";
                currentContact.Category = input;
                MessageBox.Show(currentContact.Category);

            }


            if (IsEditMode == true)
            {
                // get a reference to the main window
                var mainwindow = (MainWindow)Owner;


                // trigger a refresh
                mainwindow.dataGrid_ContactList.Items.Refresh();
            }
            //If not in edit Mode, add new currentContact
            else
            {
                ContactService.Add(currentContact);
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
            textBox_TelephoneNumber.Text = Regex.Replace(myInput, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded();
            //Save instance to class Varible



            //Load user interface to currentContact that is passed in
            textBox_FirstName.Text = currentContact.FirstName;
            textBox_LastName.Text = currentContact.LastName;
            textBox_Address1.Text = currentContact.Address1;
            textBox_Address2.Text = currentContact.Address2;
            textBox_City.Text = currentContact.City;
            textBox_Email.Text = currentContact.EmailAddress;
            //Load State info

            if (currentContact.State == null)
            {
                ConnectedWindow_ComboBox_State.SelectedIndex = 0;
            }
            else if (currentContact.State != null)
            {
                ConnectedWindow_ComboBox_State.SelectedItem = currentContact.State;
            }

            textBox_TelephoneNumber.Text = currentContact.TelephoneNumber;
            textBox_Zip.Text = currentContact.Zip;
            textBox_DOB.Text = currentContact.Birthdate;


            //get category from Object
            //If currentContact catagory has a value
            if (currentContact.Category == null)
            {
                currentContact.Category = "None";
                ConnectedWinow_radioButton_None.IsChecked = true;
            }

            if (currentContact.Category != null)
            {

                string contactCategory = currentContact.Category;


                string generalCategory = "General";
                string noneCategory = "None";
                string workCategoy = "Work";
                string personalCategory = "Personal";

                if (generalCategory == currentContact.Category)
                {
                    ConnectedWindow_radioButton_General.IsChecked = true;
                    return;
                }
                else if (noneCategory == currentContact.Category)
                {
                    ConnectedWinow_radioButton_None.IsChecked = true;
                    return;
                }
                else if (workCategoy == currentContact.Category)
                {
                    ConnectedWindow_radioButton_Work.IsChecked = true;
                    return;
                }
                else if (personalCategory == currentContact.Category)
                {
                    ConnectedWindow_radioButton_Personal.IsChecked = true;
                    return;
                }
                ConnectedWinow_radioButton_None.IsChecked = true;
            }



        }

        //View Filter for RafioButton Change


    }
}

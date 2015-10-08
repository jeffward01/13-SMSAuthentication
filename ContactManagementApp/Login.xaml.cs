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
using Twilio;

namespace ContactManagementApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private MainWindow mainWindow;
        public decimal RandomGeneratedNumber;
        public string RandomGeneratedNumberString;

        //Login Constructor
        public Login()
        {
            InitializeComponent();
        }

        //Login Window Method (Constructor), it shows this as the main window in the start.
        public Login(MainWindow mainWindow) : this()
        {
            this.mainWindow = mainWindow;
        }

        //Submit Code Button
        private void loginWindow_button_SubmitCode_Click(object sender, RoutedEventArgs e)
        {
            string UsersEnteredCodeString = loginWindow_textBox_VerificationCode.Text;
            decimal UsersEnteredCode = Decimal.Parse(UsersEnteredCodeString);

            if (UsersEnteredCode == RandomGeneratedNumber)
            {
                this.mainWindow.Show();
            }
            MessageBox.Show("You entered in the wrong Validation Code.  Please Retry");
        }

        private void loginWindow_button_SendText_Click(object sender, RoutedEventArgs e)
        {
            sendAutherizationText();
        }

        public void sendAutherizationText()
        {

            //Get Users Phone Number
            string UsersPhoneNumber = grabPhoneNumber();

            if (UsersPhoneNumber == null)
            {

                return;
            }

            //Generate Random 10 Digit Code for user
            //Save Randomly Generated Number
            RandomGeneratedNumber = GenerateRandomNumber();
            RandomGeneratedNumberString = RandomGeneratedNumber.ToString();


            //Find your Account Sid and Auth Token


            string AccountSid = "AC61b76d7cf5033d39d3fdf1a6816e3e61";
            string AuthToken = "1f4545334cf64e12d68a224de622178c";

            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var message = twilio.SendMessage("+16508351288", UsersPhoneNumber, "Hello, I am the computer security guard!  Here is your 10-digit pass code: +" + RandomGeneratedNumberString + ".");
            MessageBox.Show("Users phone number: " + UsersPhoneNumber);
            MessageBox.Show("Here is your number: " + RandomGeneratedNumberString);

            Console.WriteLine(message.Sid);
        }
        public string grabPhoneNumber()
        {
            try
            {
                string input = loginWindow_textBox_EnterPhone.Text;

                input = JeffToolBox.RemoveSpecialCharacters(input);
                input = JeffToolBox.RemoveLetters(input);

                if (input.Length > 10 || input.Length < 10)
                {
                    MessageBox.Show("You number had: " + input.Length + "digits. You did not enter the correct number of digits (10) for an American Phone Number.  Please try again.");
                    return null;
                }

                input = "+1" + input;

                return input;
            }
            catch (Exception)
            {
                MessageBox.Show("You have entered in an invalid number, please retry.  \n Enter in phonenumber in this format: 1234567890");
                return null;
            }
        }

        //Generate Random 10 digit number
        public decimal GenerateRandomNumber()
        {
            Random random = new Random();
            string r = "";
            int i= 1;

            while(i < 11)
            {
                r += random.Next(0, 9);
                i++;
            }
            decimal myRandomNumber = Decimal.Parse(r);
            return myRandomNumber;
        }
 
        //Clear textbox phone number on focus
        private void loginWindow_textBox_EnterPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearOnFocusPhone();
        }

        //Clear Textbox on focus
        public void ClearOnFocusPhone()
        {
            loginWindow_textBox_EnterPhone.Text = "";
        }
        public void ClearOnFocusCode()
        {
            loginWindow_textBox_VerificationCode.Text = "";
        }

        private void loginWindow_textBox_VerificationCode_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearOnFocusCode();
        }
    }
}

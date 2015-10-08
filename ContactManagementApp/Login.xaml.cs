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
           if(TestAuthCode())
            {
                try
                {
                    this.mainWindow.Show();
                }
                catch
                {
                    MessageBox.Show("There was an error trying to open the main window");
                    this.mainWindow.ShowDialog();
                }
            }
           else
            {
                MessageBox.Show("You entered in the wrong Validation Code.  Please Retry");
                return;
            }
           
        }
        public bool TestAuthCode()
        {
            string UsersEnteredCodeString = getCodeInput();


            decimal UsersEnteredCode = Decimal.Parse(UsersEnteredCodeString);

            if (UsersEnteredCode == RandomGeneratedNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        //Get Code Input from user
        public string getCodeInput()
        {
            string input = loginWindow_textBox_VerificationCode.Text;

            input = JeffToolBox.RemoveSpecialCharacters(input);
            input = JeffToolBox.RemoveLetters(input);

            return input;
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

            while(i < 6)
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

        private void loginWindow_button_ReSendText_Click(object sender, RoutedEventArgs e)
        {
            sendAutherizationText();
        }

        //Call User on Click with Verification Code
        private void loginWindow_button_CallUse_Click(object sender, RoutedEventArgs e)
        {
            //Get Users Phone Number
            string UsersPhoneNumber = grabPhoneNumber();

            //If users number is null, stop Method.
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
            string myTwilioPhoneNumber = "+16508351288";

            //Instantaiate a new Twilio Rest Client
            var client = new TwilioRestClient(AccountSid, AuthToken);

            //Build Call Option
            var options = new CallOptions();
            //  options.Url = "C:\\Users\\jward01\\Documents\\Visual Studio 2015\\Projects\\WPFSMSAuth\\ContactManagementApp\\TwilioVoice.xml";
            options.Url = "http://demo.twilio.com/docs/voice.xml";
            options.To = UsersPhoneNumber;
            options.From = myTwilioPhoneNumber;



            //Initiate a new Outbound Call
            var call = client.InitiateOutboundCall(options);

           // var twilio = new TwilioRestClient(AccountSid, AuthToken);
           // var message = twilio.SendMessage(myTwilioPhoneNumber, UsersPhoneNumber, "Hello, I am the computer security guard!  Here is your 10-digit pass code: +" + RandomGeneratedNumberString + ".");
            MessageBox.Show("Users phone number: " + UsersPhoneNumber);
            MessageBox.Show("Here is your number: " + RandomGeneratedNumberString);

        }
    }
}

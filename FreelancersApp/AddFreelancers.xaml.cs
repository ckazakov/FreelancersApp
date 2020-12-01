using System;
using System.Data.OleDb;
using System.Globalization;
using System.Windows;

namespace FreelancersApp
{

    public partial class AddFreelancers : Window
    {
        Connect connect = new Connect();
        public AddFreelancers()
        {
            InitializeComponent();


            getGeneretedPassowords();
            registredDate.Text = DateTime.Today.ToString("d MMMM", CultureInfo.CreateSpecificCulture("en-us"));



        }

        public string result = "";
        Random rnd = new Random();

        string PasswordGenerator(Random rnd)
        {
            result = "";
            string Chars = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string sChars = "qwertyuiopasdfghjklzxcvbnm";
            string numbers = "1234567890";
            string symbols = "!@#$%^&*()";

            int CharsLng = Chars.Length;
            int sCharsLng = sChars.Length;
            int numbersLng = numbers.Length;
            int symbolsLng = symbols.Length;

            for (int a = 0; a < 3; a++)
            {
                result += Chars[rnd.Next(CharsLng)];
                result += sChars[rnd.Next(sCharsLng)];
                result += numbers[rnd.Next(numbersLng)];
                result += symbols[rnd.Next(symbolsLng)];
            }

            return result;
        }

        void getGeneretedPassowords()
        {
            PasswordGenerator(rnd);

            result = 'E' + result.Substring(1);
            emailPass.Text = result;

            PasswordGenerator(rnd);
            result = 'F' + result.Substring(1);
            freelancerPass.Text = result;

            PasswordGenerator(rnd);
            result = 'P' + result.Substring(1);
            payoneerPass.Text = result;
            paypalPass.Text = payoneerPass.Text;
            qiwiPass.Text = payoneerPass.Text;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            getGeneretedPassowords();
        }

        private void phoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            qiwiPhone.Text = phoneNumber.Text;
        }

        public void btnAddFreelancer_Click(object sender, RoutedEventArgs e)
        {
            DialogAddFreelancer.IsOpen = true;

            try { connect.OpenOleDbConnection(); }
            catch
            {
                PopIcon.Height = 0;
                PopErrorIcon.Height = 35;
                PopTextBox.Margin = new Thickness(10, 0, 0, 0);
                PopTextBox.Text = "#1.1 Не удалось открыть соединение с локальной базой данных SQL, возможно указан неправильный путь до файла!\n";
            }


            string FullName = fullName.Text;
            string DateOfBirth = dateOfBirth.Text;
            string UserEmail = userEmail.Text;
            string EmailPass = emailPass.Text;
            string PhoneNumber = phoneNumber.Text;
            string RegistredDate = registredDate.Text;

            string FreelancerLogin = txtbxFreelancerLogin.Text;
            string FreelancerPass = freelancerPass.Text;

            string PayoneerLogin = txtbxPayoneerLogin.Text;
            string PayoneerPass = payoneerPass.Text;
            string PayoneerQuestion = txtbxPayoneerQuestion.Text;
            string PayoneerAnswer = txtbxPayoneerAnswer.Text;


            string PaypalEmail = txtbxPaypalEmail.Text;
            string PaypalPass = paypalPass.Text;

            string PaypalVerify = "-";
            if (tglPaypalVerify.IsChecked == true)
                PaypalVerify = "V";

            string QiwiPhone = qiwiPhone.Text;
            string QiwiPass = qiwiPass.Text;

            string BankName = txtbxBankName.Text;
            string BankCardNumber = txtbxBankCardNumber.Text;
            string BankCardMonthYear = txtbxBankCardMonthYear.Text;
            string BankCardCvc = txtbxBankCardCvc.Text;

            string BankNumber20 = txtbxBankNumber20.Text;
            string BankBIC = txtbxBankBIC.Text;
            string BankINN = txtbxBankINN.Text;

            string HomeAddress = txtbxHomeAddress.Text;

            string Anydesk = txtAnydesk.Text;



            string SQL = $"INSERT INTO [Table] (FullName,DateOfBirth,UserEmail,EmailPass,QiwiPhone,QiwiPass,PayoneerLogin,PayoneerPass,PayoneerQuestion,PayoneerAnswer,Anydesk,BankName,BankCardNumber,BankCardMonthYear,BankCardCvc,PaypalEmail,PaypalPass,PaypalVerify,BankNumber20,BankBIC,BankINN,PhoneNumber,HomeAddress,FreelancerLogin,FreelancerPass,RegistredDate) " +
                                     $"VALUES (@FullName,@DateOfBirth,@UserEmail,@EmailPass,@QiwiPhone,@QiwiPass,@PayoneerLogin,@PayoneerPass,@PayoneerQuestion,@PayoneerAnswer,@Anydesk,@BankName,@BankCardNumber,@BankCardMonthYear,@BankCardCvc,@PaypalEmail,@PaypalPass,@PaypalVerify,@BankNumber20,@BankBIC,@BankINN,@PhoneNumber,@HomeAddress,@FreelancerLogin,@FreelancerPass,@RegistredDate)";



            OleDbCommand oleDbCommand = new OleDbCommand(SQL, connect.GetOleDbConnection());


            oleDbCommand.Parameters.AddWithValue("@FullName", FullName);
            oleDbCommand.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            oleDbCommand.Parameters.AddWithValue("@UserEmail", UserEmail);
            oleDbCommand.Parameters.AddWithValue("@EmailPass", EmailPass);

            oleDbCommand.Parameters.AddWithValue("@QiwiPhone", QiwiPhone);
            oleDbCommand.Parameters.AddWithValue("@QiwiPass", QiwiPass);

            oleDbCommand.Parameters.AddWithValue("@PayoneerLogin", PayoneerLogin);
            oleDbCommand.Parameters.AddWithValue("@PayoneerPass", PayoneerPass);
            oleDbCommand.Parameters.AddWithValue("@PayoneerQuestion", PayoneerQuestion);
            oleDbCommand.Parameters.AddWithValue("@PayoneerAnswer", PayoneerAnswer);

            oleDbCommand.Parameters.AddWithValue("@Anydesk", Anydesk);

            oleDbCommand.Parameters.AddWithValue("@BankName", BankName);
            oleDbCommand.Parameters.AddWithValue("@BankCardNumber", BankCardNumber);
            oleDbCommand.Parameters.AddWithValue("@BankCardMonthYear", BankCardMonthYear);
            oleDbCommand.Parameters.AddWithValue("@BankCardCvc", BankCardCvc);

            oleDbCommand.Parameters.AddWithValue("@PaypalEmail", PaypalEmail);
            oleDbCommand.Parameters.AddWithValue("@PaypalPass", PaypalPass);
            oleDbCommand.Parameters.AddWithValue("@PaypalVerify", PaypalVerify);

            oleDbCommand.Parameters.AddWithValue("@BankNumber20", BankNumber20);
            oleDbCommand.Parameters.AddWithValue("@BankBIC", BankBIC);
            oleDbCommand.Parameters.AddWithValue("@BankINN", BankINN);
            oleDbCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

            oleDbCommand.Parameters.AddWithValue("@HomeAddress", HomeAddress);

            oleDbCommand.Parameters.AddWithValue("@FreelancerLogin", FreelancerLogin);
            oleDbCommand.Parameters.AddWithValue("@FreelancerPass", FreelancerPass);

            oleDbCommand.Parameters.AddWithValue("@RegistredDate", RegistredDate);


            try
            {
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {


                    PopIcon.Height = 35;
                    PopErrorIcon.Height = 0;
                    PopTextBox.Margin = new Thickness(25, 25, 25, 25);
                    PopTextBox.Text = FullName + " успешно добавлен(а) в локальную базу данных!";

                }
                else
                {
                    PopTextBox.Text = "Возникла ошибка добавления фрилансера в ЛОКАЛЬНУЮ БАЗУ!";
                }
            }
            catch
            {
                PopTextBox.Text = "Не удалось добавить фрилансера в ЛОКАЛЬНУЮ БАЗУ!";
            }


            connect.CloseOleDbConnection();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Table table = new Table();

            table.Show();
            connect.CloseOleDbConnection();
            this.Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connect.CloseOleDbConnection();
        }

    }
}

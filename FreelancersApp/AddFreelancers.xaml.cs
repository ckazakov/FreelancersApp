using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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

 //           try { connect.MySQLopenConnection(); }
 //           catch {
 //               PopIcon.Height = 0;
 //               PopErrorIcon.Height = 35;
 //               PopTextBox.Margin = new Thickness(10,0,0,0);
 //               PopTextBox.Text = "#1 Не удалось открыть соединение с онлайн сервером!\n"; }

            try { connect.SQLopenConnection(); }
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


            string MySQL = $"INSERT INTO `testtable` (FullName,DateOfBirth,UserEmail,EmailPass,PhoneNumber,RegistredDate,FreelancerLogin,FreelancerPass,PayoneerLogin,PayoneerPass,PayoneerQuestion,PayoneerAnswer,PaypalEmail,PaypalPass,PaypalVerify,QiwiPhone,QiwiPass,BankName,BankCardNumber,BankCardMonthYear,BankCardCvc,BankNumber20,BankBIC,BankINN,HomeAddress,Anydesk) " +
                                          $"VALUES (@FullName,@DateOfBirth,@UserEmail,@EmailPass,@PhoneNumber,@RegistredDate,@FreelancerLogin,@FreelancerPass,@PayoneerLogin,@PayoneerPass,@PayoneerQuestion,@PayoneerAnswer,@PaypalEmail,@PaypalPass,@PaypalVerify,@QiwiPhone,@QiwiPass,@BankName,@BankCardNumber,@BankCardMonthYear,@BankCardCvc,@BankNumber20,@BankBIC,@BankINN,@HomeAddress,@Anydesk)";

            string SQL = $"INSERT INTO [Table] (FullName,DateOfBirth,UserEmail,EmailPass,PhoneNumber,RegistredDate,FreelancerLogin,FreelancerPass,PayoneerLogin,PayoneerPass,PayoneerQuestion,PayoneerAnswer,PaypalEmail,PaypalPass,PaypalVerify,QiwiPhone,QiwiPass,BankName,BankCardNumber,BankCardMonthYear,BankCardCvc,BankNumber20,BankBIC,BankINN,HomeAddress,Anydesk) " +
                                          $"VALUES (@FullName,@DateOfBirth,@UserEmail,@EmailPass,@PhoneNumber,@RegistredDate,@FreelancerLogin,@FreelancerPass,@PayoneerLogin,@PayoneerPass,@PayoneerQuestion,@PayoneerAnswer,@PaypalEmail,@PaypalPass,@PaypalVerify,@QiwiPhone,@QiwiPass,@BankName,@BankCardNumber,@BankCardMonthYear,@BankCardCvc,@BankNumber20,@BankBIC,@BankINN,@HomeAddress,@Anydesk)";


            MySqlCommand command = new MySqlCommand(MySQL, connect.GetMySQLConnection());
            SqlCommand sqlCommand = new SqlCommand(SQL, connect.GetSQLConnection());

            command.Parameters.Add("@FullName", MySqlDbType.VarChar).Value = FullName;
            sqlCommand.Parameters.AddWithValue("@FullName", FullName);
            command.Parameters.Add("@DateOfBirth", MySqlDbType.VarChar).Value = DateOfBirth;
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.Add("@UserEmail", MySqlDbType.VarChar).Value = UserEmail;
            sqlCommand.Parameters.AddWithValue("@UserEmail", UserEmail);
            command.Parameters.Add("@EmailPass", MySqlDbType.VarChar).Value = EmailPass;
            sqlCommand.Parameters.AddWithValue("@EmailPass", EmailPass);
            command.Parameters.Add("@PhoneNumber", MySqlDbType.VarChar).Value = PhoneNumber;
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            command.Parameters.Add("@RegistredDate", MySqlDbType.VarChar).Value = RegistredDate;
            sqlCommand.Parameters.AddWithValue("@RegistredDate", RegistredDate);

            command.Parameters.Add("@FreelancerLogin", MySqlDbType.VarChar).Value = FreelancerLogin;
            sqlCommand.Parameters.AddWithValue("@FreelancerLogin", FreelancerLogin);
            command.Parameters.Add("@FreelancerPass", MySqlDbType.VarChar).Value = FreelancerPass;
            sqlCommand.Parameters.AddWithValue("@FreelancerPass", FreelancerPass);

            command.Parameters.Add("@PayoneerLogin", MySqlDbType.VarChar).Value = PayoneerLogin;
            sqlCommand.Parameters.AddWithValue("@PayoneerLogin", PayoneerLogin);
            command.Parameters.Add("@PayoneerPass", MySqlDbType.VarChar).Value = PayoneerPass;
            sqlCommand.Parameters.AddWithValue("@PayoneerPass", PayoneerPass);
            command.Parameters.Add("@PayoneerQuestion", MySqlDbType.VarChar).Value = PayoneerQuestion;
            sqlCommand.Parameters.AddWithValue("@PayoneerQuestion", PayoneerQuestion);
            command.Parameters.Add("@PayoneerAnswer", MySqlDbType.VarChar).Value = PayoneerAnswer;
            sqlCommand.Parameters.AddWithValue("@PayoneerAnswer", PayoneerAnswer);

            command.Parameters.Add("@PaypalEmail", MySqlDbType.VarChar).Value = PaypalEmail;
            sqlCommand.Parameters.AddWithValue("@PaypalEmail", PaypalEmail);
            command.Parameters.Add("@PaypalPass", MySqlDbType.VarChar).Value = PaypalPass;
            sqlCommand.Parameters.AddWithValue("@PaypalPass", PaypalPass);
            command.Parameters.Add("@PaypalVerify", MySqlDbType.VarChar).Value = PaypalVerify;
            sqlCommand.Parameters.AddWithValue("@PaypalVerify", PaypalVerify);

            command.Parameters.Add("@QiwiPhone", MySqlDbType.VarChar).Value = QiwiPhone;
            sqlCommand.Parameters.AddWithValue("@QiwiPhone", QiwiPhone);
            command.Parameters.Add("@QiwiPass", MySqlDbType.VarChar).Value = QiwiPass;
            sqlCommand.Parameters.AddWithValue("@QiwiPass", QiwiPass);

            command.Parameters.Add("@BankName", MySqlDbType.VarChar).Value = BankName;
            sqlCommand.Parameters.AddWithValue("@BankName", BankName);
            command.Parameters.Add("@BankCardNumber", MySqlDbType.VarChar).Value = BankCardNumber;
            sqlCommand.Parameters.AddWithValue("@BankCardNumber", BankCardNumber);
            command.Parameters.Add("@BankCardMonthYear", MySqlDbType.VarChar).Value = BankCardMonthYear;
            sqlCommand.Parameters.AddWithValue("@BankCardMonthYear", BankCardMonthYear);
            command.Parameters.Add("@BankCardCvc", MySqlDbType.VarChar).Value = BankCardCvc;
            sqlCommand.Parameters.AddWithValue("@BankCardCvc", BankCardCvc);

            command.Parameters.Add("@BankNumber20", MySqlDbType.VarChar).Value = BankNumber20;
            sqlCommand.Parameters.AddWithValue("@BankNumber20", BankNumber20);
            command.Parameters.Add("@BankBIC", MySqlDbType.VarChar).Value = BankBIC;
            sqlCommand.Parameters.AddWithValue("@BankBIC", BankBIC);
            command.Parameters.Add("@BankINN", MySqlDbType.VarChar).Value = BankINN;
            sqlCommand.Parameters.AddWithValue("@BankINN", BankINN);

            command.Parameters.Add("@HomeAddress", MySqlDbType.VarChar).Value = HomeAddress;
            sqlCommand.Parameters.AddWithValue("@HomeAddress", HomeAddress);

            command.Parameters.Add("@Anydesk", MySqlDbType.VarChar).Value = Anydesk;
            sqlCommand.Parameters.AddWithValue("@Anydesk", Anydesk);

            //try
            //{
            //    if (command.ExecuteNonQuery() == 1)
            //    {
            //        progressBar.Visibility = Visibility.Hidden;

            //        PopIcon.Height = 35;
            //        PopErrorIcon.Height = 0;
            //        PopTextBox.Margin = new Thickness(25, 25, 25, 25);
            //        PopTextBox.Text = "Фрилансер успешно добавлен в онлайн базу данных!";

            //    } else
            //    {
            //        PopTextBox.Text = "Возникла ошибка добавления фрилансера в онлайн базу данных, возможен конфликт полей или проблема загрузки таблицы! Проверьте соответствие названий таблицы и всех ее полей!";
            //    }
            //} catch {
            //    PopTextBox.Text += "#2 Ошибка подключения к базе данных!";
            //}


            // ДОБАВЛЕНИЕ В ЛОКАЛЬНУЮ БАЗУ!
            try
            {
                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                   

                    PopIcon.Height = 35;
                    PopErrorIcon.Height = 0;
                    PopTextBox.Margin = new Thickness(25, 25, 25, 25);
                    PopTextBox.Text = "Фрилансер успешно добавлен в локальную базу данных!";

                }
                else
                {
                    PopTextBox.Text = "Возникла ошибка добавления фрилансера в ЛОКАЛЬНУЮ БАЗУ!";
                }
            } catch
            {
                PopTextBox.Text = "Не удалось добавить фрилансера в ЛОКАЛЬНУЮ БАЗУ!";
            }


            //connect.MySQLcloseConnection();
            connect.SQLcloseConnection();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Table table = new Table();
            
            table.Show();
            connect.MySQLcloseConnection();
            connect.SQLcloseConnection();
            this.Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connect.MySQLcloseConnection();
            connect.SQLcloseConnection();
        }
    }
}

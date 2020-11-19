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

namespace FreelancersApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PasswordTextBox.Focus();
            
        }


        void checkPassoword()
        {
            if (PasswordTextBox.Password == "9696")
            {
                Table table = new Table();
                table.Show();
                this.Close();
            }
            else
            {
                icoKey.Foreground = Brushes.Red;
                txtParol.FontSize = 14;
                txtParol.Foreground = Brushes.Red;
                txtParol.Text = "Пароль не верный!";
                PasswordTextBox.Clear();
                PasswordTextBox.Focus();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            checkPassoword();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkPassoword();
            }
        }
    }
}

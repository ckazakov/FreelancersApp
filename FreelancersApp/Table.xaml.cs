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
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace FreelancersApp
{
    public partial class Table : Window
    {

        SqlConnection sqlConnection;


     

        public Table()
        {
            InitializeComponent();
            

        }

        public void FillOrUpdateTable()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Table]", sqlConnection);

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("Freelancersss");
                dataAdapter.Fill(dataTable);
                DataGridv.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


            public async void Window_Loaded(object sender, RoutedEventArgs e)
            {
            string sqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\C#\Freelancers\FreelancersApp\FreelancersApp\DBFreelancers.mdf;Integrated Security=True";
                   sqlConnection = new SqlConnection(sqlConnectionString);

            await sqlConnection.OpenAsync();

            FillOrUpdateTable();


            


        }   

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        int index;
        string columnPath;
        string columnValue;
        string rowID;
        string newValue;
        public void MouseLeftClickToRow(object sender, MouseEventArgs e)
        {
            index = DataGridv.ItemContainerGenerator.IndexFromContainer((DataGridRow)sender);
            columnPath = DataGridv.CurrentColumn.SortMemberPath.ToString();
            columnValue = DataGridv.CurrentColumn.Header.ToString();
        }


        public void DataGridv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            
            DataGridv.SelectedItem = DataGridv.Items[index];
            //DataGridv.ScrollIntoView(DataGridv.Items[index]);


             rowID = ((DataRowView)DataGridv.SelectedItems[0]).Row["Id"].ToString();
             newValue = ((TextBox)e.EditingElement).Text;


            dialogg.IsOpen = true;

            

        }

        private void Apply_Changes(object sender, RoutedEventArgs e)
        {
            txts.Text = rowID + "   " + newValue + " " + columnPath + " " + columnValue;

            string sqlUpdateQuery = $"UPDATE [Table] SET {columnPath}='{newValue}' WHERE ID={rowID}";

            SqlCommand updateCommand = new SqlCommand(sqlUpdateQuery, sqlConnection);

            try
            {
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }



            FillOrUpdateTable();
        }

        private void userWasBlocked(object sender, RoutedEventArgs e)
        {
             
        }

    }
}

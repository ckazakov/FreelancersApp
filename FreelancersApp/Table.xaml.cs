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
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBFreelancers.mdf;Integrated Security=True");
        DataTable dataTable = new DataTable("Freelancersss");


        public Table()
        {
            InitializeComponent();
            

        }

        public void FillOrUpdateTable()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Table]", sqlConnection);

            try
            {
                dataTable.Clear();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
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
        string oldCellValue;
        public void MouseLeftClickToRow(object sender, MouseEventArgs e)
        {
            index = DataGridv.ItemContainerGenerator.IndexFromContainer((DataGridRow)sender);
            columnPath = DataGridv.CurrentColumn.SortMemberPath.ToString();
            columnValue = DataGridv.CurrentColumn.Header.ToString();

            
           
        }

        public void DataGridv_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataRowView rowView = e.Row.Item as DataRowView;

            oldCellValue = rowView[DataGridv.CurrentColumn.DisplayIndex].ToString();


        }



        public void DataGridv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            
            DataGridv.SelectedItem = DataGridv.Items[index];
            //DataGridv.ScrollIntoView(DataGridv.Items[index]);

            

             rowID = ((DataRowView)DataGridv.SelectedItems[0]).Row["Id"].ToString();
             newValue = ((TextBox)e.EditingElement).Text;

            if (oldCellValue != newValue)
            {
                PopTextBox.Text = "Изменение значения \n" + oldCellValue + "\n->\n" + newValue;
                dialogg.IsOpen = true;

            }


        }

        private void Apply_Changes(object sender, RoutedEventArgs e)
        {
 

            string sqlUpdateQuery = $"UPDATE [Table] SET {columnPath}=N'{newValue}' WHERE ID={rowID}";

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

             
            AddFreelancers addFreelancers = new AddFreelancers();

            addFreelancers.Show();
            

        }


        public void btnRefreshTable(object sender, RoutedEventArgs e)
        {

            FillOrUpdateTable();
        }

        public void btnDeleteRow(object sender, RoutedEventArgs e)
        {
            AlertDeleteRow.IsOpen = true;

         
        }

        public void btnDeleteAccept(object sender, RoutedEventArgs e)
        {
            int indx = Convert.ToInt32(index) + 1;

            string sqlDeleteQuery = $"DELETE FROM [Table] WHERE ID ={indx}";
            SqlCommand deleteCommand = new SqlCommand(sqlDeleteQuery, sqlConnection);



            try
            {
                deleteCommand.ExecuteNonQuery();
                FillOrUpdateTable();
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
    }
}

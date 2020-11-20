using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelancersApp
{
    public partial class Table : Window
    {

        Connect connect = new Connect();

        public Table()
        {
            InitializeComponent();

            connect.OpenOleDbConnection();

            FillOrUpdateTable();
            


        }

        public void FillOrUpdateTable()
        {
            OleDbCommand command = new OleDbCommand("SELECT * FROM [Table]", connect.oleDbConnection);

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset,"[Table]");
            DataGridv.ItemsSource = dataset.Tables["[Table]"].DefaultView;
        }

        public void getRowID()
        {
            var selectedRowID = DataGridv.SelectedCells[0].Column.GetCellContent(DataGridv.SelectedCells[0].Item);
            if (selectedRowID is TextBlock)
            {
               rowID = (selectedRowID as TextBlock).Text;
                MessageBox.Show(rowID);
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

            getRowID();

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


            getRowID();

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

            OleDbCommand updateCommand = new OleDbCommand(sqlUpdateQuery, connect.oleDbConnection);

            updateCommand.ExecuteNonQuery();

            FillOrUpdateTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            AddFreelancers addFreelancers = new AddFreelancers();

            addFreelancers.Show();
            connect.CloseOleDbConnection();


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
            OleDbCommand deleteCommand = new OleDbCommand(sqlDeleteQuery, connect.oleDbConnection);


            deleteCommand.ExecuteNonQuery();
            FillOrUpdateTable();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connect.CloseOleDbConnection();
        }
    }
}

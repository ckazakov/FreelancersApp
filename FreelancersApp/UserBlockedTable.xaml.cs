
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelancersApp
{

    public partial class UserBlockedTable : Window
    {

        Connect connect = new Connect();
        public UserBlockedTable()
        {
            InitializeComponent();
            connect.OpenOleDbConnection();
            FillTable();
        }

        int index;
        string rowID;
        string currentFullName;
        int ID;




        public void FillTable()
        {
            OleDbCommand command = new OleDbCommand("SELECT * FROM [UserBlockedTable] ORDER BY ID DESC", connect.oleDbConnection);

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset, "[UserBlockedTable]");
            DataGridv.ItemsSource = dataset.Tables["[UserBlockedTable]"].DefaultView;
        }

        public void MouseLeftClickToRow(object sender, MouseEventArgs e)
        {

            index = DataGridv.ItemContainerGenerator.IndexFromContainer((DataGridRow)sender);

            DataGridv.SelectedItem = DataGridv.Items[index];

            var selectedRowID = DataGridv.SelectedCells[0].Column.GetCellContent(DataGridv.SelectedCells[0].Item);
            var selectedFullName = DataGridv.SelectedCells[1].Column.GetCellContent(DataGridv.SelectedCells[1].Item);
            if (selectedRowID is TextBlock)
            {
                rowID = (selectedRowID as TextBlock).Text;
                if (rowID != "")
                {
                    ID = Convert.ToInt16(rowID);
                    currentFullName = (selectedFullName as TextBlock).Text;
                }

            }






        }



        private void btnGoToMainTable(object sender, RoutedEventArgs e)
        {
            Table table = new Table();
            table.Show();
            this.Close();
        }

        private void btnDeleteRow(object sender, RoutedEventArgs e)
        {
            if (ID > 0)
            {
                txtBlockDelete.Text = currentFullName + " будет удален(а).";
                AlertDeleteRow.IsOpen = true;
            }
            else
            {
                txtError.Text = "Для начала необходимо выбрать строку для удаления...";
                Error.IsOpen = true;
            }
        }

        private void btnDeleteAccept(object sender, RoutedEventArgs e)
        {

            string sqlDeleteQuery = $"DELETE FROM [UserBlockedTable] WHERE ID ={ID}";
            OleDbCommand deleteCommand = new OleDbCommand(sqlDeleteQuery, connect.oleDbConnection);

            deleteCommand.ExecuteNonQuery();
            FillTable();

        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connect.CloseOleDbConnection();

        }

        private void ClickBtnUnbanned(object sender, RoutedEventArgs e)
        {



            if (ID > 0)
            {
                string sqlSendRowQuery = $"INSERT INTO [Table] ([ALL]) SELECT * FROM [UserBlockedTable] WHERE ID ={ID}";
                OleDbCommand sendRowCommand = new OleDbCommand(sqlSendRowQuery, connect.GetOleDbConnection());

                string sqlDeleteQuery = $"DELETE FROM [UserBlockedTable] WHERE ID ={ID}";
                OleDbCommand deleteCommand = new OleDbCommand(sqlDeleteQuery, connect.oleDbConnection);


                try
                {
                    sendRowCommand.ExecuteNonQuery();
                    deleteCommand.ExecuteNonQuery();
                    txtError.Text = "Пользователь ID: " + ID + " перенесен в основную таблицу";
                }
                catch
                {
                    txtError.Text = "Возникла ошибка выполнения SQL запроса! Возможно пользователь с данным ID уже добавлен в таблицу. ";
                }

                Error.IsOpen = true;
                FillTable();

            }
            else if (ID == 0)
            {
                txtError.Text = "Для начала необходимо выбрать строку...";
                Error.IsOpen = true;
            }
        }


    }
}

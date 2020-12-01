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
            OleDbCommand command = new OleDbCommand("SELECT * FROM [Table] ORDER BY ID", connect.oleDbConnection);

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset, "[Table]");
            DataGridv.ItemsSource = dataset.Tables["[Table]"].DefaultView;
            int count_row = DataGridv.Items.Count-1;
            countOfActiveUsers.Content = "Активных: " + count_row;
        }



        int index;
        int ID;
        string columnPath;
        string columnValue;
        
        string rowID;
        string newValue;
        string oldCellValue;
        string rowFullNameValue;
        




        public void MouseLeftClickToRow(object sender, MouseEventArgs e)
        {

            index = DataGridv.ItemContainerGenerator.IndexFromContainer((DataGridRow)sender);
            columnPath = DataGridv.CurrentColumn.SortMemberPath.ToString();
            columnValue = DataGridv.CurrentColumn.Header.ToString();



            // Выделение строки назначено в методе BeginningEdit

            //DataGridv.SelectedItem = DataGridv.Items[index];

            var selectedRowID = DataGridv.SelectedCells[0].Column.GetCellContent(DataGridv.SelectedCells[0].Item);



            if (selectedRowID is TextBlock)
            {
                rowID = (selectedRowID as TextBlock).Text;
                
                if (rowID != "")
                {
                    ID = Convert.ToInt16(rowID);
                    
                }
            }

        }

        public void DataGridv_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {


            DataRowView rowView = e.Row.Item as DataRowView;

            oldCellValue = rowView[DataGridv.CurrentColumn.DisplayIndex].ToString();

            e.Row.IsSelected = true;

            if(oldCellValue != "" && oldCellValue != " ")
            {
                Clipboard.SetText(oldCellValue);

                SnackMsg.IsActive = true;

                SnackMsg_Content.Content = "Скопировано: " + oldCellValue;
            }

            

            

        }

        private void SnackMsg_MouseEnter(object sender, MouseEventArgs e)
        {
            SnackMsg.IsActive = false;
        }



        public void DataGridv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            SnackMsg.IsActive = false;

            DataGridv.SelectedItem = DataGridv.Items[index];

            var rowFullName = DataGridv.SelectedCells[1].Column.GetCellContent(DataGridv.SelectedCells[1].Item);

            if (rowFullName is TextBlock)
            {
                rowFullNameValue = (rowFullName as TextBlock).Text;
            }

                //DataGridv.ScrollIntoView(DataGridv.Items[index]);

                newValue = ((TextBox)e.EditingElement).Text;

            if (oldCellValue != newValue)
            {
                FullNameOfRow.Text = rowFullNameValue;
                PopTextBox.Text =  "Изменение значения \n" + oldCellValue + "\n->\n" + newValue;
                dialogg.IsOpen = true;

            }

        }

        private void Apply_Changes(object sender, RoutedEventArgs e)
        {

            string sqlUpdateQuery = $"UPDATE [Table] SET {columnPath}='{newValue}' WHERE ID={rowID}";

            OleDbCommand updateCommand = new OleDbCommand(sqlUpdateQuery, connect.oleDbConnection);

            updateCommand.ExecuteNonQuery();

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




        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connect.CloseOleDbConnection();
        }



        public void clickBtnAddThisToBlockedUsers(object sender, RoutedEventArgs e)
        {

            if (ID > 0)
            {

                txtSendtoUserBlockTable.Text = "Пользователя ID: " +ID + " будет перенесен из основной таблицы в таблицу заблокированных.";
                AlertSendToBlockedTable.IsOpen = true;


            }
            else if (ID == 0)
            {


                txtError.Text = "Для начала выберите пользователя...";
                Error.IsOpen = true;

            }

        }

        private void clickBtnGoToUserBlockedTable(object sender, RoutedEventArgs e)
        {

            UserBlockedTable blockedTable = new UserBlockedTable();
            blockedTable.Show();
        }

        private void ClickBtnSendToUserBlockedTable(object sender, RoutedEventArgs e)
        {

            string sqlSendRowQuery = $"INSERT INTO [UserBlockedTable] ([ALL]) SELECT * FROM [Table] WHERE ID ={ID}";
            OleDbCommand sendRowCommand = new OleDbCommand(sqlSendRowQuery, connect.GetOleDbConnection());



            string sqlDeleteQuery = $"DELETE FROM [Table] WHERE ID ={ID}";
            OleDbCommand deleteCommand = new OleDbCommand(sqlDeleteQuery, connect.oleDbConnection);


            try
            {
                sendRowCommand.ExecuteNonQuery();
                deleteCommand.ExecuteNonQuery();
                txtError.Text = "Пользователь успешно перенесен!";
            }
            catch
            {

                txtError.Text = "Возникла ошибка выполнения SQL запроса! Возможно пользователь с ID: " + ID + " уже добавлен в таблицу.";

            }
            Error.IsOpen = true;

            FillOrUpdateTable();

        }


    }
}

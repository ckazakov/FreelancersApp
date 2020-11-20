using System.Data.OleDb;

namespace FreelancersApp
{
    class Connect
    {

        public readonly OleDbConnection oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DBFreelancers.mdb;Persist Security Info=False;");

        public void OpenOleDbConnection()
        {
            if (oleDbConnection.State == System.Data.ConnectionState.Closed)
                oleDbConnection.Open();
        }

        public void CloseOleDbConnection()
        {
            if (oleDbConnection.State == System.Data.ConnectionState.Open)
                oleDbConnection.Close();
        }

        public OleDbConnection GetOleDbConnection()
        {
            return oleDbConnection;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Restaurant
{
    class DataBase
    {
        static public DataSet dataSet  = new DataSet();

        static public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString);

        //private bool newRowAdding = false;

        public static void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
    }
}

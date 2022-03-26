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
        static public DataSet dataSet = null;

        static public SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDBsqlServer"].ConnectionString);

        private bool newRowAdding = false;
    }
}

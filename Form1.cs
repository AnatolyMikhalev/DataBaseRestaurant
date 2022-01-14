using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Restaurant 
{
    public partial class Restaurant : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlConnection nrthwndConnection = null;
        public Restaurant()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString);

            sqlConnection.Open();

            //tabControl.TabPages.Remove(tabPageEmployees);

            tabControl.TabPages.Clear();

            tabControl.TabPages.Add(tabPageAutorizaion);

            //if (sqlConnection.State == ConnectionState.Open)
            //{
            //    MessageBox.Show("Подключение установлено!");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (logintextbox.Text)
            {
                case "admin":
                    if (passwordtextbox.Text == "adminpass");
                        tabControl.TabPages.Add(tabPageEmployees);
                        //tabControl.Enabled = true;
                    break;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            SqlDataReader dataReader = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id, Lastname, FirstName, BirthDate, Address, Phone, Post FROM Employees",
                    sqlConnection);

                dataReader = sqlCommand.ExecuteReader();

                ListViewItem item = null;

                while (dataReader.Read())
                {
                    item = new ListViewItem(new string[]
                    {
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["Lastname"]),
                        Convert.ToString(dataReader["FirstName"]),
                        Convert.ToString(dataReader["BirthDate"]),
                        Convert.ToString(dataReader["Address"]),
                        Convert.ToString(dataReader["Phone"]),
                        Convert.ToString(dataReader["Post"]) 
                    } ) ;

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
        }
    }
}

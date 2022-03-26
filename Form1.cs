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
        private Employees employees = null;
        private CreateOrder createOrder = null;



        //private DataBase.sqlConnection DataBase.sqlConnection = null;

        //private SqlCommandBuilder sqlBuilderEmployees = null;
        private SqlCommandBuilder sqlBuilderMenu = null;
        private SqlCommandBuilder sqlBuilderOrders = null;

        //private SqlDataAdapter Employees.sqlDataAdapter = null;
        private SqlDataAdapter sql_DA_AddOrder = null;
        private SqlDataAdapter sql_DA_Orders = null;
        private SqlDataAdapter sql_DA_OrderedDishes = null;


        private DataTable dataTableGridView3 = null;
        private DataTable dataTableGridViewOrder = null;

        public bool newRowAdding = false;

        public Restaurant()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            employees.SearchEmployee(textBox1.Text, comboBox1.SelectedIndex);
        }  // Поиск сотрудников
        private void initDataTableGridView3()
        {
            dataTableGridView3 = DataBase.dataSet.Tables["Menu"].Clone();
        }
        private void initDataTableGridViewOrder()
        {
            dataTableGridViewOrder = DataBase.dataSet.Tables["Menu"].Clone();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (logintextbox.Text)
            {
                case "admin":
                    if (passwordtextbox.Text == "adminpass")
                    {
                        tabControl.TabPages.Add(tabPageEmployees);
                        //employees.LoadDataEmployees();
                    }
                    //tabControl.Enabled = true;
                    break;
            }
        }   // Password
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            employees.ReloadDataEmployees();
            ReloadAddOrders();
        } // Reload
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                employees.dataGridView1_CellContentClick(e);
                employees.ReloadDataEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  // Employees Insert Update Delete
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                if (e.ColumnIndex == 4)
                {
                    int r = e.RowIndex;

                    DataRow row = dataTableGridView3.NewRow();

                    row["DishID"] = dataGridView2.Rows[r].Cells["DishID"].Value;
                    row["DishName"] = dataGridView2.Rows[r].Cells["DishName"].Value;
                    row["Price"] = dataGridView2.Rows[r].Cells["Price"].Value;
                    row["DishWeight"] = dataGridView2.Rows[r].Cells["DishWeight"].Value;
                    row["Command"] = "Remove";

                    dataTableGridView3.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (MessageBox.Show("Удалить блюдо из заказа?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                    {
                        dataTableGridView3.Rows.RemoveAt(e.RowIndex);          // Удаляем строку из таблицы
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;

                foreach (DataRow row in DataBase.dataSet.Tables["OrderedDishes"].Rows)
                {
                    if (DataBase.dataSet.Tables["Orders"].Rows[r]["Id"].ToString() == row["OrderId"].ToString())
                    {
                        var addingRows = from addingRow in DataBase.dataSet.Tables["Menu"].AsEnumerable()
                                         where (int)addingRow["DishId"] == (int)row["DishId"]
                                         select addingRow;

                        foreach (var addingRow in addingRows)
                        {
                            DataRow newAddingRow = dataTableGridViewOrder.NewRow();

                            newAddingRow["DishID"] = addingRow["DishID"];
                            newAddingRow["DishName"] = addingRow["DishName"];
                            newAddingRow["Price"] = addingRow["Price"];
                            newAddingRow["DishWeight"] = addingRow["DishWeight"];


                            dataTableGridViewOrder.Rows.Add(newAddingRow);
                        }

                        //DataRow addingRow = dataTableGridViewOrder.NewRow();

                        //row["DishID"] = dataGridView2.Rows[r].Cells["DishID"].Value;
                        //row["DishName"] = dataGridView2.Rows[r].Cells["DishName"].Value;
                        //row["Price"] = dataGridView2.Rows[r].Cells["Price"].Value;
                        //row["DishWeight"] = dataGridView2.Rows[r].Cells["DishWeight"].Value;

                        //добавляем в table строку
                    }
                }

                //задаём table как datasourse of datagridvieworder


               // dataTableGridViewOrder.Rows.Add(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                employees.UserAddedRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // Employees adding row

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                employees.CellValueChanged();

                //if (newRowAdding == false)
                //{
                //    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                //    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];

                //    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                //    dataGridView1[7, rowIndex] = linkCell;

                //    editingRow.Cells["Command"].Value = "Update";

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  // Employees changing data

        private void ReloadAddOrders()
        {
            try
            {
                DataBase.dataSet.Tables["Menu"].Clear();

                sql_DA_AddOrder.Fill(DataBase.dataSet, "Menu");

                dataGridView2.DataSource = DataBase.dataSet.Tables["Menu"];
                dataGridView3.DataSource = dataTableGridView3;
                dataGridViewOrders.DataSource = DataBase.dataSet.Tables["Oredrs"];
                dataGridViewOrder.DataSource = dataTableGridViewOrder;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView2[4, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadAddOrders() 
        {
            try
            {
                sql_DA_AddOrder = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", DataBase.sqlConnection);
                sql_DA_Orders = new SqlDataAdapter("SELECT *, 'Complete' AS [Command] FROM Orders", DataBase.sqlConnection);
                sql_DA_OrderedDishes = new SqlDataAdapter("SELECT * FROM OrderedDishes", DataBase.sqlConnection);

                sqlBuilderOrders = new SqlCommandBuilder(sql_DA_AddOrder);

                dataTableGridView3 = new DataTable();
                dataTableGridViewOrder = new DataTable();

                sql_DA_AddOrder.Fill(DataBase.dataSet, "Menu");
                sql_DA_Orders.Fill(DataBase.dataSet, "Orders");
                sql_DA_OrderedDishes.Fill(DataBase.dataSet, "OrderedDishes");

                dataGridView2.DataSource = DataBase.dataSet.Tables["Menu"];
                dataGridViewOrders.DataSource = DataBase.dataSet.Tables["Orders"];

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView2[4, i] = linkCell;
                }
                for (int i = 0; i < dataGridViewOrders.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView2[3, i] = linkCell;
                }

                initDataTableGridView3();
                initDataTableGridViewOrder();
                ReloadAddOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tableEmployees = DataBase.dataSet.Tables["Employees"];

                bool availableWaiter = false;

                foreach (DataRow row in DataBase.dataSet.Tables["Employees"].Rows)
                {
                    if (row["Id"].ToString() == textBox3.Text && row["Post"].ToString() == "Официант")
                    {
                        availableWaiter = true;
                        break;
                    }
                }
                if (!availableWaiter)
                {
                    throw new Exception("Официанта с заданным Id не существует");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //DataBase.sqlConnection = new DataBase.sqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDBsqlServer"].ConnectionString);

            //DataBase.sqlConnection = new DataBase.sqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString);

            employees = new Employees();
            createOrder = new CreateOrder();

            if (DataBase.sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                DataBase.sqlConnection.Open();
            }
            tabControl.TabPages.Remove(tabPageEmployees);
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(tabPageAutorizaion);
            tabControl.TabPages.Add(tabPageEmployees);
            tabControl.TabPages.Add(tabPageAddOrder);
            tabControl.TabPages.Add(tabPageOrders);
            employees.LoadDataEmployees(ref dataGridView1, ref newRowAdding);
            LoadAddOrders();
        }

    }
}

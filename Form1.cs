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

        private SqlCommandBuilder sqlBuilderEmployees = null;
        private SqlCommandBuilder sqlBuilderMenu = null;
        private SqlCommandBuilder sqlBuilderOrders = null;

        private SqlDataAdapter sql_DA_Employees = null;
        private SqlDataAdapter sql_DA_AddOrder = null;
        private SqlDataAdapter sql_DA_Orders = null;
        private SqlDataAdapter sql_DA_OrderedDishes = null;

        private DataSet dataSet = null;

        private DataTable dataTableGridView3 = null;
        private DataTable dataTableGridViewOrder = null;

        private bool newRowAdding = false;

        public Restaurant()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Id LIKE '%{textBox1.Text}%'";
                    break;
                case 1:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"LastName LIKE '%{textBox1.Text}%'";
                    break;
                case 2:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"FirstName LIKE '%{textBox1.Text}%'";
                    break;
                case 3:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Address LIKE '%{textBox1.Text}%'";
                    break;
                case 4:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Post LIKE '%{textBox1.Text}%'";
                    break;
                default:
                    break;
            }
        }  // Поиск сотрудников
        private void initDataTableGridView3()
        {
            dataTableGridView3 = dataSet.Tables["Menu"].Clone();
        }
        private void initDataTableGridViewOrder()
        {
            dataTableGridViewOrder = dataSet.Tables["Menu"].Clone();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (logintextbox.Text)
            {
                case "admin":
                    if (passwordtextbox.Text == "adminpass")
                    {
                        tabControl.TabPages.Add(tabPageEmployees); 
                        LoadDataEmployees();
                    }
                    //tabControl.Enabled = true;
                    break;
            }
        }   // Password
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ReloadDataEmployees();
            ReloadAddOrders();
        } // Reload
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(rowIndex);          // Удаляем строку из таблицы

                            dataSet.Tables["Employees"].Rows[rowIndex].Delete(); // Удаляем строку из dataSet

                            sql_DA_Employees.Update(dataSet, "Employees"); // Удаляем строку из Базы Данных
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Employees"].NewRow();

                        row["LastName"] = dataGridView1.Rows[rowIndex].Cells["LastName"].Value;
                        row["FirstName"] = dataGridView1.Rows[rowIndex].Cells["FirstName"].Value;
                        row["BirthDate"] = dataGridView1.Rows[rowIndex].Cells["BirthDate"].Value;
                        row["Address"] = dataGridView1.Rows[rowIndex].Cells["Address"].Value;
                        row["Phone"] = dataGridView1.Rows[rowIndex].Cells["Phone"].Value;
                        row["Post"] = dataGridView1.Rows[rowIndex].Cells["Post"].Value;

                        dataSet.Tables["Employees"].Rows.Add(row);

                        dataSet.Tables["Employees"].Rows.RemoveAt(dataSet.Tables["Employees"].Rows.Count - 1);

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";

                        sql_DA_Employees.Update(dataSet, "Employees");

                        newRowAdding = false;

                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        DataTable table = dataSet.Tables["Employees"];

                        table.Rows[r]["LastName"] = dataGridView1.Rows[r].Cells["LastName"].Value;
                        table.Rows[r]["FirstName"] = dataGridView1.Rows[r].Cells["FirstName"].Value;
                        table.Rows[r]["BirthDate"] = dataGridView1.Rows[r].Cells["BirthDate"].Value;
                        table.Rows[r]["Address"] = dataGridView1.Rows[r].Cells["Address"].Value;
                        table.Rows[r]["Phone"] = dataGridView1.Rows[r].Cells["Phone"].Value;
                        table.Rows[r]["Post"] = dataGridView1.Rows[r].Cells["Post"].Value;

                        MessageBox.Show($"Обновлено строк: {Convert.ToString(sql_DA_Employees.Update(dataSet, "Employees"))}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        sql_DA_Employees.Update(table);

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";
                    }

                    ReloadDataEmployees();
                }
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

                foreach (DataRow row in dataSet.Tables["OrderedDishes"].Rows)
                {
                    if (dataSet.Tables["Orders"].Rows[r]["Id"].ToString() == row["OrderId"].ToString())
                    {
                        var addingRows = from addingRow in dataSet.Tables["Menu"].AsEnumerable()
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

                //задаём table как datadourse of datagridvieworder


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
                if (newRowAdding == false)
                {
                    newRowAdding = true;

                    int lastRow = dataGridView1.Rows.Count - 2;

                    DataGridViewRow row = dataGridView1.Rows[lastRow];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[7, lastRow] = linkCell;

                    row.Cells["Command"].Value = "Insert";
                    
                }
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
                if (newRowAdding == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[7, rowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  // Employees changing data
        private void ReloadDataEmployees()
        {
            try
            {
                dataSet.Tables["Employees"].Clear();

                sql_DA_Employees.Fill(dataSet, "Employees");

                dataGridView1.DataSource = dataSet.Tables["Employees"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[7, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  

        private void LoadDataEmployees()
        {
            try
            {
                sql_DA_Employees = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Employees", sqlConnection);

                sqlBuilderEmployees = new SqlCommandBuilder(sql_DA_Employees);

                sqlBuilderEmployees.GetInsertCommand();
                sqlBuilderEmployees.GetUpdateCommand();
                sqlBuilderEmployees.GetDeleteCommand();

                //dataSet = new DataSet();    //Инициализируем новый экземпляр класса DataSet

                sql_DA_Employees.Fill(dataSet, "Employees");

                dataGridView1.DataSource = dataSet.Tables["Employees"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[7, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadAddOrders()
        {
            try
            {
                dataSet.Tables["Menu"].Clear();

                sql_DA_AddOrder.Fill(dataSet, "Menu");

                dataGridView2.DataSource = dataSet.Tables["Menu"];
                dataGridView3.DataSource = dataTableGridView3;
                dataGridViewOrders.DataSource = dataSet.Tables["Oredrs"];
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
                sql_DA_AddOrder = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", sqlConnection);
                sql_DA_Orders = new SqlDataAdapter("SELECT *, 'Complete' AS [Command] FROM Orders", sqlConnection);
                sql_DA_OrderedDishes = new SqlDataAdapter("SELECT * FROM OrderedDishes", sqlConnection);

                sqlBuilderOrders = new SqlCommandBuilder(sql_DA_AddOrder);

                dataTableGridView3 = new DataTable();
                dataTableGridViewOrder = new DataTable();

                sql_DA_AddOrder.Fill(dataSet, "Menu");
                sql_DA_Orders.Fill(dataSet, "Orders");
                sql_DA_OrderedDishes.Fill(dataSet, "OrderedDishes");

                dataGridView2.DataSource = dataSet.Tables["Menu"];
                dataGridViewOrders.DataSource = dataSet.Tables["Orders"];

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
                DataTable tableEmployees = dataSet.Tables["Employees"];

                bool availableWaiter = false;

                foreach (DataRow row in dataSet.Tables["Employees"].Rows)
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
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDBsqlServer"].ConnectionString);

            dataSet = new DataSet();    //Инициализируем новый экземпляр класса DataSet

            //sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString);
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            tabControl.TabPages.Remove(tabPageEmployees);
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(tabPageAutorizaion);
            tabControl.TabPages.Add(tabPageEmployees);
            tabControl.TabPages.Add(tabPageAddOrder);
            tabControl.TabPages.Add(tabPageOrders);
            LoadDataEmployees();
            LoadAddOrders();
        }

    }
}

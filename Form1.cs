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

        private SqlDataAdapter sql_DA_Employees = null;
        private SqlDataAdapter sql_DA_Menu = null;

        private DataSet dataSet = null;

        private DataTable dataTableGridView3 = null;

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
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"BirthDate LIKE '%{textBox1.Text}%'";
                    break;
                case 4:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Address LIKE '%{textBox1.Text}%'";
                    break;
                case 5:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Post LIKE '%{textBox1.Text}%'";
                    break;
                default:
                    break;
            }
        }  // Поиск сотрудников
        private void initDataTableGridView3()
        {
            //dataSet.Tables.Add(dataTableGridView3);

            dataTableGridView3 = dataSet.Tables["Menu"].Clone();

            //dataTableGridView3.Columns.Add("DishID", typeof(string));
            //dataTableGridView3.Columns.Add("DishName", typeof(string));
            //dataTableGridView3.Columns.Add("Price", typeof(string));
            //dataTableGridView3.Columns.Add("DishWeight", typeof(string));
            //dataTableGridView3.Columns.Add("Command", typeof(string));

            
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
                     

                    //dataTableGridView3.Rows[r]["DishID"] = dataGridView2.Rows[r].Cells["DishID"].Value;
                    //dataTableGridView3.Rows[r]["DishName"] = dataGridView2.Rows[r].Cells["DishName"].Value;
                    //dataTableGridView3.Rows[r]["Price"] = dataGridView2.Rows[r].Cells["Price"].Value;
                    //dataTableGridView3.Rows[r]["DishWeight"] = dataGridView2.Rows[r].Cells["DishWeight"].Value;

                    //dataGridView2.Rows[e.RowIndex].Cells["Id"]

                    //dataTableGridView3.Rows.Add();

                    //dataGridView3.DataSource = dataTableGridView3;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

                //sqlBuilder.GetInsertCommand();
                //sqlBuilder.GetUpdateCommand();
                //sqlBuilder.GetDeleteCommand();

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

                sql_DA_Employees.Fill(dataSet, "Menu");

                dataGridView2.DataSource = dataSet.Tables["Menu"];
                dataGridView3.DataSource = dataTableGridView3;

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
                sql_DA_Employees = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", sqlConnection);

                sqlBuilderEmployees = new SqlCommandBuilder(sql_DA_Employees);

                dataTableGridView3 = new DataTable();
                //sqlBuilder.GetInsertCommand();
                //sqlBuilder.GetUpdateCommand();
                //sqlBuilder.GetDeleteCommand();

                //dataSet = new DataSet();    //Инициализируем новый экземпляр класса DataSet

                sql_DA_Employees.Fill(dataSet, "Menu");

                dataGridView2.DataSource = dataSet.Tables["Menu"];

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView2[4, i] = linkCell;
                }

                initDataTableGridView3();
                ReloadAddOrders();
                //dataGridView3.ColumnCount = 5;

                //dataGridView3.Columns[0].Name = "DishID";
                //dataGridView3.Columns[1].Name = "DishName";
                //dataGridView3.Columns[2].Name = "Price";
                //dataGridView3.Columns[3].Name = "DishWeight";
                //dataGridView3.Columns[4].Name = "Command";

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
            LoadDataEmployees();
            LoadAddOrders();
        }

    }
}

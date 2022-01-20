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

        private SqlCommandBuilder sqlBuilder = null;

        private SqlDataAdapter sqlDataAdapter = null;

        private DataSet dataSet = null;

        private bool newRowAdding = false;
        public Restaurant()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            switch (logintextbox.Text)
            {
                case "admin":
                    if (passwordtextbox.Text == "adminpass") ;
                    tabControl.TabPages.Add(tabPageEmployees);
                    //tabControl.Enabled = true;
                    break;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

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

                            sqlDataAdapter.Update(dataSet, "Employees"); // Удаляем строку из Базы Данных
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

                        sqlDataAdapter.Update(dataSet, "Employees");

                        newRowAdding = false;

                    }
                    else if(task == "Update")
                    {
                        int r = e.RowIndex;

                        DataTable table = dataSet.Tables["Employees"];

                        table.Rows[r]["LastName"] = dataGridView1.Rows[r].Cells["LastName"].Value;
                        table.Rows[r]["FirstName"] = dataGridView1.Rows[r].Cells["FirstName"].Value;
                        table.Rows[r]["BirthDate"] = dataGridView1.Rows[r].Cells["BirthDate"].Value;
                        table.Rows[r]["Address"] = dataGridView1.Rows[r].Cells["Address"].Value;
                        table.Rows[r]["Phone"] = dataGridView1.Rows[r].Cells["Phone"].Value;
                        table.Rows[r]["Post"] = dataGridView1.Rows[r].Cells["Post"].Value;

                        MessageBox.Show($"Обновлено строк: {Convert.ToString(sqlDataAdapter.Update(dataSet, "Employees"))}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        sqlDataAdapter.Update(table);

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";
                    }

                    ReloadData();
                }
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
        }
        private void ReloadData()
        {
            try
            {
                dataSet.Tables["Employees"].Clear();  

                sqlDataAdapter.Fill(dataSet, "Employees");

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

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Employees", sqlConnection);

                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();    //Инициализируем новый экземпляр класса DataSet

                sqlDataAdapter.Fill(dataSet, "Employees");     

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
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDBsqlServer"].ConnectionString);
            //sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString);

            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open(); 
            }

            //tabControl.TabPages.Remove(tabPageEmployees);

            //tabControl.TabPages.Clear();

            //tabControl.TabPages.Add(tabPageAutorizaion);

            LoadData();
        }

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
        }
    }
}

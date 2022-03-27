using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant
{
    class Employees : DataBase
    {
        static public SqlCommandBuilder sqlBuilder = null;

        static public SqlDataAdapter sqlDataAdapter = null;

        static public DataGridView dataGridView = null;

        public bool newRowAdding;

        public Employees(ref DataGridView dataGridView1, ref bool newRowAdding)
        {
            try
            {
                dataGridView = dataGridView1;

                this.newRowAdding = newRowAdding;

                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Employees", sqlConnection);

                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                //dataSet = new DataSet();    //Инициализируем новый экземпляр класса DataSet

                sqlDataAdapter.Fill(dataSet, "Employees");

                dataGridView.DataSource = dataSet.Tables["Employees"];

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView[7, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ReloadDataEmployees()
        {
            try
            {
                dataSet.Tables["Employees"].Clear();

                sqlDataAdapter.Fill(dataSet, "Employees");

                dataGridView.DataSource = dataSet.Tables["Employees"];

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView[7, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataGridView1_CellContentClick(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    string task = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            dataGridView.Rows.RemoveAt(rowIndex);          // Удаляем строку из таблицы

                            dataSet.Tables["Employees"].Rows[rowIndex].Delete(); // Удаляем строку из DataBase.dataSet

                            sqlDataAdapter.Update(dataSet, "Employees"); // Удаляем строку из Базы Данных
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Employees"].NewRow();

                        row["LastName"] = dataGridView.Rows[rowIndex].Cells["LastName"].Value;
                        row["FirstName"] = dataGridView.Rows[rowIndex].Cells["FirstName"].Value;
                        row["BirthDate"] = dataGridView.Rows[rowIndex].Cells["BirthDate"].Value;
                        row["Address"] = dataGridView.Rows[rowIndex].Cells["Address"].Value;
                        row["Phone"] = dataGridView.Rows[rowIndex].Cells["Phone"].Value;
                        row["Post"] = dataGridView.Rows[rowIndex].Cells["Post"].Value;

                        dataSet.Tables["Employees"].Rows.Add(row);

                        dataSet.Tables["Employees"].Rows.RemoveAt(dataSet.Tables["Employees"].Rows.Count - 1);

                        dataGridView.Rows.RemoveAt(dataGridView.Rows.Count - 2);

                        dataGridView.Rows[e.RowIndex].Cells[7].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Employees");

                        newRowAdding = false;

                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        DataTable table = dataSet.Tables["Employees"];

                        table.Rows[r]["LastName"] = dataGridView.Rows[r].Cells["LastName"].Value;
                        table.Rows[r]["FirstName"] = dataGridView.Rows[r].Cells["FirstName"].Value;
                        table.Rows[r]["BirthDate"] = dataGridView.Rows[r].Cells["BirthDate"].Value;
                        table.Rows[r]["Address"] = dataGridView.Rows[r].Cells["Address"].Value;
                        table.Rows[r]["Phone"] = dataGridView.Rows[r].Cells["Phone"].Value;
                        table.Rows[r]["Post"] = dataGridView.Rows[r].Cells["Post"].Value;

                        MessageBox.Show($"Обновлено строк: {Convert.ToString(sqlDataAdapter.Update(dataSet, "Employees"))}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        sqlDataAdapter.Update(table);

                        dataGridView.Rows[e.RowIndex].Cells[7].Value = "Delete";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                newRowAdding = false;
            }
        }

        public void SearchEmployee(String Text, int Index)
        {
            switch (Index)
            {
                case 0:
                    (dataGridView.DataSource as DataTable).DefaultView.RowFilter = $"Id LIKE '%{Text}%'";
                    break;
                case 1:
                    (dataGridView.DataSource as DataTable).DefaultView.RowFilter = $"LastName LIKE '%{Text}%'";
                    break;
                case 2:
                    (dataGridView.DataSource as DataTable).DefaultView.RowFilter = $"FirstName LIKE '%{Text}%'";
                    break;
                case 3:
                    (dataGridView.DataSource as DataTable).DefaultView.RowFilter = $"Address LIKE '%{Text}%'";
                    break;
                case 4:
                    (dataGridView.DataSource as DataTable).DefaultView.RowFilter = $"Post LIKE '%{Text}%'";
                    break;
                default:
                    break;
            }
        }
        public void UserAddedRow()
        {
            try
            {
                if (newRowAdding == false)
                {
                    newRowAdding = true;

                    int lastRow = dataGridView.Rows.Count - 2;

                    DataGridViewRow row = dataGridView.Rows[lastRow];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView[7, lastRow] = linkCell;

                    row.Cells["Command"].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CellValueChanged()
        {
            try
            {
                if (newRowAdding == false)
                {
                    int rowIndex = dataGridView.SelectedCells[0].RowIndex;

                    DataGridViewRow editingRow = dataGridView.Rows[rowIndex];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView[7, rowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


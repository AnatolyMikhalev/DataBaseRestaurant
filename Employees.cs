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
        public void LoadDataEmployees(ref DataGridView dataGridView1)
        {
            try
            {
                dataGridView = dataGridView1;

                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Employees", sqlConnection);

                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();    //Инициализируем новый экземпляр класса DataSet

                sqlDataAdapter.Fill(DataBase.dataSet, "Employees");

                dataGridView.DataSource = DataBase.dataSet.Tables["Employees"];

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

        public void dataGridView1_CellContentClick(DataGridViewCellEventArgs e, bool newRowAdding)
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

                        DataBase.dataSet.Tables["Employees"].Rows[rowIndex].Delete(); // Удаляем строку из DataBase.dataSet

                        Employees.sqlDataAdapter.Update(DataBase.dataSet, "Employees"); // Удаляем строку из Базы Данных
                    }
                }
                else if (task == "Insert")
                {
                    int rowIndex = dataGridView.Rows.Count - 2;

                    DataRow row = DataBase.dataSet.Tables["Employees"].NewRow();

                    row["LastName"] = dataGridView.Rows[rowIndex].Cells["LastName"].Value;
                    row["FirstName"] = dataGridView.Rows[rowIndex].Cells["FirstName"].Value;
                    row["BirthDate"] = dataGridView.Rows[rowIndex].Cells["BirthDate"].Value;
                    row["Address"] = dataGridView.Rows[rowIndex].Cells["Address"].Value;
                    row["Phone"] = dataGridView.Rows[rowIndex].Cells["Phone"].Value;
                    row["Post"] = dataGridView.Rows[rowIndex].Cells["Post"].Value;

                    DataBase.dataSet.Tables["Employees"].Rows.Add(row);

                    DataBase.dataSet.Tables["Employees"].Rows.RemoveAt(DataBase.dataSet.Tables["Employees"].Rows.Count - 1);

                    dataGridView.Rows.RemoveAt(dataGridView.Rows.Count - 2);

                    dataGridView.Rows[e.RowIndex].Cells[7].Value = "Delete";

                    Employees.sqlDataAdapter.Update(DataBase.dataSet, "Employees");

                    newRowAdding = false;

                }
                else if (task == "Update")
                {
                    int r = e.RowIndex;

                    DataTable table = DataBase.dataSet.Tables["Employees"];

                    table.Rows[r]["LastName"] = dataGridView.Rows[r].Cells["LastName"].Value;
                    table.Rows[r]["FirstName"] = dataGridView.Rows[r].Cells["FirstName"].Value;
                    table.Rows[r]["BirthDate"] = dataGridView.Rows[r].Cells["BirthDate"].Value;
                    table.Rows[r]["Address"] = dataGridView.Rows[r].Cells["Address"].Value;
                    table.Rows[r]["Phone"] = dataGridView.Rows[r].Cells["Phone"].Value;
                    table.Rows[r]["Post"] = dataGridView.Rows[r].Cells["Post"].Value;

                    MessageBox.Show($"Обновлено строк: {Convert.ToString(Employees.sqlDataAdapter.Update(DataBase.dataSet, "Employees"))}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Employees.sqlDataAdapter.Update(table);

                    dataGridView.Rows[e.RowIndex].Cells[7].Value = "Delete";
                }
            }
        }

        public void ReloadDataEmployees()
        {
            try
            {
                DataBase.dataSet.Tables["Employees"].Clear();

                Employees.sqlDataAdapter.Fill(DataBase.dataSet, "Employees");

                dataGridView.DataSource = DataBase.dataSet.Tables["Employees"];

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
    }
}

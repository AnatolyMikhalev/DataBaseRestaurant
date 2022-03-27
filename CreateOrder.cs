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
    class CreateOrder : DataBase
    {
        static public SqlCommandBuilder sqlBuilderAddOrder = null;
        static public SqlCommandBuilder sqlBuilderOrders = null;

        static public SqlDataAdapter sql_DA_AddOrder = null;
        static public SqlDataAdapter sql_DA_Orders = null;
        static public SqlDataAdapter sql_DA_OrderedDishes = null;

        public DataGridView dataGridView2 = null;
        public DataGridView dataGridView3 = null;
        public DataGridView dataGridViewOrder = null;
        public DataGridView dataGridViewOrders = null;

        public DataTable dataTableGridView3 = null;
        public DataTable dataTableGridViewOrder = null;

        public bool newRowAdding;

        public CreateOrder(ref DataGridView dataGridView2, ref DataGridView dataGridView3, ref DataGridView dataGridViewOrder, ref DataGridView dataGridViewOrders)
        {
            try
            {
                this.dataGridView2 = dataGridView2;
                this.dataGridView3 = dataGridView3;
                this.dataGridViewOrder = dataGridViewOrder;
                this.dataGridViewOrders = dataGridViewOrders;

                sql_DA_AddOrder = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", sqlConnection);
                sql_DA_Orders = new SqlDataAdapter("SELECT *, 'Complete' AS [Command] FROM Orders", sqlConnection);
                sql_DA_OrderedDishes = new SqlDataAdapter("SELECT * FROM OrderedDishes", sqlConnection);

                sqlBuilderAddOrder = new SqlCommandBuilder(sql_DA_AddOrder);
                sqlBuilderOrders = new SqlCommandBuilder(sql_DA_Orders);

                dataTableGridView3 = new DataTable();
                dataTableGridViewOrder = new DataTable();


                sqlBuilderOrders.GetInsertCommand();
                sqlBuilderOrders.GetUpdateCommand();
                sqlBuilderOrders.GetDeleteCommand();

                sql_DA_AddOrder.Fill(dataSet, "Menu");
                sql_DA_Orders.Fill(dataSet, "Orders");
                sql_DA_OrderedDishes.Fill(dataSet, "OrderedDishes");

                this.dataGridView2.DataSource = dataSet.Tables["Menu"];
                this.dataGridViewOrders.DataSource = dataSet.Tables["Orders"];


                InitDataTableGridView3();
                InitDataTableGridViewOrder();
                ReloadAddOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitDataTableGridView3()
        {
            dataTableGridView3 = dataSet.Tables["Menu"].Clone();
        }
        private void InitDataTableGridViewOrder()
        {
            dataTableGridViewOrder = dataSet.Tables["Menu"].Clone();
        }
        public void ReloadAddOrders()
        {
            try
            {
                dataSet.Tables["Menu"].Clear();
                dataSet.Tables["Orders"].Clear();

                sql_DA_AddOrder.Fill(dataSet, "Menu");
                sql_DA_Orders.Fill(dataSet, "Orders");

                dataGridView2.DataSource = dataSet.Tables["Menu"];
                dataGridView3.DataSource = dataTableGridView3;
                dataGridViewOrders.DataSource = dataSet.Tables["Orders"];
                dataGridViewOrder.DataSource = dataTableGridViewOrder;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView2[4, i] = linkCell;
                }
                for (int i = 0; i < this.dataGridViewOrders.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridViewOrders[3, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        public void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        public void Button2_Click(object sender, EventArgs e, String Id, String Table)
        {
            try
            {
                DataTable tableEmployees = dataSet.Tables["Employees"];

                bool availableWaiter = false;

                foreach (DataRow row in dataSet.Tables["Employees"].Rows)
                {
                    if (row["Id"].ToString() == Id && row["Post"].ToString() == "Официант")
                    {
                        availableWaiter = true;
                        break;
                    }
                }
                if (!availableWaiter)
                {
                    throw new Exception("Официанта с заданным Id не существует");
                }


                DataRow newAddingRow = dataSet.Tables["Orders"].NewRow();

                newAddingRow["Table"] = Convert.ToInt32(Table);
                newAddingRow["WaiterId"] = Convert.ToInt32(Id);

                dataSet.Tables["Orders"].Rows.Add(newAddingRow);

                sql_DA_Orders.Update(dataSet, "Orders");
                
                ReloadAddOrders();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;

                dataTableGridViewOrder.Clear();

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
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

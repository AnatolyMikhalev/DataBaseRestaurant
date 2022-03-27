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

                sql_DA_AddOrder = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", DataBase.sqlConnection);
                sql_DA_Orders = new SqlDataAdapter("SELECT *, 'Complete' AS [Command] FROM Orders", DataBase.sqlConnection);
                sql_DA_OrderedDishes = new SqlDataAdapter("SELECT * FROM OrderedDishes", DataBase.sqlConnection);

                sqlBuilderOrders = new SqlCommandBuilder(sql_DA_AddOrder);

                dataTableGridView3 = new DataTable();
                dataTableGridViewOrder = new DataTable();

                sql_DA_AddOrder.Fill(DataBase.dataSet, "Menu");
                sql_DA_Orders.Fill(DataBase.dataSet, "Orders");
                sql_DA_OrderedDishes.Fill(DataBase.dataSet, "OrderedDishes");

                this.dataGridView2.DataSource = DataBase.dataSet.Tables["Menu"];
                this.dataGridViewOrders.DataSource = DataBase.dataSet.Tables["Orders"];

                for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    this.dataGridView2[4, i] = linkCell;
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
        private void initDataTableGridView3()
        {
            dataTableGridView3 = DataBase.dataSet.Tables["Menu"].Clone();
        }
        private void initDataTableGridViewOrder()
        {
            dataTableGridViewOrder = DataBase.dataSet.Tables["Menu"].Clone();
        }
        public void ReloadAddOrders()
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

        public void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        public void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        public void button2_Click(object sender, EventArgs e, String Id)
        {
            try
            {
                DataTable tableEmployees = DataBase.dataSet.Tables["Employees"];

                bool availableWaiter = false;

                foreach (DataRow row in DataBase.dataSet.Tables["Employees"].Rows)
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

                foreach (DataRow row in DataBase.dataSet.Tables["Employees"].Rows)
                {
                    if (row["Id"].ToString() == Id && row["Post"].ToString() == "Официант")
                    {
                        availableWaiter = true;
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
    }

}

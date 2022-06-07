using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.Objects.SqlClient;

namespace Restaurant
{
    class CreateOrder : DataBase
    {
        static public SqlCommandBuilder sqlBuilderMenu = null;
        static public SqlCommandBuilder sqlBuilderOrders = null;
        static public SqlCommandBuilder sqlBuilderOrderedDishes = null;

        static public SqlDataAdapter sql_DA_Menu = null;
        static public SqlDataAdapter sql_DA_Orders = null;
        static public SqlDataAdapter sql_DA_OrderedDishes = null;

        public DataGridView dataGridViewMenu = null;
        public DataGridView dataGridViewSelectDishes = null;
        public DataGridView dataGridViewOrder = null;
        public DataGridView dataGridViewOrders = null;

        public DataTable dataTableGridViewSelectDishes = null;
        public DataTable dataTableGridViewOrder = null;

        public bool newRowAdding;

        public CreateOrder(ref DataGridView dataGridViewMenu, ref DataGridView dataGridViewSelectDishes, ref DataGridView dataGridViewOrder, ref DataGridView dataGridViewOrders)
        {
            try
            {
                this.dataGridViewMenu = dataGridViewMenu;
                this.dataGridViewSelectDishes = dataGridViewSelectDishes;
                this.dataGridViewOrder = dataGridViewOrder;
                this.dataGridViewOrders = dataGridViewOrders;

                sql_DA_Menu = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", sqlConnection);
                sql_DA_Orders = new SqlDataAdapter("SELECT *, 'Complete' AS [Command] FROM Orders", sqlConnection);
                sql_DA_OrderedDishes = new SqlDataAdapter("SELECT * FROM OrderedDishes", sqlConnection);

                sqlBuilderMenu = new SqlCommandBuilder(sql_DA_Menu);
                sqlBuilderOrders = new SqlCommandBuilder(sql_DA_Orders);
                sqlBuilderOrderedDishes = new SqlCommandBuilder(sql_DA_OrderedDishes);

                dataTableGridViewSelectDishes = new DataTable();
                dataTableGridViewOrder = new DataTable();


                sqlBuilderOrders.GetInsertCommand();
                sqlBuilderOrders.GetUpdateCommand();
                sqlBuilderOrders.GetDeleteCommand();

                sqlBuilderOrderedDishes.GetInsertCommand();
                sqlBuilderOrderedDishes.GetUpdateCommand();
                sqlBuilderOrderedDishes.GetDeleteCommand();

                sql_DA_Menu.Fill(dataSet, "Menu");
                sql_DA_Orders.Fill(dataSet, "Orders");
                sql_DA_OrderedDishes.Fill(dataSet, "OrderedDishes");

                this.dataGridViewMenu.DataSource = dataSet.Tables["Menu"];
                this.dataGridViewOrders.DataSource = dataSet.Tables["Orders"];


                InitDataTableGridViewSelectDishes();
                InitDataTableGridViewOrder();
                ReloadAddOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitDataTableGridViewSelectDishes()
        {
            dataTableGridViewSelectDishes = dataSet.Tables["Menu"].Clone();
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
                dataSet.Tables["OrderedDishes"].Clear();

                sql_DA_Menu.Fill(dataSet, "Menu");
                sql_DA_Orders.Fill(dataSet, "Orders");
                sql_DA_OrderedDishes.Fill(dataSet, "OrderedDishes");

                dataGridViewMenu.DataSource = dataSet.Tables["Menu"];
                dataGridViewSelectDishes.DataSource = dataTableGridViewSelectDishes;
                dataGridViewOrders.DataSource = dataSet.Tables["Orders"];
                dataGridViewOrder.DataSource = dataTableGridViewOrder;

                for (int i = 0; i < dataGridViewMenu.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridViewMenu[4, i] = linkCell;
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

                    DataRow row = dataTableGridViewSelectDishes.NewRow();

                    row["DishID"] = dataGridViewMenu.Rows[r].Cells["DishID"].Value;
                    row["DishName"] = dataGridViewMenu.Rows[r].Cells["DishName"].Value;
                    row["Price"] = dataGridViewMenu.Rows[r].Cells["Price"].Value;
                    row["DishWeight"] = dataGridViewMenu.Rows[r].Cells["DishWeight"].Value;
                    row["Command"] = "Remove";

                    dataTableGridViewSelectDishes.Rows.Add(row);
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
                        dataTableGridViewSelectDishes.Rows.RemoveAt(e.RowIndex);          // Удаляем строку из таблицы
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

                var addingRows = dataTableGridViewSelectDishes.AsEnumerable().
                                 Select(order => new
                                 {
                                     DishID = order.Field<int>("DishID"),
                                 });  


                foreach (var addingRow in addingRows)
                {
                    DataRow newAddingRow1 = dataSet.Tables["OrderedDishes"].NewRow();

                    newAddingRow1["DishID"] = Convert.ToInt32(addingRow.DishID);
                    newAddingRow1["OrderID"] = Convert.ToInt32(dataSet.Tables["Orders"].Rows[dataSet.Tables["Orders"].Rows.Count - 1].ItemArray[0]);

                    dataSet.Tables["OrderedDishes"].Rows.Add(newAddingRow1);

                }

                sql_DA_OrderedDishes.Update(dataSet, "OrderedDishes");
                ReloadAddOrders();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddOrderesDish(Int32 DishID, Int32 OrderId)
        {
            DataRow newAddingRow = dataSet.Tables["OrderedDishes"].NewRow();

            newAddingRow["DishID"] = DishID;
            newAddingRow["OrderID"] = OrderId;

            dataSet.Tables["OrderedDishes"].Rows.Add(newAddingRow);
            sql_DA_OrderedDishes.Update(dataSet, "OrderedDishes");
            ReloadAddOrders();
        }

        public void DataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;

                dataTableGridViewOrder.Clear();



                //foreach (DataRow row in dataSet.Tables["OrderedDishes"].Rows)
                //{
                //    if (dataSet.Tables["Orders"].Rows[r]["Id"].ToString() == row["OrderId"].ToString())
                //    {
                //        var addingRows = from addingRow in dataSet.Tables["Menu"].AsEnumerable()
                //                         where (int)addingRow["DishId"] == (int)row["DishId"]
                //                         select addingRow;

                //        foreach (var addingRow in addingRows)
                //        {
                //            DataRow newAddingRow = dataTableGridViewOrder.NewRow();

                //            newAddingRow["DishID"] = addingRow["DishID"];
                //            newAddingRow["DishName"] = addingRow["DishName"];
                //            newAddingRow["Price"] = addingRow["Price"];
                //            newAddingRow["DishWeight"] = addingRow["DishWeight"];

                //            dataTableGridViewOrder.Rows.Add(newAddingRow);
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

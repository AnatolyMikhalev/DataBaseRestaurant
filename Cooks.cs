using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant
{
    class Cooks : DataBase
    {
        static public SqlCommandBuilder sqlBuilderMenuControl = null;

        static public SqlDataAdapter sql_DA_MenuControl = null;

        public DataGridView dataGridViewMenuControl = null;

        public DataTable dataTableGridViewMenuControl = null;

        public Cooks(ref DataGridView dataGridViewMenuControl)
        {
            try
            {
                this.dataGridViewMenuControl = dataGridViewMenuControl;

                sql_DA_MenuControl = new SqlDataAdapter("SELECT *, 'Add' AS [Command] FROM Menu", sqlConnection);

                sqlBuilderMenuControl = new SqlCommandBuilder(sql_DA_MenuControl);

                dataTableGridViewMenuControl = new DataTable(); 
                 
                sqlBuilderMenuControl.GetInsertCommand();
                sqlBuilderMenuControl.GetUpdateCommand();
                sqlBuilderMenuControl.GetDeleteCommand();

                sql_DA_MenuControl.Fill(dataSet, "Menu");

                this.dataGridViewMenuControl.DataSource = dataSet.Tables["Menu"];

                dataTableGridViewMenuControl = dataSet.Tables["Menu"].Clone();
                
                ReloadCooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ReloadCooks()
        {
            try
            {
                dataSet.Tables["Menu"].Clear();

                sql_DA_MenuControl.Fill(dataSet, "Menu");

                dataGridViewMenuControl.DataSource = dataSet.Tables["Menu"];

                for (int i = 0; i < dataGridViewMenuControl.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridViewMenuControl[4, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ColumnIndex == 4)
        //        {
        //            int r = e.RowIndex;

        //            DataRow row = dataTableGridViewSelectDishes.NewRow();

        //            row["DishID"] = dataGridViewMenu.Rows[r].Cells["DishID"].Value;
        //            row["DishName"] = dataGridViewMenu.Rows[r].Cells["DishName"].Value;
        //            row["Price"] = dataGridViewMenu.Rows[r].Cells["Price"].Value;
        //            row["DishWeight"] = dataGridViewMenu.Rows[r].Cells["DishWeight"].Value;
        //            row["Command"] = "Remove";

        //            dataTableGridViewSelectDishes.Rows.Add(row);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //public void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ColumnIndex == 4)
        //        {
        //            if (MessageBox.Show("Удалить блюдо из заказа?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        //                    == DialogResult.Yes)
        //            {
        //                dataTableGridViewSelectDishes.Rows.RemoveAt(e.RowIndex);          // Удаляем строку из таблицы
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //public void Button2_Click(object sender, EventArgs e, String Id, String Table)
        //{
        //    try
        //    {
        //        DataTable tableEmployees = dataSet.Tables["Employees"];

        //        bool availableWaiter = false;

        //        foreach (DataRow row in dataSet.Tables["Employees"].Rows)
        //        {
        //            if (row["Id"].ToString() == Id && row["Post"].ToString() == "Официант")
        //            {
        //                availableWaiter = true;
        //                break;
        //            }
        //        }
        //        if (!availableWaiter)
        //        {
        //            throw new Exception("Официанта с заданным Id не существует");
        //        }

        //        DataRow newAddingRow = dataSet.Tables["Orders"].NewRow();

        //        newAddingRow["Table"] = Convert.ToInt32(Table);
        //        newAddingRow["WaiterId"] = Convert.ToInt32(Id);

        //        dataSet.Tables["Orders"].Rows.Add(newAddingRow);

        //        sql_DA_Orders.Update(dataSet, "Orders");

        //        ReloadAddOrders();

        //        var addingRows = dataTableGridViewSelectDishes.AsEnumerable().
        //                         Select(order => new
        //                         {
        //                             DishID = order.Field<int>("DishID"),
        //                         });


        //        foreach (var addingRow in addingRows)
        //        {
        //            DataRow newAddingRow1 = dataSet.Tables["OrderedDishes"].NewRow();

        //            newAddingRow1["DishID"] = Convert.ToInt32(addingRow.DishID);
        //            newAddingRow1["OrderID"] = Convert.ToInt32(dataSet.Tables["Orders"].Rows[dataSet.Tables["Orders"].Rows.Count - 1].ItemArray[0]);

        //            dataSet.Tables["OrderedDishes"].Rows.Add(newAddingRow1);

        //        }

        //        sql_DA_OrderedDishes.Update(dataSet, "OrderedDishes");
        //        ReloadAddOrders();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //public void DataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        int r = e.RowIndex;
        //        string OrderId = dataSet.Tables["Orders"].Rows[r]["Id"].ToString();

        //        dataTableGridViewOrder.Clear();

        //        var addingRows = from Order in dataSet.Tables["Orders"].AsEnumerable()
        //                         join DishInOrder in dataSet.Tables["OrderedDishes"].AsEnumerable()
        //                         on Order["Id"].ToString() equals DishInOrder["OrderId"].ToString()
        //                         join Dish in dataSet.Tables["Menu"].AsEnumerable()
        //                         on DishInOrder["DishId"].ToString() equals Dish["DishId"].ToString()
        //                         where Order["Id"].ToString() == OrderId
        //                         select Dish;

        //        foreach (var addingRow in addingRows)
        //        {
        //            Console.WriteLine($"{addingRow["DishId"]} - {addingRow["DishName"]} ");

        //            DataRow row = dataTableGridViewOrder.NewRow();

        //            row["DishID"] = addingRow["DishID"];
        //            row["DishName"] = addingRow["DishName"];
        //            row["Price"] = addingRow["Price"];
        //            row["DishWeight"] = addingRow["DishWeight"];
        //            row["Command"] = "Command";

        //            dataTableGridViewOrder.Rows.Add(row);
        //        }
        //        Console.WriteLine("---------------------------------");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //public void DataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        //------------------------ Удаление заказа из Orders

        //        int r = e.RowIndex;
        //        string OrderId = dataSet.Tables["Orders"].Rows[r]["Id"].ToString();

        //        dataTableGridViewOrder.Rows.RemoveAt(r);          // Удаляем строку из таблицы

        //        dataSet.Tables["Orders"].Rows[r].Delete(); // Удаляем строку из DataBase.dataSet

        //        sql_DA_Orders.Update(dataSet, "Orders"); // Удаляем строку из Базы Данных

        //        ReloadAddOrders();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }
}

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

        public bool newRowAdding;

        public Cooks(ref DataGridView dataGridViewMenuControl)
        {
            try
            {
                this.dataGridViewMenuControl = dataGridViewMenuControl;

                sql_DA_MenuControl = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Menu", sqlConnection);

                sqlBuilderMenuControl = new SqlCommandBuilder(sql_DA_MenuControl);

                dataTableGridViewMenuControl = new DataTable();

                sqlBuilderMenuControl.GetInsertCommand();
                sqlBuilderMenuControl.GetUpdateCommand();
                sqlBuilderMenuControl.GetDeleteCommand();

                sql_DA_MenuControl.Fill(dataSet, "Menu");

                this.dataGridViewMenuControl.DataSource = dataSet.Tables["Menu"];

                dataTableGridViewMenuControl = dataSet.Tables["Menu"].Clone();

                ReloadCooks();

                for (int i = 0; i < dataGridViewMenuControl.Rows.Count - 1; i++)
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

                //TabFocus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    string task = dataGridViewMenuControl.Rows[e.RowIndex].Cells[4].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            dataGridViewMenuControl.Rows.RemoveAt(rowIndex);          // Удаляем строку из таблицы

                            dataSet.Tables["Menu"].Rows[rowIndex].Delete(); // Удаляем строку из DataBase.dataSet

                            sql_DA_MenuControl.Update(dataSet, "Menu"); // Удаляем строку из Базы Данных
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridViewMenuControl.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Menu"].NewRow();

                        //row["DishId"] = dataGridViewMenuControl.Rows[rowIndex].Cells["DishId"].Value;
                        row["DishName"] = dataGridViewMenuControl.Rows[rowIndex].Cells["DishName"].Value;
                        row["Price"] = dataGridViewMenuControl.Rows[rowIndex].Cells["Price"].Value;
                        row["DishWeight"] = dataGridViewMenuControl.Rows[rowIndex].Cells["DishWeight"].Value;

                        dataSet.Tables["Menu"].Rows.Add(row);

                        dataSet.Tables["Menu"].Rows.RemoveAt(dataSet.Tables["Menu"].Rows.Count - 1);

                        dataGridViewMenuControl.Rows.RemoveAt(dataGridViewMenuControl.Rows.Count - 2);

                        dataGridViewMenuControl.Rows[e.RowIndex].Cells[4].Value = "Delete";

                        sql_DA_MenuControl.Update(dataSet, "Menu");

                        newRowAdding = false;

                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        DataTable table = dataSet.Tables["Menu"];

                        table.Rows[r]["DishId"] = dataGridViewMenuControl.Rows[r].Cells["DishId"].Value;
                        table.Rows[r]["DishName"] = dataGridViewMenuControl.Rows[r].Cells["DishName"].Value;
                        table.Rows[r]["Price"] = dataGridViewMenuControl.Rows[r].Cells["Price"].Value;
                        table.Rows[r]["DishWeight"] = dataGridViewMenuControl.Rows[r].Cells["DishWeight"].Value;

                        MessageBox.Show($"Обновлено строк: {Convert.ToString(sql_DA_MenuControl.Update(dataSet, "Menu"))}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        sql_DA_MenuControl.Update(table);

                        dataGridViewMenuControl.Rows[e.RowIndex].Cells[4].Value = "Delete";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                newRowAdding = false;
            }

        }  //TODO: реализовать обработку нажатий ячеек



        public void SearchDish(String Text, int Index)
        {
            switch (Index)
            {
                case 0:
                    (dataGridViewMenuControl.DataSource as DataTable).DefaultView.RowFilter = $"Id LIKE '%{Text}%'";
                    break;
                case 1:
                    (dataGridViewMenuControl.DataSource as DataTable).DefaultView.RowFilter = $"LastName LIKE '%{Text}%'";
                    break;
                case 2:
                    (dataGridViewMenuControl.DataSource as DataTable).DefaultView.RowFilter = $"FirstName LIKE '%{Text}%'";
                    break;
                case 3:
                    (dataGridViewMenuControl.DataSource as DataTable).DefaultView.RowFilter = $"Address LIKE '%{Text}%'";
                    break;
                case 4:
                    (dataGridViewMenuControl.DataSource as DataTable).DefaultView.RowFilter = $"Post LIKE '%{Text}%'";
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

                    int lastRow = dataGridViewMenuControl.Rows.Count - 2;

                    DataGridViewRow row = dataGridViewMenuControl.Rows[lastRow];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridViewMenuControl[4, lastRow] = linkCell;

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
                    int rowIndex = dataGridViewMenuControl.SelectedCells[0].RowIndex;

                    DataGridViewRow editingRow = dataGridViewMenuControl.Rows[rowIndex];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridViewMenuControl[4, rowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TabFocus()
        {
            for (int i = 0; i < dataGridViewMenuControl.Rows.Count - 1; i++ )
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                dataGridViewMenuControl[4, i] = linkCell;

                dataGridViewMenuControl[4, i].Value = "Delete";
            }
        }

        //TODO: реализовать удаление блюд из меню

        //TODO: реализовать изменение блюд в меню

        //TODO: реализовать добавление блюд в меню

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

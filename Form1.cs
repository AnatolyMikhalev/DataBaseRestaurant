﻿using System;
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
        private Employees employees = null;
        private CreateOrder createOrder = null;
        private Cooks cooks = null;



        //private DataBase.sqlConnection DataBase.sqlConnection = null;



        private DataTable dataTableGridView3 = null;
        private DataTable dataTableGridViewOrder = null;

        public bool newRowAdding = false;

        public Restaurant()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            employees.SearchEmployee(textBox1.Text, comboBox1.SelectedIndex);
        }  // Поиск сотрудников
        private void initDataTableGridView3()
        {
            dataTableGridView3 = DataBase.dataSet.Tables["Menu"].Clone();
        }
        private void initDataTableGridViewOrder()
        {
            dataTableGridViewOrder = DataBase.dataSet.Tables["Menu"].Clone();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (logintextbox.Text)
            {
                case "admin":
                    if (passwordtextbox.Text == "adminpass")
                    {
                        tabControl.TabPages.Add(tabPageEmployees);
                        //employees.LoadDataEmployees();
                    }
                    //tabControl.Enabled = true;
                    break;
            }
        }   // Password
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            employees.ReloadDataEmployees();
            createOrder.ReloadAddOrders();
        } // Reload
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            employees.DataGridView1_CellContentClick(e);
            employees.ReloadDataEmployees();
        }  // Employees Insert Update Delete
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            employees.UserAddedRow();
        } // Employees adding row

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            employees.CellValueChanged();
        }  // Employees
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            createOrder.DataGridView2_CellContentClick(sender, e);
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            createOrder.DataGridView3_CellContentClick(sender, e);
        }
        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            createOrder.DataGridViewOrders_CellContentClick(sender, e);
        }
        private void dataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            createOrder.DataGridViewOrders_CellClick(sender, e);
        } //changing data
        private void button2_Click(object sender, EventArgs e)
        {
            createOrder.Button2_Click(sender, e, textBox3.Text, textBox4.Text);
        }
        private void dataGridViewMenuControl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cooks.CellContentClick(sender, e);
        }
        private void dataGridViewMenuControl_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            cooks.UserAddedRow();
        }
        private void dataGridViewMenuControl_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            cooks.CellValueChanged();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //DataBase.sqlConnection = new DataBase.sqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDBsqlServer"].ConnectionString);
            //DataBase.sqlConnection = new DataBase.sqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString);

            employees = new Employees(ref dataGridView1, ref newRowAdding);
            createOrder = new CreateOrder(ref dataGridViewMenu, ref dataGridViewSelectDishes, ref dataGridViewOrder, ref dataGridViewOrders);
            cooks = new Cooks(ref dataGridViewMenuControl);

            //createOrder.TabFocus();
            //cooks.TabFocus();

            DataBase.OpenConnection();

            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(tabPageAutorizaion);
            tabControl.TabPages.Add(tabPageEmployees);
            tabControl.TabPages.Add(tabPageAddOrder);
            tabControl.TabPages.Add(tabPageOrders);
            tabControl.TabPages.Add(tabPageCooks);
        }

        private void dataGridViewMenu_Click(object sender, EventArgs e)
        {
            createOrder.TabFocus();
        }

        private void dataGridViewMenuControl_Click(object sender, EventArgs e)
        {
            cooks.TabFocus();
        }
    }
}

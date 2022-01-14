
namespace Restaurant
{
    partial class Restaurant
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPageOrders = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnEmployeeID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBirthDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAdress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button3 = new System.Windows.Forms.Button();
            this.tabPageAutorizaion = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.passwordtextbox = new System.Windows.Forms.TextBox();
            this.logintextbox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAddOrder = new System.Windows.Forms.TabPage();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageOrders.SuspendLayout();
            this.tabPageEmployees.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPageAutorizaion.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageAddOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageOrders
            // 
            this.tabPageOrders.Controls.Add(this.listView2);
            this.tabPageOrders.Location = new System.Drawing.Point(4, 22);
            this.tabPageOrders.Name = "tabPageOrders";
            this.tabPageOrders.Size = new System.Drawing.Size(792, 434);
            this.tabPageOrders.TabIndex = 3;
            this.tabPageOrders.Text = "Заказы";
            this.tabPageOrders.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(792, 434);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ProductName";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "QuantityUnit";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "UnitPrice";
            // 
            // tabPageEmployees
            // 
            this.tabPageEmployees.Controls.Add(this.tableLayoutPanel2);
            this.tabPageEmployees.Location = new System.Drawing.Point(4, 22);
            this.tabPageEmployees.Name = "tabPageEmployees";
            this.tabPageEmployees.Size = new System.Drawing.Size(792, 434);
            this.tabPageEmployees.TabIndex = 2;
            this.tabPageEmployees.Text = "Сотрудники";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 434);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEmployeeID,
            this.columnLastName,
            this.columnFirstName,
            this.columnBirthDate,
            this.columnAdress,
            this.columnPhone,
            this.columnPost});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(786, 384);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnEmployeeID
            // 
            this.columnEmployeeID.Text = "Id";
            this.columnEmployeeID.Width = 48;
            // 
            // columnLastName
            // 
            this.columnLastName.Text = "Фамилия";
            this.columnLastName.Width = 93;
            // 
            // columnFirstName
            // 
            this.columnFirstName.Text = "Имя";
            this.columnFirstName.Width = 88;
            // 
            // columnBirthDate
            // 
            this.columnBirthDate.Text = "Дата рождения";
            this.columnBirthDate.Width = 136;
            // 
            // columnAdress
            // 
            this.columnAdress.Text = "Адрес";
            this.columnAdress.Width = 145;
            // 
            // columnPhone
            // 
            this.columnPhone.Text = "Телефон";
            this.columnPhone.Width = 102;
            // 
            // columnPost
            // 
            this.columnPost.Text = "Должность";
            this.columnPost.Width = 115;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 393);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 38);
            this.button3.TabIndex = 1;
            this.button3.Text = "SELECT";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPageAutorizaion
            // 
            this.tabPageAutorizaion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPageAutorizaion.Controls.Add(this.label2);
            this.tabPageAutorizaion.Controls.Add(this.label1);
            this.tabPageAutorizaion.Controls.Add(this.button1);
            this.tabPageAutorizaion.Controls.Add(this.passwordtextbox);
            this.tabPageAutorizaion.Controls.Add(this.logintextbox);
            this.tabPageAutorizaion.Location = new System.Drawing.Point(4, 22);
            this.tabPageAutorizaion.Name = "tabPageAutorizaion";
            this.tabPageAutorizaion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAutorizaion.Size = new System.Drawing.Size(792, 434);
            this.tabPageAutorizaion.TabIndex = 0;
            this.tabPageAutorizaion.Text = "Авторизация";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Пароль:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Логин:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 61);
            this.button1.TabIndex = 6;
            this.button1.Text = "Авторизация";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // passwordtextbox
            // 
            this.passwordtextbox.Location = new System.Drawing.Point(313, 163);
            this.passwordtextbox.Name = "passwordtextbox";
            this.passwordtextbox.Size = new System.Drawing.Size(125, 20);
            this.passwordtextbox.TabIndex = 1;
            // 
            // logintextbox
            // 
            this.logintextbox.Location = new System.Drawing.Point(313, 137);
            this.logintextbox.Name = "logintextbox";
            this.logintextbox.Size = new System.Drawing.Size(125, 20);
            this.logintextbox.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAutorizaion);
            this.tabControl.Controls.Add(this.tabPageEmployees);
            this.tabControl.Controls.Add(this.tabPageOrders);
            this.tabControl.Controls.Add(this.tabPageAddOrder);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 460);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageAddOrder
            // 
            this.tabPageAddOrder.Controls.Add(this.listView3);
            this.tabPageAddOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddOrder.Name = "tabPageAddOrder";
            this.tabPageAddOrder.Size = new System.Drawing.Size(792, 434);
            this.tabPageAddOrder.TabIndex = 4;
            this.tabPageAddOrder.Text = "Добавить заказ";
            this.tabPageAddOrder.UseVisualStyleBackColor = true;
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView3.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView3.GridLines = true;
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(0, 0);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(229, 434);
            this.listView3.TabIndex = 0;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // Restaurant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.tabControl);
            this.Name = "Restaurant";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPageOrders.ResumeLayout(false);
            this.tabPageEmployees.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPageAutorizaion.ResumeLayout(false);
            this.tabPageAutorizaion.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageAddOrder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageOrders;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TabPage tabPageEmployees;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnEmployeeID;
        private System.Windows.Forms.ColumnHeader columnLastName;
        private System.Windows.Forms.ColumnHeader columnFirstName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPageAutorizaion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox passwordtextbox;
        private System.Windows.Forms.TextBox logintextbox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ColumnHeader columnBirthDate;
        private System.Windows.Forms.ColumnHeader columnAdress;
        private System.Windows.Forms.ColumnHeader columnPhone;
        private System.Windows.Forms.ColumnHeader columnPost;
        private System.Windows.Forms.TabPage tabPageAddOrder;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}



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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Restaurant));
            this.tabPageOrders = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPageAutorizaion = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.passwordtextbox = new System.Windows.Forms.TextBox();
            this.logintextbox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAddOrder = new System.Windows.Forms.TabPage();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPageOrders.SuspendLayout();
            this.tabPageEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageAutorizaion.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageAddOrder.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageOrders
            // 
            this.tabPageOrders.Controls.Add(this.listView2);
            this.tabPageOrders.Location = new System.Drawing.Point(4, 22);
            this.tabPageOrders.Name = "tabPageOrders";
            this.tabPageOrders.Size = new System.Drawing.Size(792, 382);
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
            this.listView2.Size = new System.Drawing.Size(792, 382);
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
            this.tabPageEmployees.Controls.Add(this.comboBox1);
            this.tabPageEmployees.Controls.Add(this.textBox1);
            this.tabPageEmployees.Controls.Add(this.dataGridView1);
            this.tabPageEmployees.Location = new System.Drawing.Point(4, 22);
            this.tabPageEmployees.Name = "tabPageEmployees";
            this.tabPageEmployees.Size = new System.Drawing.Size(792, 382);
            this.tabPageEmployees.TabIndex = 2;
            this.tabPageEmployees.Text = "Сотрудники";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(136, 331);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(792, 315);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
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
            this.tabPageAutorizaion.Size = new System.Drawing.Size(792, 382);
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
            this.tabControl.Location = new System.Drawing.Point(0, 52);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 408);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageAddOrder
            // 
            this.tabPageAddOrder.Controls.Add(this.dataGridView2);
            this.tabPageAddOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddOrder.Name = "tabPageAddOrder";
            this.tabPageAddOrder.Size = new System.Drawing.Size(792, 382);
            this.tabPageAddOrder.TabIndex = 4;
            this.tabPageAddOrder.Text = "Добавить заказ";
            this.tabPageAddOrder.UseVisualStyleBackColor = true;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 22);
            this.toolStripButton1.Text = "Обновить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Id",
            "LastName",
            "FirstName",
            "BirthDate",
            "Address",
            "Phone",
            "Post"});
            this.comboBox1.Location = new System.Drawing.Point(9, 330);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(9, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(298, 370);
            this.dataGridView2.TabIndex = 0;
            // 
            // Restaurant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip2);
            this.Name = "Restaurant";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPageOrders.ResumeLayout(false);
            this.tabPageEmployees.ResumeLayout(false);
            this.tabPageEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageAutorizaion.ResumeLayout(false);
            this.tabPageAutorizaion.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageAddOrder.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageOrders;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TabPage tabPageEmployees;
        private System.Windows.Forms.TabPage tabPageAutorizaion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox passwordtextbox;
        private System.Windows.Forms.TextBox logintextbox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAddOrder;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}


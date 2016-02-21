namespace Xbox360WirelessChatpad
{
    partial class Window_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_Main));
            this.appLogTextbox = new System.Windows.Forms.TextBox();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatpadTextBox = new System.Windows.Forms.TextBox();
            this.ctrl1DeadzoneGroup = new System.Windows.Forms.GroupBox();
            this.ctrl1RightDeadzonePercentLabel = new System.Windows.Forms.Label();
            this.ctrl1LeftDeadzonePercentLabel = new System.Windows.Forms.Label();
            this.ctrl1RightDeadzone = new System.Windows.Forms.TrackBar();
            this.rightDeadzoneLabel = new System.Windows.Forms.Label();
            this.ctrl1LeftDeadzone = new System.Windows.Forms.TrackBar();
            this.leftDeadzoneLabel = new System.Windows.Forms.Label();
            this.ctrl1AzertyButton = new System.Windows.Forms.RadioButton();
            this.ctrl1QwertzButton = new System.Windows.Forms.RadioButton();
            this.ctrl1QwertyButton = new System.Windows.Forms.RadioButton();
            this.ctrl1Group = new System.Windows.Forms.GroupBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctrl1KeyboardGroup = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProfile = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.trayIconMenu.SuspendLayout();
            this.ctrl1DeadzoneGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl1RightDeadzone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl1LeftDeadzone)).BeginInit();
            this.ctrl1Group.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.ctrl1KeyboardGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // appLogTextbox
            // 
            this.appLogTextbox.Location = new System.Drawing.Point(12, 425);
            this.appLogTextbox.Multiline = true;
            this.appLogTextbox.Name = "appLogTextbox";
            this.appLogTextbox.ReadOnly = true;
            this.appLogTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.appLogTextbox.Size = new System.Drawing.Size(772, 135);
            this.appLogTextbox.TabIndex = 5;
            this.appLogTextbox.TextChanged += new System.EventHandler(this.appLogTextbox_TextChanged);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayIconMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Xbox 360 Wireless Chatpad";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(93, 26);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // chatpadTextBox
            // 
            this.chatpadTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatpadTextBox.Location = new System.Drawing.Point(11, 565);
            this.chatpadTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.chatpadTextBox.Name = "chatpadTextBox";
            this.chatpadTextBox.Size = new System.Drawing.Size(773, 20);
            this.chatpadTextBox.TabIndex = 3;
            this.chatpadTextBox.Text = "-Test Chatpad Here-";
            this.chatpadTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chatpadTextBox.Enter += new System.EventHandler(this.chatpadTextBox_Enter);
            // 
            // ctrl1DeadzoneGroup
            // 
            this.ctrl1DeadzoneGroup.Controls.Add(this.ctrl1RightDeadzonePercentLabel);
            this.ctrl1DeadzoneGroup.Controls.Add(this.ctrl1LeftDeadzonePercentLabel);
            this.ctrl1DeadzoneGroup.Controls.Add(this.ctrl1RightDeadzone);
            this.ctrl1DeadzoneGroup.Controls.Add(this.rightDeadzoneLabel);
            this.ctrl1DeadzoneGroup.Controls.Add(this.ctrl1LeftDeadzone);
            this.ctrl1DeadzoneGroup.Controls.Add(this.leftDeadzoneLabel);
            this.ctrl1DeadzoneGroup.Location = new System.Drawing.Point(13, 17);
            this.ctrl1DeadzoneGroup.Name = "ctrl1DeadzoneGroup";
            this.ctrl1DeadzoneGroup.Size = new System.Drawing.Size(147, 157);
            this.ctrl1DeadzoneGroup.TabIndex = 4;
            this.ctrl1DeadzoneGroup.TabStop = false;
            this.ctrl1DeadzoneGroup.Text = "Analog Deadzones";
            // 
            // ctrl1RightDeadzonePercentLabel
            // 
            this.ctrl1RightDeadzonePercentLabel.Location = new System.Drawing.Point(111, 90);
            this.ctrl1RightDeadzonePercentLabel.Name = "ctrl1RightDeadzonePercentLabel";
            this.ctrl1RightDeadzonePercentLabel.Size = new System.Drawing.Size(30, 13);
            this.ctrl1RightDeadzonePercentLabel.TabIndex = 5;
            this.ctrl1RightDeadzonePercentLabel.Text = "0%";
            this.ctrl1RightDeadzonePercentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl1LeftDeadzonePercentLabel
            // 
            this.ctrl1LeftDeadzonePercentLabel.Location = new System.Drawing.Point(111, 21);
            this.ctrl1LeftDeadzonePercentLabel.Name = "ctrl1LeftDeadzonePercentLabel";
            this.ctrl1LeftDeadzonePercentLabel.Size = new System.Drawing.Size(30, 13);
            this.ctrl1LeftDeadzonePercentLabel.TabIndex = 4;
            this.ctrl1LeftDeadzonePercentLabel.Text = "0%";
            this.ctrl1LeftDeadzonePercentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrl1RightDeadzone
            // 
            this.ctrl1RightDeadzone.Location = new System.Drawing.Point(7, 106);
            this.ctrl1RightDeadzone.Maximum = 30;
            this.ctrl1RightDeadzone.Name = "ctrl1RightDeadzone";
            this.ctrl1RightDeadzone.Size = new System.Drawing.Size(134, 42);
            this.ctrl1RightDeadzone.TabIndex = 3;
            this.ctrl1RightDeadzone.ValueChanged += new System.EventHandler(this.deadzoneR_ValueChanged);
            // 
            // rightDeadzoneLabel
            // 
            this.rightDeadzoneLabel.AutoSize = true;
            this.rightDeadzoneLabel.Location = new System.Drawing.Point(6, 90);
            this.rightDeadzoneLabel.Name = "rightDeadzoneLabel";
            this.rightDeadzoneLabel.Size = new System.Drawing.Size(95, 13);
            this.rightDeadzoneLabel.TabIndex = 2;
            this.rightDeadzoneLabel.Text = "Right Analog Stick";
            // 
            // ctrl1LeftDeadzone
            // 
            this.ctrl1LeftDeadzone.Location = new System.Drawing.Point(7, 37);
            this.ctrl1LeftDeadzone.Maximum = 30;
            this.ctrl1LeftDeadzone.Name = "ctrl1LeftDeadzone";
            this.ctrl1LeftDeadzone.Size = new System.Drawing.Size(134, 42);
            this.ctrl1LeftDeadzone.TabIndex = 1;
            this.ctrl1LeftDeadzone.ValueChanged += new System.EventHandler(this.deadzoneL_ValueChanged);
            // 
            // leftDeadzoneLabel
            // 
            this.leftDeadzoneLabel.AutoSize = true;
            this.leftDeadzoneLabel.Location = new System.Drawing.Point(6, 21);
            this.leftDeadzoneLabel.Name = "leftDeadzoneLabel";
            this.leftDeadzoneLabel.Size = new System.Drawing.Size(88, 13);
            this.leftDeadzoneLabel.TabIndex = 0;
            this.leftDeadzoneLabel.Text = "Left Analog Stick";
            // 
            // ctrl1AzertyButton
            // 
            this.ctrl1AzertyButton.AutoSize = true;
            this.ctrl1AzertyButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl1AzertyButton.Location = new System.Drawing.Point(5, 63);
            this.ctrl1AzertyButton.Name = "ctrl1AzertyButton";
            this.ctrl1AzertyButton.Size = new System.Drawing.Size(136, 22);
            this.ctrl1AzertyButton.TabIndex = 2;
            this.ctrl1AzertyButton.TabStop = true;
            this.ctrl1AzertyButton.Text = "A Z E R T Y";
            this.ctrl1AzertyButton.UseVisualStyleBackColor = true;
            this.ctrl1AzertyButton.CheckedChanged += new System.EventHandler(this.keyboardType_Selected);
            // 
            // ctrl1QwertzButton
            // 
            this.ctrl1QwertzButton.AutoSize = true;
            this.ctrl1QwertzButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl1QwertzButton.Location = new System.Drawing.Point(5, 41);
            this.ctrl1QwertzButton.Name = "ctrl1QwertzButton";
            this.ctrl1QwertzButton.Size = new System.Drawing.Size(136, 22);
            this.ctrl1QwertzButton.TabIndex = 1;
            this.ctrl1QwertzButton.TabStop = true;
            this.ctrl1QwertzButton.Text = "Q W E R T Z";
            this.ctrl1QwertzButton.UseVisualStyleBackColor = true;
            this.ctrl1QwertzButton.CheckedChanged += new System.EventHandler(this.keyboardType_Selected);
            // 
            // ctrl1QwertyButton
            // 
            this.ctrl1QwertyButton.AutoSize = true;
            this.ctrl1QwertyButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl1QwertyButton.Location = new System.Drawing.Point(5, 19);
            this.ctrl1QwertyButton.Name = "ctrl1QwertyButton";
            this.ctrl1QwertyButton.Size = new System.Drawing.Size(136, 22);
            this.ctrl1QwertyButton.TabIndex = 0;
            this.ctrl1QwertyButton.TabStop = true;
            this.ctrl1QwertyButton.Text = "Q W E R T Y";
            this.ctrl1QwertyButton.UseVisualStyleBackColor = true;
            this.ctrl1QwertyButton.CheckedChanged += new System.EventHandler(this.keyboardType_Selected);
            // 
            // ctrl1Group
            // 
            this.ctrl1Group.Controls.Add(this.cbDebug);
            this.ctrl1Group.Controls.Add(this.tabControl1);
            this.ctrl1Group.Controls.Add(this.button2);
            this.ctrl1Group.Controls.Add(this.button3);
            this.ctrl1Group.Controls.Add(this.button1);
            this.ctrl1Group.Controls.Add(this.label1);
            this.ctrl1Group.Controls.Add(this.cbProfile);
            this.ctrl1Group.Location = new System.Drawing.Point(11, 12);
            this.ctrl1Group.Name = "ctrl1Group";
            this.ctrl1Group.Size = new System.Drawing.Size(592, 407);
            this.ctrl1Group.TabIndex = 8;
            this.ctrl1Group.TabStop = false;
            this.ctrl1Group.Text = "Controller 1";
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.Location = new System.Drawing.Point(337, 45);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(58, 17);
            this.cbDebug.TabIndex = 16;
            this.cbDebug.Text = "Debug";
            this.cbDebug.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(580, 348);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Buttons";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.comboBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 319);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(53, 33);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 0;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(400, 33);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(123, 21);
            this.comboBox4.TabIndex = 0;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(226, 33);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(118, 21);
            this.comboBox3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "No modifier";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.numericUpDown4);
            this.tabPage3.Controls.Add(this.numericUpDown3);
            this.tabPage3.Controls.Add(this.numericUpDown2);
            this.tabPage3.Controls.Add(this.numericUpDown1);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.ctrl1DeadzoneGroup);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(572, 322);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sticks";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.DecimalPlaces = 3;
            this.numericUpDown4.Location = new System.Drawing.Point(88, 278);
            this.numericUpDown4.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.ReadOnly = true;
            this.numericUpDown4.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown4.TabIndex = 5;
            this.numericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 3;
            this.numericUpDown3.Location = new System.Drawing.Point(10, 278);
            this.numericUpDown3.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.ReadOnly = true;
            this.numericUpDown3.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown3.TabIndex = 5;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 3;
            this.numericUpDown2.Location = new System.Drawing.Point(88, 240);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.ReadOnly = true;
            this.numericUpDown2.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown2.TabIndex = 5;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Location = new System.Drawing.Point(10, 240);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(195, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 157);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Right stick - Pixel";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(111, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(111, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(7, 106);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(134, 42);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.deadzoneR_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Y";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(7, 37);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(134, 42);
            this.trackBar2.TabIndex = 1;
            this.trackBar2.Value = 1;
            this.trackBar2.ValueChanged += new System.EventHandler(this.deadzoneL_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "X";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ctrl1KeyboardGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chatpad";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrl1KeyboardGroup
            // 
            this.ctrl1KeyboardGroup.Controls.Add(this.ctrl1AzertyButton);
            this.ctrl1KeyboardGroup.Controls.Add(this.ctrl1QwertyButton);
            this.ctrl1KeyboardGroup.Controls.Add(this.ctrl1QwertzButton);
            this.ctrl1KeyboardGroup.Location = new System.Drawing.Point(6, 6);
            this.ctrl1KeyboardGroup.Name = "ctrl1KeyboardGroup";
            this.ctrl1KeyboardGroup.Size = new System.Drawing.Size(147, 91);
            this.ctrl1KeyboardGroup.TabIndex = 9;
            this.ctrl1KeyboardGroup.TabStop = false;
            this.ctrl1KeyboardGroup.Text = "Keyboard Type";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(337, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Profile";
            // 
            // cbProfile
            // 
            this.cbProfile.FormattingEnabled = true;
            this.cbProfile.Location = new System.Drawing.Point(75, 19);
            this.cbProfile.Name = "cbProfile";
            this.cbProfile.Size = new System.Drawing.Size(256, 21);
            this.cbProfile.TabIndex = 12;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(609, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(49, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Run";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(609, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(175, 362);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "debug - values";
            // 
            // Window_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 596);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ctrl1Group);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.appLogTextbox);
            this.Controls.Add(this.chatpadTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Window_Main";
            this.Text = "Xbox 360 Controller and Chatpad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Main_FormClosing);
            this.Load += new System.EventHandler(this.Window_Main_Load);
            this.Resize += new System.EventHandler(this.Window_Main_Resize);
            this.trayIconMenu.ResumeLayout(false);
            this.ctrl1DeadzoneGroup.ResumeLayout(false);
            this.ctrl1DeadzoneGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl1RightDeadzone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl1LeftDeadzone)).EndInit();
            this.ctrl1Group.ResumeLayout(false);
            this.ctrl1Group.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ctrl1KeyboardGroup.ResumeLayout(false);
            this.ctrl1KeyboardGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox appLogTextbox;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.TextBox chatpadTextBox;
        private System.Windows.Forms.GroupBox ctrl1DeadzoneGroup;
        private System.Windows.Forms.TrackBar ctrl1RightDeadzone;
        private System.Windows.Forms.Label rightDeadzoneLabel;
        private System.Windows.Forms.TrackBar ctrl1LeftDeadzone;
        private System.Windows.Forms.Label leftDeadzoneLabel;
        private System.Windows.Forms.Label ctrl1RightDeadzonePercentLabel;
        private System.Windows.Forms.Label ctrl1LeftDeadzonePercentLabel;
        private System.Windows.Forms.RadioButton ctrl1AzertyButton;
        private System.Windows.Forms.RadioButton ctrl1QwertzButton;
        private System.Windows.Forms.RadioButton ctrl1QwertyButton;
        private System.Windows.Forms.GroupBox ctrl1Group;
        private System.Windows.Forms.GroupBox ctrl1KeyboardGroup;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
    }
}


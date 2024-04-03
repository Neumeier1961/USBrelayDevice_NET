namespace HIDusbRelay_Test
{
    partial class UI_Main
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
                usbRelay.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Main));
            this.btn_DeviceList = new System.Windows.Forms.Button();
            this.cb_Devices = new System.Windows.Forms.ComboBox();
            this.btn_OpenDevice = new System.Windows.Forms.Button();
            this.btn_CloseDevice = new System.Windows.Forms.Button();
            this.btn_AllON = new System.Windows.Forms.Button();
            this.btn_AllOFF = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RLY_1 = new System.Windows.Forms.Label();
            this.RLY_2 = new System.Windows.Forms.Label();
            this.RLY_3 = new System.Windows.Forms.Label();
            this.RLY_4 = new System.Windows.Forms.Label();
            this.RLY_8 = new System.Windows.Forms.Label();
            this.RLY_7 = new System.Windows.Forms.Label();
            this.RLY_6 = new System.Windows.Forms.Label();
            this.RLY_5 = new System.Windows.Forms.Label();
            this.lb_ConnectedDevice = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.nud_relay = new System.Windows.Forms.NumericUpDown();
            this.btn_RlyOn = new System.Windows.Forms.Button();
            this.btn_RlyOff = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_UseDeviceID = new System.Windows.Forms.CheckBox();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.lb_DeviceInfo = new System.Windows.Forms.Label();
            this.btn_SetID = new System.Windows.Forms.Button();
            this.tb_DeviceID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_Default = new System.Windows.Forms.RadioButton();
            this.rb_DeviceID = new System.Windows.Forms.RadioButton();
            this.rb_FirstDevice = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rb_DoNotOpen = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_relay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_DeviceList
            // 
            this.btn_DeviceList.Location = new System.Drawing.Point(264, 26);
            this.btn_DeviceList.Name = "btn_DeviceList";
            this.btn_DeviceList.Size = new System.Drawing.Size(57, 23);
            this.btn_DeviceList.TabIndex = 1;
            this.btn_DeviceList.Text = "Refresh";
            this.btn_DeviceList.UseVisualStyleBackColor = true;
            this.btn_DeviceList.Click += new System.EventHandler(this.btn_DeviceList_Click);
            // 
            // cb_Devices
            // 
            this.cb_Devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Devices.FormattingEnabled = true;
            this.cb_Devices.Location = new System.Drawing.Point(71, 27);
            this.cb_Devices.Name = "cb_Devices";
            this.cb_Devices.Size = new System.Drawing.Size(176, 21);
            this.cb_Devices.TabIndex = 2;
            this.cb_Devices.SelectedIndexChanged += new System.EventHandler(this.cb_Devices_SelectedIndexChanged);
            // 
            // btn_OpenDevice
            // 
            this.btn_OpenDevice.Location = new System.Drawing.Point(20, 63);
            this.btn_OpenDevice.Name = "btn_OpenDevice";
            this.btn_OpenDevice.Size = new System.Drawing.Size(110, 23);
            this.btn_OpenDevice.TabIndex = 4;
            this.btn_OpenDevice.Text = "Open Device";
            this.btn_OpenDevice.UseVisualStyleBackColor = true;
            this.btn_OpenDevice.Click += new System.EventHandler(this.btn_OpenDevice_Click);
            // 
            // btn_CloseDevice
            // 
            this.btn_CloseDevice.Location = new System.Drawing.Point(137, 63);
            this.btn_CloseDevice.Name = "btn_CloseDevice";
            this.btn_CloseDevice.Size = new System.Drawing.Size(110, 23);
            this.btn_CloseDevice.TabIndex = 5;
            this.btn_CloseDevice.Text = "Close Device";
            this.btn_CloseDevice.UseVisualStyleBackColor = true;
            this.btn_CloseDevice.Click += new System.EventHandler(this.btn_CloseDevice_Click);
            // 
            // btn_AllON
            // 
            this.btn_AllON.Location = new System.Drawing.Point(290, 151);
            this.btn_AllON.Name = "btn_AllON";
            this.btn_AllON.Size = new System.Drawing.Size(75, 23);
            this.btn_AllON.TabIndex = 6;
            this.btn_AllON.Text = "All ON";
            this.btn_AllON.UseVisualStyleBackColor = true;
            this.btn_AllON.Click += new System.EventHandler(this.btn_AllON_Click);
            // 
            // btn_AllOFF
            // 
            this.btn_AllOFF.Location = new System.Drawing.Point(290, 184);
            this.btn_AllOFF.Name = "btn_AllOFF";
            this.btn_AllOFF.Size = new System.Drawing.Size(75, 23);
            this.btn_AllOFF.TabIndex = 7;
            this.btn_AllOFF.Text = "All OFF";
            this.btn_AllOFF.UseVisualStyleBackColor = true;
            this.btn_AllOFF.Click += new System.EventHandler(this.btn_AllOFF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Devices:";
            // 
            // RLY_1
            // 
            this.RLY_1.AutoSize = true;
            this.RLY_1.BackColor = System.Drawing.Color.Silver;
            this.RLY_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_1.Enabled = false;
            this.RLY_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_1.Location = new System.Drawing.Point(94, 34);
            this.RLY_1.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_1.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_1.Name = "RLY_1";
            this.RLY_1.Size = new System.Drawing.Size(32, 32);
            this.RLY_1.TabIndex = 11;
            this.RLY_1.Text = "1";
            this.RLY_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_1.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_2
            // 
            this.RLY_2.AutoSize = true;
            this.RLY_2.BackColor = System.Drawing.Color.Silver;
            this.RLY_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_2.Enabled = false;
            this.RLY_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_2.Location = new System.Drawing.Point(94, 80);
            this.RLY_2.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_2.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_2.Name = "RLY_2";
            this.RLY_2.Size = new System.Drawing.Size(32, 32);
            this.RLY_2.TabIndex = 12;
            this.RLY_2.Text = "2";
            this.RLY_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_2.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_3
            // 
            this.RLY_3.AutoSize = true;
            this.RLY_3.BackColor = System.Drawing.Color.Silver;
            this.RLY_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_3.Enabled = false;
            this.RLY_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_3.Location = new System.Drawing.Point(94, 126);
            this.RLY_3.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_3.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_3.Name = "RLY_3";
            this.RLY_3.Size = new System.Drawing.Size(32, 32);
            this.RLY_3.TabIndex = 13;
            this.RLY_3.Text = "3";
            this.RLY_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_3.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_4
            // 
            this.RLY_4.AutoSize = true;
            this.RLY_4.BackColor = System.Drawing.Color.Silver;
            this.RLY_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_4.Enabled = false;
            this.RLY_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_4.Location = new System.Drawing.Point(94, 172);
            this.RLY_4.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_4.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_4.Name = "RLY_4";
            this.RLY_4.Size = new System.Drawing.Size(32, 32);
            this.RLY_4.TabIndex = 14;
            this.RLY_4.Text = "4";
            this.RLY_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_4.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_8
            // 
            this.RLY_8.AutoSize = true;
            this.RLY_8.BackColor = System.Drawing.Color.Silver;
            this.RLY_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_8.Enabled = false;
            this.RLY_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_8.Location = new System.Drawing.Point(170, 34);
            this.RLY_8.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_8.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_8.Name = "RLY_8";
            this.RLY_8.Size = new System.Drawing.Size(32, 32);
            this.RLY_8.TabIndex = 18;
            this.RLY_8.Text = "8";
            this.RLY_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_8.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_7
            // 
            this.RLY_7.AutoSize = true;
            this.RLY_7.BackColor = System.Drawing.Color.Silver;
            this.RLY_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_7.Enabled = false;
            this.RLY_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_7.Location = new System.Drawing.Point(170, 80);
            this.RLY_7.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_7.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_7.Name = "RLY_7";
            this.RLY_7.Size = new System.Drawing.Size(32, 32);
            this.RLY_7.TabIndex = 17;
            this.RLY_7.Text = "7";
            this.RLY_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_7.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_6
            // 
            this.RLY_6.AutoSize = true;
            this.RLY_6.BackColor = System.Drawing.Color.Silver;
            this.RLY_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_6.Enabled = false;
            this.RLY_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_6.Location = new System.Drawing.Point(170, 126);
            this.RLY_6.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_6.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_6.Name = "RLY_6";
            this.RLY_6.Size = new System.Drawing.Size(32, 32);
            this.RLY_6.TabIndex = 16;
            this.RLY_6.Text = "6";
            this.RLY_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_6.Click += new System.EventHandler(this.RLY_Click);
            // 
            // RLY_5
            // 
            this.RLY_5.AutoSize = true;
            this.RLY_5.BackColor = System.Drawing.Color.Silver;
            this.RLY_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RLY_5.Enabled = false;
            this.RLY_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLY_5.Location = new System.Drawing.Point(170, 172);
            this.RLY_5.MaximumSize = new System.Drawing.Size(32, 32);
            this.RLY_5.MinimumSize = new System.Drawing.Size(32, 32);
            this.RLY_5.Name = "RLY_5";
            this.RLY_5.Size = new System.Drawing.Size(32, 32);
            this.RLY_5.TabIndex = 15;
            this.RLY_5.Text = "5";
            this.RLY_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RLY_5.Click += new System.EventHandler(this.RLY_Click);
            // 
            // lb_ConnectedDevice
            // 
            this.lb_ConnectedDevice.AutoSize = true;
            this.lb_ConnectedDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ConnectedDevice.Location = new System.Drawing.Point(256, 70);
            this.lb_ConnectedDevice.Name = "lb_ConnectedDevice";
            this.lb_ConnectedDevice.Size = new System.Drawing.Size(185, 16);
            this.lb_ConnectedDevice.TabIndex = 22;
            this.lb_ConnectedDevice.Text = "Connected Device: NONE";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(591, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorLogToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // errorLogToolStripMenuItem
            // 
            this.errorLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            this.errorLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.errorLogToolStripMenuItem.Text = "Error Log";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Relay Number:";
            // 
            // nud_relay
            // 
            this.nud_relay.Location = new System.Drawing.Point(343, 39);
            this.nud_relay.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nud_relay.Name = "nud_relay";
            this.nud_relay.Size = new System.Drawing.Size(56, 20);
            this.nud_relay.TabIndex = 25;
            this.nud_relay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_relay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_RlyOn
            // 
            this.btn_RlyOn.Location = new System.Drawing.Point(290, 65);
            this.btn_RlyOn.Name = "btn_RlyOn";
            this.btn_RlyOn.Size = new System.Drawing.Size(75, 23);
            this.btn_RlyOn.TabIndex = 26;
            this.btn_RlyOn.Text = "ON";
            this.btn_RlyOn.UseVisualStyleBackColor = true;
            this.btn_RlyOn.Click += new System.EventHandler(this.btn_RlyOn_Click);
            // 
            // btn_RlyOff
            // 
            this.btn_RlyOff.Location = new System.Drawing.Point(290, 94);
            this.btn_RlyOff.Name = "btn_RlyOff";
            this.btn_RlyOff.Size = new System.Drawing.Size(75, 23);
            this.btn_RlyOff.TabIndex = 27;
            this.btn_RlyOff.Text = "OFF";
            this.btn_RlyOff.UseVisualStyleBackColor = true;
            this.btn_RlyOff.Click += new System.EventHandler(this.btn_RlyOff_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.RLY_1);
            this.groupBox1.Controls.Add(this.btn_RlyOff);
            this.groupBox1.Controls.Add(this.RLY_2);
            this.groupBox1.Controls.Add(this.btn_RlyOn);
            this.groupBox1.Controls.Add(this.RLY_3);
            this.groupBox1.Controls.Add(this.nud_relay);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_AllOFF);
            this.groupBox1.Controls.Add(this.RLY_4);
            this.groupBox1.Controls.Add(this.btn_AllON);
            this.groupBox1.Controls.Add(this.RLY_5);
            this.groupBox1.Controls.Add(this.RLY_6);
            this.groupBox1.Controls.Add(this.RLY_7);
            this.groupBox1.Controls.Add(this.RLY_8);
            this.groupBox1.Location = new System.Drawing.Point(122, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 246);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relay Control";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(72, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "Click to change State";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Silver;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 194);
            this.label7.MaximumSize = new System.Drawing.Size(30, 22);
            this.label7.MinimumSize = new System.Drawing.Size(30, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 22);
            this.label7.TabIndex = 30;
            this.label7.Text = "NA";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Lime;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 166);
            this.label6.MaximumSize = new System.Drawing.Size(30, 22);
            this.label6.MinimumSize = new System.Drawing.Size(30, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 22);
            this.label6.TabIndex = 29;
            this.label6.Text = "On";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightYellow;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 138);
            this.label5.MaximumSize = new System.Drawing.Size(30, 22);
            this.label5.MinimumSize = new System.Drawing.Size(30, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 22);
            this.label5.TabIndex = 28;
            this.label5.Text = "Off";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.chk_UseDeviceID);
            this.groupBox2.Controls.Add(this.btn_Copy);
            this.groupBox2.Controls.Add(this.lb_DeviceInfo);
            this.groupBox2.Controls.Add(this.btn_DeviceList);
            this.groupBox2.Controls.Add(this.cb_Devices);
            this.groupBox2.Controls.Add(this.lb_ConnectedDevice);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_OpenDevice);
            this.groupBox2.Controls.Add(this.btn_CloseDevice);
            this.groupBox2.Location = new System.Drawing.Point(27, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 213);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "USB Relay Device";
            // 
            // chk_UseDeviceID
            // 
            this.chk_UseDeviceID.AutoSize = true;
            this.chk_UseDeviceID.Location = new System.Drawing.Point(347, 31);
            this.chk_UseDeviceID.Name = "chk_UseDeviceID";
            this.chk_UseDeviceID.Size = new System.Drawing.Size(131, 17);
            this.chk_UseDeviceID.TabIndex = 25;
            this.chk_UseDeviceID.Text = "Open using Device ID";
            this.chk_UseDeviceID.UseVisualStyleBackColor = true;
            // 
            // btn_Copy
            // 
            this.btn_Copy.Location = new System.Drawing.Point(187, 180);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(161, 23);
            this.btn_Copy.TabIndex = 24;
            this.btn_Copy.Text = "Copy Device Info to Clipboard";
            this.btn_Copy.UseVisualStyleBackColor = true;
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // lb_DeviceInfo
            // 
            this.lb_DeviceInfo.AutoSize = true;
            this.lb_DeviceInfo.BackColor = System.Drawing.Color.LightYellow;
            this.lb_DeviceInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_DeviceInfo.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DeviceInfo.Location = new System.Drawing.Point(7, 99);
            this.lb_DeviceInfo.MaximumSize = new System.Drawing.Size(520, 75);
            this.lb_DeviceInfo.MinimumSize = new System.Drawing.Size(520, 75);
            this.lb_DeviceInfo.Name = "lb_DeviceInfo";
            this.lb_DeviceInfo.Size = new System.Drawing.Size(520, 75);
            this.lb_DeviceInfo.TabIndex = 23;
            this.lb_DeviceInfo.Text = "Device Info:";
            // 
            // btn_SetID
            // 
            this.btn_SetID.Location = new System.Drawing.Point(286, 521);
            this.btn_SetID.Name = "btn_SetID";
            this.btn_SetID.Size = new System.Drawing.Size(75, 23);
            this.btn_SetID.TabIndex = 30;
            this.btn_SetID.Text = "Set ID";
            this.btn_SetID.UseVisualStyleBackColor = true;
            this.btn_SetID.Click += new System.EventHandler(this.btn_SetID_Click);
            // 
            // tb_DeviceID
            // 
            this.tb_DeviceID.Location = new System.Drawing.Point(191, 522);
            this.tb_DeviceID.MaxLength = 5;
            this.tb_DeviceID.Name = "tb_DeviceID";
            this.tb_DeviceID.Size = new System.Drawing.Size(81, 20);
            this.tb_DeviceID.TabIndex = 31;
            this.tb_DeviceID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 526);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Device ID:";
            // 
            // rb_Default
            // 
            this.rb_Default.AutoSize = true;
            this.rb_Default.Checked = true;
            this.rb_Default.Location = new System.Drawing.Point(13, 25);
            this.rb_Default.Name = "rb_Default";
            this.rb_Default.Size = new System.Drawing.Size(59, 17);
            this.rb_Default.TabIndex = 33;
            this.rb_Default.TabStop = true;
            this.rb_Default.Text = "Default";
            this.rb_Default.UseVisualStyleBackColor = true;
            // 
            // rb_DeviceID
            // 
            this.rb_DeviceID.AutoSize = true;
            this.rb_DeviceID.Location = new System.Drawing.Point(13, 49);
            this.rb_DeviceID.Name = "rb_DeviceID";
            this.rb_DeviceID.Size = new System.Drawing.Size(73, 17);
            this.rb_DeviceID.TabIndex = 34;
            this.rb_DeviceID.Text = "Device ID";
            this.rb_DeviceID.UseVisualStyleBackColor = true;
            // 
            // rb_FirstDevice
            // 
            this.rb_FirstDevice.AutoSize = true;
            this.rb_FirstDevice.Location = new System.Drawing.Point(13, 73);
            this.rb_FirstDevice.Name = "rb_FirstDevice";
            this.rb_FirstDevice.Size = new System.Drawing.Size(81, 17);
            this.rb_FirstDevice.TabIndex = 35;
            this.rb_FirstDevice.Text = "First Device";
            this.rb_FirstDevice.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Azure;
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.rb_DoNotOpen);
            this.groupBox3.Controls.Add(this.rb_Default);
            this.groupBox3.Controls.Add(this.rb_FirstDevice);
            this.groupBox3.Controls.Add(this.rb_DeviceID);
            this.groupBox3.Location = new System.Drawing.Point(9, 257);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(104, 186);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Start Options";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 39);
            this.label8.TabIndex = 37;
            this.label8.Text = "options for \r\nclass instance\r\noverloads";
            // 
            // rb_DoNotOpen
            // 
            this.rb_DoNotOpen.AutoSize = true;
            this.rb_DoNotOpen.Location = new System.Drawing.Point(13, 97);
            this.rb_DoNotOpen.Name = "rb_DoNotOpen";
            this.rb_DoNotOpen.Size = new System.Drawing.Size(88, 17);
            this.rb_DoNotOpen.TabIndex = 36;
            this.rb_DoNotOpen.Text = "Do Not Open";
            this.rb_DoNotOpen.UseVisualStyleBackColor = true;
            // 
            // UI_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(591, 556);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_DeviceID);
            this.Controls.Add(this.btn_SetID);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "UI_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NOYITO USB Relay Test Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_relay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_DeviceList;
        private System.Windows.Forms.ComboBox cb_Devices;
        private System.Windows.Forms.Button btn_OpenDevice;
        private System.Windows.Forms.Button btn_CloseDevice;
        private System.Windows.Forms.Button btn_AllON;
        private System.Windows.Forms.Button btn_AllOFF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RLY_1;
        private System.Windows.Forms.Label RLY_2;
        private System.Windows.Forms.Label RLY_3;
        private System.Windows.Forms.Label RLY_4;
        private System.Windows.Forms.Label RLY_8;
        private System.Windows.Forms.Label RLY_7;
        private System.Windows.Forms.Label RLY_6;
        private System.Windows.Forms.Label RLY_5;
        private System.Windows.Forms.Label lb_ConnectedDevice;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nud_relay;
        private System.Windows.Forms.Button btn_RlyOn;
        private System.Windows.Forms.Button btn_RlyOff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_DeviceInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btn_Copy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_SetID;
        private System.Windows.Forms.TextBox tb_DeviceID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_Default;
        private System.Windows.Forms.RadioButton rb_DeviceID;
        private System.Windows.Forms.RadioButton rb_FirstDevice;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rb_DoNotOpen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chk_UseDeviceID;
    }
}


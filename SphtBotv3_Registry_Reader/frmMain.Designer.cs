namespace SphtBotv3_Registry_Manager
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuStrip = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUDPPort = new System.Windows.Forms.TextBox();
            this.cboAwayIdle = new System.Windows.Forms.ComboBox();
            this.cboExtendedWhois = new System.Windows.Forms.ComboBox();
            this.cboShowUndecoded = new System.Windows.Forms.ComboBox();
            this.cboADBanner = new System.Windows.Forms.ComboBox();
            this.cboDescribeUserFlags = new System.Windows.Forms.ComboBox();
            this.cboChannelOrder = new System.Windows.Forms.ComboBox();
            this.cboBleedTimestamps = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBindIP = new System.Windows.Forms.TextBox();
            this.txtIgnorePluginMask = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRealmCharacter = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboRealmName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ttIPM = new System.Windows.Forms.ToolTip(this.components);
            this.ttRC = new System.Windows.Forms.ToolTip(this.components);
            this.ttRN = new System.Windows.Forms.ToolTip(this.components);
            this.ttAI = new System.Windows.Forms.ToolTip(this.components);
            this.ttSU = new System.Windows.Forms.ToolTip(this.components);
            this.ttEW = new System.Windows.Forms.ToolTip(this.components);
            this.ttDAB = new System.Windows.Forms.ToolTip(this.components);
            this.ttDUF = new System.Windows.Forms.ToolTip(this.components);
            this.ttCO = new System.Windows.Forms.ToolTip(this.components);
            this.ttBT = new System.Windows.Forms.ToolTip(this.components);
            this.ttUDP = new System.Windows.Forms.ToolTip(this.components);
            this.ttBindIP = new System.Windows.Forms.ToolTip(this.components);
            this.cboProfiles = new System.Windows.Forms.ComboBox();
            this.lblProfiles = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tmrEditButton = new System.Windows.Forms.Timer(this.components);
            this.mnuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuStrip
            // 
            this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.changelogToolStripMenuItem});
            this.mnuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(499, 24);
            this.mnuStrip.TabIndex = 0;
            this.mnuStrip.Text = "mnuStrip";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.changelogToolStripMenuItem.Text = "&ChangeLog";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(267, 206);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(97, 35);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read Registry";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(370, 206);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(97, 35);
            this.btnWrite.TabIndex = 4;
            this.btnWrite.Text = "Write to Registry";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtUDPPort);
            this.groupBox1.Controls.Add(this.cboAwayIdle);
            this.groupBox1.Controls.Add(this.cboExtendedWhois);
            this.groupBox1.Controls.Add(this.cboShowUndecoded);
            this.groupBox1.Controls.Add(this.cboADBanner);
            this.groupBox1.Controls.Add(this.cboDescribeUserFlags);
            this.groupBox1.Controls.Add(this.cboChannelOrder);
            this.groupBox1.Controls.Add(this.cboBleedTimestamps);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 237);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DWORD Values";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 209);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "UDP Port";
            this.ttUDP.SetToolTip(this.label11, "Sets the UDP port you want to use for the game you\'re connecting to?");
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.Location = new System.Drawing.Point(114, 206);
            this.txtUDPPort.MaxLength = 5;
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.Size = new System.Drawing.Size(102, 20);
            this.txtUDPPort.TabIndex = 71;
            // 
            // cboAwayIdle
            // 
            this.cboAwayIdle.BackColor = System.Drawing.Color.White;
            this.cboAwayIdle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAwayIdle.FormattingEnabled = true;
            this.cboAwayIdle.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboAwayIdle.Location = new System.Drawing.Point(114, 179);
            this.cboAwayIdle.Name = "cboAwayIdle";
            this.cboAwayIdle.Size = new System.Drawing.Size(102, 21);
            this.cboAwayIdle.TabIndex = 65;
            // 
            // cboExtendedWhois
            // 
            this.cboExtendedWhois.BackColor = System.Drawing.Color.White;
            this.cboExtendedWhois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExtendedWhois.FormattingEnabled = true;
            this.cboExtendedWhois.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboExtendedWhois.Location = new System.Drawing.Point(114, 127);
            this.cboExtendedWhois.Name = "cboExtendedWhois";
            this.cboExtendedWhois.Size = new System.Drawing.Size(102, 21);
            this.cboExtendedWhois.TabIndex = 63;
            // 
            // cboShowUndecoded
            // 
            this.cboShowUndecoded.BackColor = System.Drawing.Color.White;
            this.cboShowUndecoded.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShowUndecoded.FormattingEnabled = true;
            this.cboShowUndecoded.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboShowUndecoded.Location = new System.Drawing.Point(114, 153);
            this.cboShowUndecoded.Name = "cboShowUndecoded";
            this.cboShowUndecoded.Size = new System.Drawing.Size(102, 21);
            this.cboShowUndecoded.TabIndex = 64;
            // 
            // cboADBanner
            // 
            this.cboADBanner.BackColor = System.Drawing.Color.White;
            this.cboADBanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboADBanner.FormattingEnabled = true;
            this.cboADBanner.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboADBanner.Location = new System.Drawing.Point(114, 101);
            this.cboADBanner.Name = "cboADBanner";
            this.cboADBanner.Size = new System.Drawing.Size(102, 21);
            this.cboADBanner.TabIndex = 62;
            // 
            // cboDescribeUserFlags
            // 
            this.cboDescribeUserFlags.BackColor = System.Drawing.Color.White;
            this.cboDescribeUserFlags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDescribeUserFlags.FormattingEnabled = true;
            this.cboDescribeUserFlags.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboDescribeUserFlags.Location = new System.Drawing.Point(114, 75);
            this.cboDescribeUserFlags.Name = "cboDescribeUserFlags";
            this.cboDescribeUserFlags.Size = new System.Drawing.Size(102, 21);
            this.cboDescribeUserFlags.TabIndex = 61;
            // 
            // cboChannelOrder
            // 
            this.cboChannelOrder.BackColor = System.Drawing.Color.White;
            this.cboChannelOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChannelOrder.FormattingEnabled = true;
            this.cboChannelOrder.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboChannelOrder.Location = new System.Drawing.Point(114, 49);
            this.cboChannelOrder.Name = "cboChannelOrder";
            this.cboChannelOrder.Size = new System.Drawing.Size(102, 21);
            this.cboChannelOrder.TabIndex = 60;
            // 
            // cboBleedTimestamps
            // 
            this.cboBleedTimestamps.BackColor = System.Drawing.Color.White;
            this.cboBleedTimestamps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBleedTimestamps.FormattingEnabled = true;
            this.cboBleedTimestamps.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.cboBleedTimestamps.Location = new System.Drawing.Point(114, 23);
            this.cboBleedTimestamps.Name = "cboBleedTimestamps";
            this.cboBleedTimestamps.Size = new System.Drawing.Size(102, 21);
            this.cboBleedTimestamps.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Away Idle";
            this.ttAI.SetToolTip(this.label7, "Enable or Disable the infamous \"Away Idle\"\r\nExample:\r\nx0.Baroness is away (Bot ha" +
                    "s been idle for 55 minutes.)");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Show Undecoded";
            this.ttSU.SetToolTip(this.label6, "Enable or Disable the showing of non-encrypted messages in addition to the encryp" +
                    "ted messages from other users");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Extended Whois";
            this.ttEW.SetToolTip(this.label5, "Enable or Disable showing of the IP Address that you\'re using and the IP Address " +
                    "of the server you\'re connected to when you /whois yourself");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "Display AD-Banner";
            this.ttDAB.SetToolTip(this.label4, "Enable or Disable showing of Battle.net advertisement number sequence");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Describe User Flags";
            this.ttDUF.SetToolTip(this.label3, "Enable or Disable the explanation of exactly what type of flags a user has in the" +
                    " channel\r\nExample: (When you hover over the user)\r\nFlags: 10 (no udp) or Flags: " +
                    "2 (operator)");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Channel Order";
            this.ttCO.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Bleed Timestamps";
            this.ttBT.SetToolTip(this.label1, "Enable or Disable of whatever the fuck it is this does");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtBindIP);
            this.groupBox2.Controls.Add(this.txtIgnorePluginMask);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtRealmCharacter);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cboRealmName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(242, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 133);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "String Values";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 72;
            this.label12.Text = "Bind IP";
            this.ttBindIP.SetToolTip(this.label12, "Can be used to override the IP address with which the program uses to connect to " +
                    "various networks\r\nWARNING: If you don\'t know what you\'re doing, don\'t mess with " +
                    "this.");
            // 
            // txtBindIP
            // 
            this.txtBindIP.Location = new System.Drawing.Point(110, 101);
            this.txtBindIP.MaxLength = 15;
            this.txtBindIP.Name = "txtBindIP";
            this.txtBindIP.Size = new System.Drawing.Size(130, 20);
            this.txtBindIP.TabIndex = 71;
            // 
            // txtIgnorePluginMask
            // 
            this.txtIgnorePluginMask.Location = new System.Drawing.Point(110, 75);
            this.txtIgnorePluginMask.Name = "txtIgnorePluginMask";
            this.txtIgnorePluginMask.Size = new System.Drawing.Size(130, 20);
            this.txtIgnorePluginMask.TabIndex = 70;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 69;
            this.label10.Text = "Ignore Plugin Mask";
            this.ttIPM.SetToolTip(this.label10, "Adds the ability to ignore plugins that are automatically loaded (BCP file extens" +
                    "ion)\r\nEach plugin is separated by a comma\r\nExample:\r\noper.bcp, aliases.bcp, chat" +
                    "focus.bcp");
            // 
            // txtRealmCharacter
            // 
            this.txtRealmCharacter.Location = new System.Drawing.Point(110, 49);
            this.txtRealmCharacter.MaxLength = 15;
            this.txtRealmCharacter.Name = "txtRealmCharacter";
            this.txtRealmCharacter.Size = new System.Drawing.Size(130, 20);
            this.txtRealmCharacter.TabIndex = 68;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Realm character";
            this.ttRC.SetToolTip(this.label9, "The Diablo II MCP character name on the Diablo II account you\'re using");
            // 
            // cboRealmName
            // 
            this.cboRealmName.BackColor = System.Drawing.Color.White;
            this.cboRealmName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRealmName.FormattingEnabled = true;
            this.cboRealmName.Items.AddRange(new object[] {
            "USEast",
            "USWest",
            "Europe",
            "Asia"});
            this.cboRealmName.Location = new System.Drawing.Point(110, 23);
            this.cboRealmName.Name = "cboRealmName";
            this.cboRealmName.Size = new System.Drawing.Size(130, 21);
            this.cboRealmName.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Realm name";
            this.ttRN.SetToolTip(this.label8, "The name of the Diablo II MCP realm that you want to connect to");
            // 
            // cboProfiles
            // 
            this.cboProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProfiles.FormattingEnabled = true;
            this.cboProfiles.Location = new System.Drawing.Point(15, 40);
            this.cboProfiles.MaxDropDownItems = 16;
            this.cboProfiles.Name = "cboProfiles";
            this.cboProfiles.Size = new System.Drawing.Size(161, 21);
            this.cboProfiles.TabIndex = 20;
            // 
            // lblProfiles
            // 
            this.lblProfiles.AutoSize = true;
            this.lblProfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfiles.Location = new System.Drawing.Point(12, 24);
            this.lblProfiles.Name = "lblProfiles";
            this.lblProfiles.Size = new System.Drawing.Size(97, 13);
            this.lblProfiles.TabIndex = 73;
            this.lblProfiles.Text = "{0} Profile(s) Found";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(182, 38);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 74;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tmrEditButton
            // 
            this.tmrEditButton.Enabled = true;
            this.tmrEditButton.Tick += new System.EventHandler(this.tmrEditButton_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 310);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblProfiles);
            this.Controls.Add(this.cboProfiles);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.mnuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuStrip;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SphtBotv3 Registry Manager";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBleedTimestamps;
        private System.Windows.Forms.ComboBox cboAwayIdle;
        private System.Windows.Forms.ComboBox cboExtendedWhois;
        private System.Windows.Forms.ComboBox cboShowUndecoded;
        private System.Windows.Forms.ComboBox cboADBanner;
        private System.Windows.Forms.ComboBox cboDescribeUserFlags;
        private System.Windows.Forms.ComboBox cboChannelOrder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIgnorePluginMask;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRealmCharacter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboRealmName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip ttIPM;
        private System.Windows.Forms.ToolTip ttRC;
        private System.Windows.Forms.ToolTip ttRN;
        private System.Windows.Forms.ToolTip ttAI;
        private System.Windows.Forms.ToolTip ttSU;
        private System.Windows.Forms.ToolTip ttEW;
        private System.Windows.Forms.ToolTip ttDAB;
        private System.Windows.Forms.ToolTip ttDUF;
        private System.Windows.Forms.ToolTip ttCO;
        private System.Windows.Forms.ToolTip ttBT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip ttUDP;
        private System.Windows.Forms.TextBox txtUDPPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolTip ttBindIP;
        private System.Windows.Forms.TextBox txtBindIP;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.Label lblProfiles;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Timer tmrEditButton;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cboProfiles;
    }
}


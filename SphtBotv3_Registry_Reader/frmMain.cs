﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Security.Principal;
using System.Reflection;
using Microsoft.Win32;


namespace SphtBotv3_Registry_Manager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private bool IsRunAsAdmin()
        {
            var WI = WindowsIdentity.GetCurrent();
            var WP = new WindowsPrincipal(WI);

            return WP.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                // It is not possible to launch a ClickOnce app as administrator directly, so instead we launch the program as administrator in a new process.
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                // The following properties run the new process as administrator
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";

                // Start the new process
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception)
                {
                    // The user did not allow the program to run as administrator
                    MessageBox.Show("Sorry, this application must be run as Administrator.");
                }

                // Shut down the current process
                Application.Exit();
            }
            else
            {
                // Since we're running as administrator now, carry on.
                this.Text = Application.ProductName + " - " + Application.ProductVersion + " (Release)";

                // Since the global profile ("SphtBotv3" SubKey) is 2 SubKeys up from the "Profiles" SubKey, it needs to be declared separately
                // Running the bot without ProfileLauncher reads from CurrentUser otherwise, PL reads the global profile from LocalMachine (this needs to be fixed)
                //RegistryKey regKeyGlobal = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                RegistryKey regKeyProfile = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\", RegistryKeyPermissionCheck.ReadWriteSubTree);

                // For each SubKey listed under the "Profiles" Key, create the DWORD and STRING values if they don't exist. This works well with foreach
                // This is old code, I had to restructure it a little bit by taking out the foreach statement
                // Since txtCDKey.Text and txtXPCDKey.Text can be multiple values (probably should of done an array - keeping this in mind)
                /*
                foreach (string rValue in regKeyProfile.GetSubKeyNames())
                {
                    // Initiate a new RegistryKey and create all the values if they don't exist for every SubKey that does exist under the "Profiles" SubKey
                    using (RegistryKey regKey = regKeyProfile.CreateSubKey(rValue))
                    {
                        if ((string)regKeyProfile.GetValue("BNLS Address") == System.String.Empty) regKeyProfile.SetValue("BNLS Address", "bnls.mattkv.net", RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Bind IP") == System.String.Empty) regKeyProfile.SetValue("Bind IP", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("BotNet Account Name") == System.String.Empty) regKeyProfile.SetValue("BotNet Account Name", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("BotNet Account Password") == System.String.Empty) regKeyProfile.SetValue("BotNet Account Password", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("BotNet Database Mask") == System.String.Empty) regKeyProfile.SetValue("BotNet Database Mask", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("BotNet Password") == System.String.Empty) regKeyProfile.SetValue("BotNet Password", "b8f9b319f223ddcc38", RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("BotNet Server") == System.String.Empty) regKeyProfile.SetValue("BotNet Server", "none", RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Brood War CD-Key") == System.String.Empty) regKeyProfile.SetValue("Brood War CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("CD-Key User") == System.String.Empty) regKeyProfile.SetValue("CD-Key User", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Diablo CD-Key") == System.String.Empty) regKeyProfile.SetValue("Diablo CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Diablo II CD-Key") == System.String.Empty) regKeyProfile.SetValue("Diablo II CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Diablo II: LoD CD-Key") == System.String.Empty) regKeyProfile.SetValue("Diablo II: LoD CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Diablo Shareware CD-Key") == System.String.Empty) regKeyProfile.SetValue("Diablo Shareware CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("E-Mail") == System.String.Empty) regKeyProfile.SetValue("E-Mail", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Home Channel") == System.String.Empty) regKeyProfile.SetValue("Home Channel", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Ignore Plugin Mask") == System.String.Empty) regKeyProfile.SetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("IRC Channels") == System.String.Empty) regKeyProfile.SetValue("IRC Channels", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("IRC E-Mail") == System.String.Empty) regKeyProfile.SetValue("IRC E-Mail", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("IRC Nickname") == System.String.Empty) regKeyProfile.SetValue("IRC Nickname", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("IRC Password") == System.String.Empty) regKeyProfile.SetValue("IRC Password", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("IRC Server") == System.String.Empty) regKeyProfile.SetValue("IRC Server", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("IRC Username") == System.String.Empty) regKeyProfile.SetValue("IRC Username", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Japan StarCraft CD-Key") == System.String.Empty) regKeyProfile.SetValue("StarCraft CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Message Filters") == System.String.Empty) regKeyProfile.SetValue("Message Filters", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Notify Keyword") == System.String.Empty) regKeyProfile.SetValue("Notify Keyword", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Password") == System.String.Empty) regKeyProfile.SetValue("Password", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Platform") == System.String.Empty) regKeyProfile.SetValue("Platform", "IX86", RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Realm Character Name") == System.String.Empty) regKeyProfile.SetValue("Realm Character Name", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Realm Name") == System.String.Empty) regKeyProfile.SetValue("Realm Name", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Server") == System.String.Empty) regKeyProfile.SetValue("Server", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("StarCraft CD-Key") == System.String.Empty) regKeyProfile.SetValue("StarCraft CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("StarCraft Shareware CD-Key") == System.String.Empty) regKeyProfile.SetValue("StarCraft Shareware CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("The Frozen Throne CD-Key") == System.String.Empty) regKeyProfile.SetValue("The Frozen Throne CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Timestamp") == System.String.Empty) regKeyProfile.SetValue("User Filters", "HH:MM:SS AM/PM", RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("User Filters") == System.String.Empty) regKeyProfile.SetValue("User Filters", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("Username") == System.String.Empty) regKeyProfile.SetValue("Username", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("WarCraft II CD-Key") == System.String.Empty) regKeyProfile.SetValue("WarCraft II CD-Key", System.String.Empty, RegistryValueKind.String);
                        if ((string)regKeyProfile.GetValue("WarCraft III CD-Key") == System.String.Empty) regKeyProfile.SetValue("WarCraft III CD-Key", System.String.Empty, RegistryValueKind.String);
                        if (regKeyProfile.GetValue("Auto Rejoin") == null) regKeyProfile.SetValue("Auto Rejoin", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Away Idle") == null) regKeyProfile.SetValue("Away Idle", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Bleed Timestamps") == null) regKeyProfile.SetValue("Bleed Timestamps", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Channel Order") == null) regKeyProfile.SetValue("Channel Order", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Connect to Battle.net") == null) regKeyProfile.SetValue("Connect to Battle.net", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Describe User Flags") == null) regKeyProfile.SetValue("Describe User Flags", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Disable Windows Notify") == null) regKeyProfile.SetValue("Disable Windows Notify", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Display AD-Banner") == null) regKeyProfile.SetValue("Display AD-Banner", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Extended Whois") == null) regKeyProfile.SetValue("Extended Whois", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Ignore Ping") == null) regKeyProfile.SetValue("Ignore Ping", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("No UDP") == null) regKeyProfile.SetValue("No UDP", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Notify 2") == null) regKeyProfile.SetValue("Notify 2", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Notify") == null) regKeyProfile.SetValue("Notify", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Post-Reply Ping") == null) regKeyProfile.SetValue("Post-Reply Ping", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Product") == null) regKeyProfile.SetValue("Product", 1, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Realm Character Class") == null) regKeyProfile.SetValue("Realm Character Class", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Show Undecoded") == null) regKeyProfile.SetValue("Show Undecoded", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("Spawn") == null) regKeyProfile.SetValue("Spawn", 0, RegistryValueKind.DWord);
                        if (regKeyProfile.GetValue("UDP Port") == null) regKeyProfile.SetValue("UDP Port", 0, RegistryValueKind.DWord);
                    }
                }

                // If the values for the global profile SubKey doesn't exist, it will be created with the default value of a null string or a DWORD (0x00000000) for the global profile ("SphtBotv3" SubKey)
                if ((string)regKeyGlobal.GetValue("BNLS Address") == System.String.Empty) regKeyGlobal.SetValue("BNLS Address", "bnls.mattkv.net", RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Bind IP") == System.String.Empty) regKeyGlobal.SetValue("Bind IP", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("BotNet Account Name") == System.String.Empty) regKeyGlobal.SetValue("BotNet Account Name", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("BotNet Account Password") == System.String.Empty) regKeyGlobal.SetValue("BotNet Account Password", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("BotNet Database Mask") == System.String.Empty) regKeyGlobal.SetValue("BotNet Database Mask", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("BotNet Password") == System.String.Empty) regKeyGlobal.SetValue("BotNet Password", "b8f9b319f223ddcc38", RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("BotNet Server") == System.String.Empty) regKeyGlobal.SetValue("BotNet Server", "none", RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Brood War CD-Key") == System.String.Empty) regKeyGlobal.SetValue("Brood War CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("CD-Key User") == System.String.Empty) regKeyGlobal.SetValue("CD-Key User", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Diablo CD-Key") == System.String.Empty) regKeyGlobal.SetValue("Diablo CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Diablo II CD-Key") == System.String.Empty) regKeyGlobal.SetValue("Diablo II CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Diablo II: LoD CD-Key") == System.String.Empty) regKeyGlobal.SetValue("Diablo II: LoD CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Diablo Shareware CD-Key") == System.String.Empty) regKeyGlobal.SetValue("Diablo Shareware CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("E-Mail") == System.String.Empty) regKeyGlobal.SetValue("E-Mail", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Home Channel") == System.String.Empty) regKeyGlobal.SetValue("Home Channel", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Ignore Plugin Mask") == System.String.Empty) regKeyGlobal.SetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("IRC Channels") == System.String.Empty) regKeyGlobal.SetValue("IRC Channels", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("IRC E-Mail") == System.String.Empty) regKeyGlobal.SetValue("IRC E-Mail", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("IRC Nickname") == System.String.Empty) regKeyGlobal.SetValue("IRC Nickname", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("IRC Password") == System.String.Empty) regKeyGlobal.SetValue("IRC Password", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("IRC Server") == System.String.Empty) regKeyGlobal.SetValue("IRC Server", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("IRC Username") == System.String.Empty) regKeyGlobal.SetValue("IRC Username", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Japan StarCraft CD-Key") == System.String.Empty) regKeyGlobal.SetValue("StarCraft CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Message Filters") == System.String.Empty) regKeyGlobal.SetValue("Message Filters", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Notify Keyword") == System.String.Empty) regKeyGlobal.SetValue("Notify Keyword", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Password") == System.String.Empty) regKeyGlobal.SetValue("Password", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Platform") == System.String.Empty) regKeyGlobal.SetValue("Platform", "IX86", RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Realm Character Name") == System.String.Empty) regKeyGlobal.SetValue("Realm Character Name", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Realm Name") == System.String.Empty) regKeyGlobal.SetValue("Realm Name", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Server") == System.String.Empty) regKeyGlobal.SetValue("Server", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("StarCraft CD-Key") == System.String.Empty) regKeyGlobal.SetValue("StarCraft CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("StarCraft Shareware CD-Key") == System.String.Empty) regKeyGlobal.SetValue("StarCraft Shareware CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("The Frozen Throne CD-Key") == System.String.Empty) regKeyGlobal.SetValue("The Frozen Throne CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Timestamp") == System.String.Empty) regKeyGlobal.SetValue("User Filters", "HH:MM:SS AM/PM", RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("User Filters") == System.String.Empty) regKeyGlobal.SetValue("User Filters", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("Username") == System.String.Empty) regKeyGlobal.SetValue("Username", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("WarCraft II CD-Key") == System.String.Empty) regKeyGlobal.SetValue("WarCraft II CD-Key", System.String.Empty, RegistryValueKind.String);
                if ((string)regKeyGlobal.GetValue("WarCraft III CD-Key") == System.String.Empty) regKeyGlobal.SetValue("WarCraft III CD-Key", System.String.Empty, RegistryValueKind.String);
                if (regKeyGlobal.GetValue("Auto Rejoin") == null) regKeyGlobal.SetValue("Auto Rejoin", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Away Idle") == null) regKeyGlobal.SetValue("Away Idle", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Bleed Timestamps") == null) regKeyGlobal.SetValue("Bleed Timestamps", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Channel Order") == null) regKeyGlobal.SetValue("Channel Order", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Connect to Battle.net") == null) regKeyGlobal.SetValue("Connect to Battle.net", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Describe User Flags") == null) regKeyGlobal.SetValue("Describe User Flags", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Disable Windows Notify") == null) regKeyGlobal.SetValue("Disable Windows Notify", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Display AD-Banner") == null) regKeyGlobal.SetValue("Display AD-Banner", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Extended Whois") == null) regKeyGlobal.SetValue("Extended Whois", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Ignore Ping") == null) regKeyGlobal.SetValue("Ignore Ping", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("No UDP") == null) regKeyGlobal.SetValue("No UDP", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Notify 2") == null) regKeyGlobal.SetValue("Notify 2", 1, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Notify") == null) regKeyGlobal.SetValue("Notify", 1, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Post-Reply Ping") == null) regKeyGlobal.SetValue("Post-Reply Ping", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Product") == null) regKeyGlobal.SetValue("Product", 1, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Realm Character Class") == null) regKeyGlobal.SetValue("Realm Character Class", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Show Undecoded") == null) regKeyGlobal.SetValue("Show Undecoded", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("Spawn") == null) regKeyGlobal.SetValue("Spawn", 0, RegistryValueKind.DWord);
                if (regKeyGlobal.GetValue("UDP Port") == null) regKeyGlobal.SetValue("UDP Port", 0, RegistryValueKind.DWord);
                */

                try
                {
                    // Since retrieving SubKeys is an array, declare it as such. It's an array because it's possible for more than 1 string SubKey (Profile in this case) to exist
                    string[] regName = regKeyProfile.GetSubKeyNames();

                    // Count the keys (int) and convert it to a string so it can be assigned to the ComboBox as text
                    lblProfiles.Text = string.Format("{0} profile{1} found", regKeyProfile.SubKeyCount.ToString(), regKeyProfile.SubKeyCount != 1 ? "s" : "");

                    foreach (string Value in regName)
                    {
                        // So add that SubKey to the ComboBox
                        if (!cboProfiles.Items.Contains(Value))
                        {
                            cboProfiles.Items.Add(Value);
                        }
                        else
                            cboProfiles.Items.Remove(Value);
                    }

                    // Adds the global profile in the ComboBox for selection
                    if (!cboProfiles.Items.Contains("Global"))
                        cboProfiles.Items.Add("Global");

                    cboProfiles.Text = "Global";

                    // Closes the registry until it's used again either by read or write
                    regKeyProfile.Close();
                    //regKeyGlobal.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Read()
        {
            // If the combo box text says "Global", then use the CurrentUser Registry, otherwise use the LocalMachine (where the profiles are held)
            bool global = cboProfiles.Text == "Global";
            RegistryKey regKey = global ? Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree) :
                                          Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\" + cboProfiles.Text, RegistryKeyPermissionCheck.ReadWriteSubTree);

            try
            {
                // Get values of all the designated keys. Since the DWORD values come out as strings, they need to be converted
                // Since the DWORD values come back as 1 or 0 due to how the program recognizes it, this basically translates 1 into Enabled or 0 into Disabled using a Ternary Operator
                // DropDownList items can't be temporarily edited like DropDown style can. Thus read it straight from the Registry, then assign the value
                cboADBanner.Text = Convert.ToString((Int32)regKey.GetValue("Display AD-Banner")) == "1" ? "Enabled" : "Disabled";
                cboAwayIdle.Text = Convert.ToString((Int32)regKey.GetValue("Away Idle")) == "1" ? "Enabled" : "Disabled";
                cboBNLSAddress.Text = (string)regKey.GetValue("BNLS Address") == System.String.Empty ? "bnls.mattkv.net" : (string)regKey.GetValue("BNLS Address");
                cboBleedTimestamps.Text = Convert.ToString((Int32)regKey.GetValue("Bleed Timestamps")) == "1" ? "Enabled" : "Disabled";
                cboChannelOrder.Text = Convert.ToString((Int32)regKey.GetValue("Channel Order")) == "1" ? "Enabled" : "Disabled";
                cboDescribeUserFlags.Text = Convert.ToString((Int32)regKey.GetValue("Describe User Flags")) == "1" ? "Enabled" : "Disabled";
                cboExtendedWhois.Text = Convert.ToString((Int32)regKey.GetValue("Extended Whois")) == "1" ? "Enabled" : "Disabled";
                cboIRCServer.Text = (string)regKey.GetValue("IRC Server", System.String.Empty, RegistryValueOptions.None);
                cboNotify.Text = Convert.ToString((Int32)regKey.GetValue("Disable Windows Notify")) == "0" ? "Enabled" : "Disabled";
                cboRealmName.Text = (string)regKey.GetValue("Realm Name") == System.String.Empty ? "USEast" : (string)regKey.GetValue("Realm Name");
                cboServer.Text = (string)regKey.GetValue("Server") == System.String.Empty ? "useast.battle.net" : (string)regKey.GetValue("Server");
                cboShowUndecoded.Text = Convert.ToString((Int32)regKey.GetValue("Show Undecoded")) == "1" ? "Enabled" : "Disabled";
                chkAutoRejoin.Checked = Convert.ToString((Int32)regKey.GetValue("Auto Rejoin")) == "1" ? true : false;
                chkBNETAutoCon.Checked = Convert.ToString((Int32)regKey.GetValue("Connect to Battle.net")) == "1" ? true : false;
                chkBanKick.Checked = Convert.ToString((Int32)regKey.GetValue("Notify 2")) == "1" ? true : false;
                chkCDKey.Checked = Convert.ToString((Int32)regKey.GetValue("Spawn")) == "1" ? true : false;
                chkJoinLeave.Checked = Convert.ToString((Int32)regKey.GetValue("Notify")) == "1" ? true : false;
                chkUDP.Checked = Convert.ToString((Int32)regKey.GetValue("No UDP")) == "1" ? true : false;
                txtBNETPassword.Text = (string)regKey.GetValue("Password", System.String.Empty);
                txtBNETUsername.Text = (string)regKey.GetValue("Username", System.String.Empty, RegistryValueOptions.None);
                txtBindIP.Text = (string)regKey.GetValue("Bind IP", System.String.Empty, RegistryValueOptions.None);
                txtBotNetPassword.Text = (string)regKey.GetValue("BotNet Account Password", System.String.Empty, RegistryValueOptions.None);
                txtBotNetServer.Text = (string)regKey.GetValue("BotNet Server", System.String.Empty, RegistryValueOptions.None);
                txtBotNetUsername.Text = (string)regKey.GetValue("BotNet Account Name", System.String.Empty, RegistryValueOptions.None);
                txtCDKeyUser.Text = (string)regKey.GetValue("CD-Key User", System.String.Empty, RegistryValueOptions.None);
                txtEMail.Text = (string)regKey.GetValue("E-Mail", System.String.Empty, RegistryValueOptions.None);
                txtHome.Text = (string)regKey.GetValue("Home Channel", System.String.Empty, RegistryValueOptions.None);
                txtIRCAccount.Text = (string)regKey.GetValue("IRC Nickname", System.String.Empty, RegistryValueOptions.None);
                txtIRCChannels.Text = (string)regKey.GetValue("IRC Channels", System.String.Empty, RegistryValueOptions.None);
                txtIRCEmail.Text = (string)regKey.GetValue("IRC E-Mail", System.String.Empty, RegistryValueOptions.None);
                txtIRCName.Text = (string)regKey.GetValue("IRC Username", System.String.Empty, RegistryValueOptions.None);
                txtIRCPassword.Text = (string)regKey.GetValue("IRC Password", System.String.Empty, RegistryValueOptions.None);
                txtIgnorePluginMask.Text = (string)regKey.GetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueOptions.None);
                txtMask.Text = (string)regKey.GetValue("BotNet Database Mask", System.String.Empty, RegistryValueOptions.None);
                txtNotify.Text = (string)regKey.GetValue("Notify Keyword", System.String.Empty, RegistryValueOptions.None);
                txtRealmCharacter.Text = (string)regKey.GetValue("Realm Character Name", System.String.Empty, RegistryValueOptions.None);
                txtUDPPort.Text = Convert.ToString((Int32)regKey.GetValue("UDP Port", 0, RegistryValueOptions.None));

                string BN = (string)regKey.GetValue("BotNet Database", System.String.Empty, RegistryValueOptions.None);
                string[] BotNet = BN.Split(' ');

                string Value1 = BotNet[0];
                string Value2 = BotNet[1];

                txtBotNetDatabase.Text = Value1;
                txtDatabasePassword.Text = Value2;

                if (Convert.ToString((Int32)regKey.GetValue("Ignore Ping")) == "1")
                    cboPing.Text = "Ignore pre-logon ping (-1ms ping)";
                else if (Convert.ToString((Int32)regKey.GetValue("Post-Reply Ping")) == "1")
                    cboPing.Text = "Post-send pre-logon ping (0ms ping)";
                else if (Convert.ToString((Int32)regKey.GetValue("Ignore Ping")) == "0" && Convert.ToString((Int32)regKey.GetValue("Post-Reply Ping")) == "0")
                    cboPing.Text = "None";

                if ((string)regKey.GetValue("Platform") == "IX86")
                    cboPlatform.Text = "Intel x86";
                else if ((string)regKey.GetValue("Platform") == "PMAC")
                    cboPlatform.Text = "Power Macintosh";
                else if ((string)regKey.GetValue("Platform") == "XMAC")
                    cboPlatform.Text = "Macintosh (Mac OS X)";

                switch (Convert.ToString((Int32)regKey.GetValue("Product")))
                {
                    case "1":
                        cboProduct.Text = "StarCraft";
                        txtCDKey.Text = (string)regKey.GetValue("StarCraft CD-Key");
                        break;
                    case "2":
                        cboProduct.Text = "StarCraft: Brood War";
                        txtCDKey.Text = (string)regKey.GetValue("Brood War CD-Key");
                        break;
                    case "3":
                        cboProduct.Text = "Warcraft II: Battle.net Edition";
                        txtCDKey.Text = (string)regKey.GetValue("WarCraft II CD-Key");
                        break;
                    case "4":
                        cboProduct.Text = "Diablo II";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo II CD-Key");
                        break;
                    case "5":
                        cboProduct.Text = "Diablo II: Lord of Destruction";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo II CD-Key");
                        txtXPCDKey.Text = (string)regKey.GetValue("Diablo II: LoD CD-Key");
                        break;
                    case "6":
                        cboProduct.Text = "StarCraft: Japan";
                        txtCDKey.Text = (string)regKey.GetValue("Japan StarCraft CD-Key");
                        break;
                    case "7":
                        cboProduct.Text = "WarCraft III: Reign of Chaos";
                        txtCDKey.Text = (string)regKey.GetValue("WarCraft III CD-Key");
                        break;
                    case "8":
                        cboProduct.Text = "WarCraft III: The Frozen Throne";
                        txtCDKey.Text = (string)regKey.GetValue("WarCraft III CD-Key");
                        txtXPCDKey.Text = (string)regKey.GetValue("The Frozen Throne CD-Key");
                        break;
                    case "9":
                        cboProduct.Text = "Diablo";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo CD-Key");
                        break;
                    case "10":
                        cboProduct.Text = "Diablo Shareware";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo Shareware CD-Key");
                        break;
                    case "11":
                        cboProduct.Text = "StarCraft Shareware";
                        txtCDKey.Text = (string)regKey.GetValue("StarCraft Shareware CD-Key");
                        break;
                    default:
                        break;
                }

                switch (Convert.ToString((Int32)regKey.GetValue("Realm Character Class")))
                {
                    case "0":
                        cboClass.Text = "Amazon";
                        break;
                    case "1":
                        cboClass.Text = "Sorceress";
                        break;
                    case "2":
                        cboClass.Text = "Necromancer";
                        break;
                    case "3":
                        cboClass.Text = "Paladin";
                        break;
                    case "4":
                        cboClass.Text = "Barbarian";
                        break;
                    case "5":
                        cboClass.Text = "Druid";
                        break;
                    case "6":
                        cboClass.Text = "Assassin";
                        break;
                    default:
                        break;
                }

                regKey.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetAbled(RegistryKey regKey, ComboBox cboB, string strKey, RegistryValueKind regKind)
        {
            // regKey is passed over from Write() depending on a Profile or Global.
            // The SubKey name is passed, if the text on the ComboBox says Enabled write a 1 else a 0 value DWORD
            if (cboB != cboNotify)
                regKey.SetValue(strKey, cboB.Text == "Enabled" ? 1 : 0, regKind);
            else
                regKey.SetValue(strKey, cboB.Text == "Enabled" ? 0 : 1, regKind);
        }

        private void SetAble(RegistryKey regKey, CheckBox chkB, string strKey, RegistryValueKind regKind)
        {
            // regKey is passed over from Write() depending on a Profile or Global.
            // The SubKey name is passed, if the CheckBox is checked then write a 1 else a 0 value DWORD
            regKey.SetValue(strKey, chkB.Checked == true ? 1 : 0, regKind);
        }

        private void SetValue(RegistryKey regKey, string strKey, string Value, RegistryValueKind regKind)
        {
            // regKey is passed over from Write() depending on a Profile or Global.
            // The SubKey name is passed and a string value is assigned to it
            regKey.SetValue(strKey, Value, regKind);
        }

        private void Write()
        {
            bool global = cboProfiles.Text == "Global";
            bool result = true;
            RegistryKey regKey = global ? Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree) :
                                          Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\" + cboProfiles.Text, RegistryKeyPermissionCheck.ReadWriteSubTree);

            try
            {
                // SetAbled(regKey (Global or Profile), ComboBoxName, SubKeyName, Dword/String/QWord,etc)
                SetAbled(regKey, cboADBanner, "Display AD-Banner", RegistryValueKind.DWord);
                SetAbled(regKey, cboAwayIdle, "Away Idle", RegistryValueKind.DWord);
                SetAbled(regKey, cboBleedTimestamps, "Bleed Timestamps", RegistryValueKind.DWord);
                SetAbled(regKey, cboChannelOrder, "Channel Order", RegistryValueKind.DWord);
                SetAbled(regKey, cboDescribeUserFlags, "Describe User Flags", RegistryValueKind.DWord);
                SetAbled(regKey, cboExtendedWhois, "Extended Whois", RegistryValueKind.DWord);
                SetAbled(regKey, cboNotify, "Disable Windows Notify", RegistryValueKind.DWord);
                SetAbled(regKey, cboShowUndecoded, "Show Undecoded", RegistryValueKind.DWord);

                // SetAble(regKey (Global or Profile), CheckBoxName, SubKeyName, Dword/String/QWord,etc)
                SetAble(regKey, chkAutoRejoin, "Auto Rejoin", RegistryValueKind.DWord);
                SetAble(regKey, chkBNETAutoCon, "Connect to Battle.net", RegistryValueKind.DWord);
                SetAble(regKey, chkBanKick, "Notify 2", RegistryValueKind.DWord);
                SetAble(regKey, chkCDKey, "Spawn", RegistryValueKind.DWord);
                SetAble(regKey, chkJoinLeave, "Notify", RegistryValueKind.DWord);
                SetAble(regKey, chkUDP, "No UDP", RegistryValueKind.DWord);

                // SetValue(regKey (Global or Profile), SubKeyName, TextDisplayed, String)
                SetValue(regKey, "BNLS Address", cboBNLSAddress.Text, RegistryValueKind.String);
                SetValue(regKey, "Bind IP", txtBindIP.Text, RegistryValueKind.String);
                SetValue(regKey, "BotNet Account Name", txtBotNetUsername.Text, RegistryValueKind.String);
                SetValue(regKey, "BotNet Account Password", txtBotNetPassword.Text, RegistryValueKind.String);
                SetValue(regKey, "BotNet Database", txtBotNetDatabase.Text + " " + txtDatabasePassword.Text, RegistryValueKind.String);
                SetValue(regKey, "BotNet Database Mask", txtMask.Text, RegistryValueKind.String);
                SetValue(regKey, "BotNet Server", txtBotNetServer.Text, RegistryValueKind.String);
                SetValue(regKey, "CD-Key User", txtCDKeyUser.Text, RegistryValueKind.String);
                SetValue(regKey, "E-Mail", txtEMail.Text, RegistryValueKind.String);
                SetValue(regKey, "Home Channel", txtHome.Text, RegistryValueKind.String);
                SetValue(regKey, "Ignore Plugin Mask", txtIgnorePluginMask.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Channels", txtIRCChannels.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC E-Mail", txtIRCEmail.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Nickname", txtIRCAccount.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Password", txtIRCPassword.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Server", cboIRCServer.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Username", txtIRCName.Text, RegistryValueKind.String);
                SetValue(regKey, "Notify Keyword", txtNotify.Text, RegistryValueKind.String);
                SetValue(regKey, "Password", txtBNETPassword.Text, RegistryValueKind.String);
                SetValue(regKey, "Realm Character Name", txtRealmCharacter.Text, RegistryValueKind.String);
                SetValue(regKey, "Realm Name", cboRealmName.Text, RegistryValueKind.String);
                SetValue(regKey, "Server", cboServer.Text, RegistryValueKind.String);
                SetValue(regKey, "UDP Port", txtUDPPort.Text, RegistryValueKind.DWord);
                SetValue(regKey, "Username", txtBNETUsername.Text, RegistryValueKind.String);

                if (cboPing.Text == "Ignore pre-logon ping (-1ms ping)")
                    regKey.SetValue("Ignore Ping", 1, RegistryValueKind.DWord);
                else if (cboPing.Text == "Post-send pre-logon ping (0ms ping)")
                    regKey.SetValue("Post-Reply Ping", 1, RegistryValueKind.DWord);
                else if (cboPing.Text == "None")
                {
                    regKey.SetValue("Post-Reply Ping", 0, RegistryValueKind.DWord);
                    regKey.SetValue("Ignore Ping", 0, RegistryValueKind.DWord);
                }

                if (cboPlatform.Text == "Intel x86")
                    regKey.SetValue("Platform", "IX86", RegistryValueKind.String);
                else if (cboPlatform.Text == "Power Macintosh")
                    regKey.SetValue("Platform", "PMAC", RegistryValueKind.String);
                else if (cboPlatform.Text == "Macintosh (Mac OS X)")
                    regKey.SetValue("Platform", "XMAC", RegistryValueKind.String);

                switch (cboProduct.Text)
                {
                    case "StarCraft":
                        regKey.SetValue("Product", 1, RegistryValueKind.DWord);
                        SetValue(regKey, "StarCraft CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "StarCraft: Brood War":
                        regKey.SetValue("Product", 2, RegistryValueKind.DWord);
                        regKey.SetValue("Brood War CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "Warcraft II: Battle.net Edition":
                        regKey.SetValue("Product", 3, RegistryValueKind.DWord);
                        regKey.SetValue("Warcraft II CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "Diablo II":
                        regKey.SetValue("Product", 4, RegistryValueKind.DWord);
                        regKey.SetValue("Diablo II CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "Diablo II: Lord of Destruction":
                        regKey.SetValue("Product", 5, RegistryValueKind.DWord);
                        regKey.SetValue("Diablo II: LoD CD-Key", txtXPCDKey.Text, RegistryValueKind.String);
                        break;
                    case "StarCraft: Japan":
                        regKey.SetValue("Product", 6, RegistryValueKind.DWord);
                        regKey.SetValue("Japan StarCraft CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "WarCraft III: Reign of Chaos":
                        regKey.SetValue("Product", 7, RegistryValueKind.DWord);
                        regKey.SetValue("WarCraft III CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "WarCraft III: The Frozen Throne":
                        regKey.SetValue("Product", 8, RegistryValueKind.DWord);
                        regKey.SetValue("The Frozen Throne CD-Key", txtXPCDKey.Text, RegistryValueKind.String);
                        break;
                    case "Diablo":
                        regKey.SetValue("Product", 9, RegistryValueKind.DWord);
                        regKey.SetValue("Diablo CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "Diablo Shareware":
                        regKey.SetValue("Product", 10, RegistryValueKind.DWord);
                        regKey.SetValue("Diablo Shareware CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    case "StarCraft Shareware":
                        regKey.SetValue("Product", 11, RegistryValueKind.DWord);
                        regKey.SetValue("StarCraft Shareware CD-Key", txtCDKey.Text, RegistryValueKind.String);
                        break;
                    default:
                        break;
                }

                switch (cboClass.Text)
                {
                    case "Amazon":
                        regKey.SetValue("Realm Character Class", 0, RegistryValueKind.DWord);
                        break;
                    case "Sorceress":
                        regKey.SetValue("Realm Character Class", 1, RegistryValueKind.DWord);
                        break;
                    case "Necromancer":
                        regKey.SetValue("Realm Character Class", 2, RegistryValueKind.DWord);
                        break;
                    case "Paladin":
                        regKey.SetValue("Realm Character Class", 3, RegistryValueKind.DWord);
                        break;
                    case "Barbarian":
                        regKey.SetValue("Realm Character Class", 4, RegistryValueKind.DWord);
                        break;
                    case "Druid":
                        regKey.SetValue("Realm Character Class", 5, RegistryValueKind.DWord);
                        break;
                    case "Assassin":
                        regKey.SetValue("Realm Character Class", 6, RegistryValueKind.DWord);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            switch (result)
            {
                case true:
                    MessageBox.Show("Writing to registry was successful!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }   
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Read();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAbout();
            frm.Show();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            Write();
        }

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangeLog();
            frm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form frm = new frmEdit(cboProfiles.Text);
            frm.Text = string.Format("Editing profile: {0}", cboProfiles.Text);
            frm.Show();
        }

        private void tmrEditButton_Tick(object sender, EventArgs e)
        {
            btnEdit.Enabled = cboProfiles.Text == "Global" ? false : true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboProfiles.Items.Clear();
            frmMain_Load(sender, e);
        }

        #region Buttons
        private void btnBNETPassword_Click(object sender, EventArgs e)
        {
            switch (txtBNETPassword.UseSystemPasswordChar)
            {
                case true:
                    txtBNETPassword.UseSystemPasswordChar = false;
                    break;
                default:
                    txtBNETPassword.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnEMail_Click(object sender, EventArgs e)
        {
            switch (txtEMail.UseSystemPasswordChar)
            {
                case true:
                    txtEMail.UseSystemPasswordChar = false;
                    break;
                default:
                    txtEMail.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnCDKey_Click(object sender, EventArgs e)
        {
            switch (txtCDKey.UseSystemPasswordChar)
            {
                case true:
                    txtCDKey.UseSystemPasswordChar = false;
                    break;
                default:
                    txtCDKey.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnXPCDKey_Click(object sender, EventArgs e)
        {
            switch (txtXPCDKey.UseSystemPasswordChar)
            {
                case true:
                    txtXPCDKey.UseSystemPasswordChar = false;
                    break;
                default:
                    txtXPCDKey.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnBotNetServer_Click(object sender, EventArgs e)
        {
            switch (txtBotNetServer.UseSystemPasswordChar)
            {
                case true:
                    txtBotNetServer.UseSystemPasswordChar = false;
                    break;
                default:
                    txtBotNetServer.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnDatabasePassword_Click(object sender, EventArgs e)
        {
            switch (txtDatabasePassword.UseSystemPasswordChar)
            {
                case true:
                    txtDatabasePassword.UseSystemPasswordChar = false;
                    break;
                default:
                    txtDatabasePassword.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnBotNetPassword_Click(object sender, EventArgs e)
        {
            switch (txtBotNetPassword.UseSystemPasswordChar)
            {
                case true:
                    txtBotNetPassword.UseSystemPasswordChar = false;
                    break;
                default:
                    txtBotNetPassword.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnIRCName_Click(object sender, EventArgs e)
        {
            switch (txtIRCName.UseSystemPasswordChar)
            {
                case true:
                    txtIRCName.UseSystemPasswordChar = false;
                    break;
                default:
                    txtIRCName.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnIRCEmail_Click(object sender, EventArgs e)
        {
            switch (txtIRCEmail.UseSystemPasswordChar)
            {
                case true:
                    txtIRCEmail.UseSystemPasswordChar = false;
                    break;
                default:
                    txtIRCEmail.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnIRCPassword_Click(object sender, EventArgs e)
        {
            switch (txtIRCPassword.UseSystemPasswordChar)
            {
                case true:
                    txtIRCPassword.UseSystemPasswordChar = false;
                    break;
                default:
                    txtIRCPassword.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnPerform_Click(object sender, EventArgs e)
        {
            switch (txtPerform.UseSystemPasswordChar)
            {
                case true:
                    txtPerform.UseSystemPasswordChar = false;
                    break;
                default:
                    txtPerform.UseSystemPasswordChar = true;
                    break;
            }
        }
        #endregion

        // This is to display which RegistryKeyValue in "real-time" with whatever Product you select
        private void cboProduct_SelectedItemChanged(object sender, EventArgs e)
        {
            bool global = cboProfiles.Text == "Global";
            RegistryKey regKey = global ? Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree) :
                                          Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\" + cboProfiles.Text, RegistryKeyPermissionCheck.ReadWriteSubTree);

            switch (cboProduct.Text)
            {
                case "StarCraft":
                    txtCDKey.Text = (string)regKey.GetValue("StarCraft CD-Key");
                    break;
                case "StarCraft: Brood War":
                    txtCDKey.Text = (string)regKey.GetValue("Brood War CD-Key");
                    break;
                case "WarCraft II: Battle.net Edition":
                    txtCDKey.Text = (string)regKey.GetValue("WarCraft II CD-Key");
                    break;
                case "Diablo II":
                    txtCDKey.Text = (string)regKey.GetValue("Diablo II CD-Key");
                    break;
                case "Diablo II: Lord of Destruction":
                    txtCDKey.Text = (string)regKey.GetValue("Diablo II CD-Key");
                    txtXPCDKey.Text = (string)regKey.GetValue("Diablo II: LoD CD-Key");
                    break;
                case "StarCraft: Japan":
                    txtCDKey.Text = (string)regKey.GetValue("Japan StarCraft CD-Key");
                    break;
                case "WarCraft III: Reign of Chaos":
                    txtCDKey.Text = (string)regKey.GetValue("WarCraft III CD-Key");
                    break;
                case "WarCraft III: The Frozen Throne":
                    txtCDKey.Text = (string)regKey.GetValue("WarCraft III CD-Key");
                    txtXPCDKey.Text = (string)regKey.GetValue("The Frozen Throne CD-Key");
                    break;
                case "Diablo":
                    txtCDKey.Text = (string)regKey.GetValue("Diablo CD-Key");
                    break;
                case "Diablo Shareware":
                    txtCDKey.Text = (string)regKey.GetValue("Diablo Shareware CD-Key");
                    break;
                case "StarCraft Shareware":
                    txtCDKey.Text = (string)regKey.GetValue("StarCraft Shareware CD-Key");
                    break;
            }
        }
    }
}

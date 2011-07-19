using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Microsoft.Win32;
using System.Diagnostics;


namespace SphtBotv3_Registry_Manager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - " + Application.ProductVersion + " (Beta)";

            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3");

            // If the key doesn't exist, it will be created with the default value of a null string or a DWORD (0x00000000)
            if (regKey.GetValue("Away Idle") == null) regKey.SetValue("Away Idle", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("Bleed Timestamps") == null) regKey.SetValue("Bleed Timestamps", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("Channel Order") == null) regKey.SetValue("Channel Order", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("Describe User Flags") == null) regKey.SetValue("Describe User Flags", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("Display AD-Banner") == null) regKey.SetValue("Display AD-Banner", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("Extended Whois") == null) regKey.SetValue("Extended Whois", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("Show Undecoded") == null) regKey.SetValue("Show Undecoded", 0, RegistryValueKind.DWord);
            if (regKey.GetValue("UDP Port") == null) regKey.SetValue("UDP Port", 0, RegistryValueKind.DWord);
            if ((string)regKey.GetValue("Ignore Plugin Mask") == System.String.Empty) regKey.SetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueKind.String);
            if ((string)regKey.GetValue("Realm Character") == System.String.Empty) regKey.SetValue("Realm Character", System.String.Empty, RegistryValueKind.String);
            if ((string)regKey.GetValue("Realm Name") == System.String.Empty) regKey.SetValue("Realm Name", System.String.Empty, RegistryValueKind.String);
            if ((string)regKey.GetValue("Bind IP") == System.String.Empty) regKey.SetValue("Bind IP", System.String.Empty, RegistryValueKind.String);

            // Closes the registry until it's used again either by read or write
            regKey.Close();
        }

        private void Read()
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3");

            // Get values of all the designated keys. Since the DWORD values come out as strings, they need to be converted
            cboAwayIdle.Text = Convert.ToString((Int32)regKey.GetValue("Away Idle", 0, RegistryValueOptions.None));
            cboBleedTimestamps.Text = Convert.ToString((Int32)regKey.GetValue("Bleed Timestamps", 0, RegistryValueOptions.None));
            cboChannelOrder.Text = Convert.ToString((Int32)regKey.GetValue("Channel Order", 0, RegistryValueOptions.None));
            cboDescribeUserFlags.Text = Convert.ToString((Int32)regKey.GetValue("Describe User Flags", 0, RegistryValueOptions.None));
            cboADBanner.Text = Convert.ToString((Int32)regKey.GetValue("Display AD-Banner", 0, RegistryValueOptions.None));
            cboShowUndecoded.Text = Convert.ToString((Int32)regKey.GetValue("Show Undecoded", 0, RegistryValueOptions.None));
            cboExtendedWhois.Text = Convert.ToString((Int32)regKey.GetValue("Extended Whois", 0, RegistryValueOptions.None));
            txtUDPPort.Text = Convert.ToString((Int32)regKey.GetValue("UDP Port", 0, RegistryValueOptions.None));
            txtIgnorePluginMask.Text = (string)regKey.GetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueOptions.None);
            txtRealmCharacter.Text = (string)regKey.GetValue("Realm Character", System.String.Empty, RegistryValueOptions.None);
            cboRealmName.Text = (string)regKey.GetValue("Realm Name", "USEast", RegistryValueOptions.None);
            txtBindIP.Text = (string)regKey.GetValue("Bind IP", System.String.Empty, RegistryValueOptions.None);

            // Since the DWORD values come back as 1 or 0 due to how the program recognizes it, this basically translates 1 into Enabled or 0 into Disabled using a Ternary Operator
            cboBleedTimestamps.Text = cboBleedTimestamps.Text == "1" ? "Enabled" : "Disabled";
            cboAwayIdle.Text = cboAwayIdle.Text == "1" ? "Enabled" : "Disabled";
            cboChannelOrder.Text = cboChannelOrder.Text == "1" ? "Enabled" : "Disabled";
            cboShowUndecoded.Text = cboShowUndecoded.Text == "1" ? "Enabled" : "Disabled";
            cboDescribeUserFlags.Text = cboDescribeUserFlags.Text == "1" ? "Enabled" : "Disabled";
            cboADBanner.Text = cboADBanner.Text == "1" ? "Enabled" : "Disabled";
            cboExtendedWhois.Text = cboExtendedWhois.Text == "1" ? "Enabled" : "Disabled";

            regKey.Close();
        }

        private void Write()
        {
            bool Result;
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3");

            if (cboBleedTimestamps.Text == "Enabled") 
            { 
                regKey.SetValue("Bleed Timestamps", 1, RegistryValueKind.DWord); 
                Result = true; 
            } 
            else if (cboBleedTimestamps.Text == "Disabled") 
            { 
                regKey.SetValue("Bleed Timestamps", 0, RegistryValueKind.DWord); 
                Result = true;
            }
            else Result = false;

            if (cboChannelOrder.Text == "Enabled")
            {
                regKey.SetValue("Channel Order", 1, RegistryValueKind.DWord);
                Result = true;
            }
            else if (cboChannelOrder.Text == "Disabled")
            {
                regKey.SetValue("Channel Order", 0, RegistryValueKind.DWord);
                Result = true;
            }
            else Result = false;

            if (cboDescribeUserFlags.Text == "Enabled")
            {
                regKey.SetValue("Describe User Flags", 1, RegistryValueKind.DWord);
                Result = true;
            }
            else if (cboDescribeUserFlags.Text == "Disabled")
            {
                regKey.SetValue("Describe User Flags", 0, RegistryValueKind.DWord);
                Result = true;
            }
            else Result = false;

            if (cboADBanner.Text == "Enabled")
            {
                regKey.SetValue("Display AD-Banner", 1, RegistryValueKind.DWord);
                Result = true;
            }
            else if (cboADBanner.Text == "Disabled")
            {
                regKey.SetValue("Display AD-Banner", 0, RegistryValueKind.DWord);
                Result = true;
            }
            else Result = false;

            if (cboAwayIdle.Text == "Enabled")
            {
                regKey.SetValue("Away Idle", 1, RegistryValueKind.DWord);
                Result = true;
            }
            else if (cboAwayIdle.Text == "Disabled")
            {
                regKey.SetValue("Away Idle", 0, RegistryValueKind.DWord);
                Result = true;
            }
            else Result = false;

            if (cboShowUndecoded.Text == "Enabled")
            {
                regKey.SetValue("Show Undecoded", 1, RegistryValueKind.DWord);
                Result = true;
            }
            else if (cboShowUndecoded.Text == "Disabled")
            {
                regKey.SetValue("Show Undecoded", 0, RegistryValueKind.DWord);
                Result = true;
            }
            else Result = false;

            if (cboExtendedWhois.Text == "Enabled")
            {
                regKey.SetValue("Extended Whois", 1, RegistryValueKind.DWord);
                Result = true;
            }
            else if (cboExtendedWhois.Text == "Disabled")
            {
                regKey.SetValue("Extended Whois", 0, RegistryValueKind.DWord);
                Result = true;
            }
            else Result = false;

            regKey.SetValue("UDP Port", txtUDPPort.Text, RegistryValueKind.DWord);
            regKey.SetValue("Bind IP", txtBindIP.Text, RegistryValueKind.String);
            regKey.SetValue("Realm Name", cboRealmName.Text, RegistryValueKind.String);
            regKey.SetValue("Realm Character", txtRealmCharacter.Text, RegistryValueKind.String);
            regKey.SetValue("Ignore Plugin Mask", txtIgnorePluginMask.Text, RegistryValueKind.String);
            regKey.Close();

            if (Result == true)
                MessageBox.Show("Writing to registry was successful!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Writing to registry was unsuccessful!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}

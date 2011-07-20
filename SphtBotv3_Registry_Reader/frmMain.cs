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
            this.Text = Application.ProductName + " - " + Application.ProductVersion + " (Release)";

            // Adds the global profile in the ComboBox for selection
            cboProfiles.Items.Add("Global");

            // Since the global profile ("SphtBotv3" SubKey) is 2 SubKeys up from the "Profiles" SubKey, it needs to be declared separately
            RegistryKey regKeyGlobal = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3");
            RegistryKey regKeyProfile = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles");

            // For each SuBKey listed under the "Profiles" Key, create the DWORD and STRING values if they don't exist. This works well with foreach
            foreach (string rValue in regKeyProfile.GetSubKeyNames())
            {
                // Initiate a new RegistryKey and create all the values if they don't exist for every SubKey that does exist under the "Profiles" SubKey
                using (RegistryKey regKey = regKeyProfile.CreateSubKey(rValue))
                {
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
                }
            }

            // If the values for the global profile SubKey doesn't exist, it will be created with the default value of a null string or a DWORD (0x00000000) for the global profile ("SphtBotv3" SubKey)
            if (regKeyGlobal.GetValue("Away Idle") == null) regKeyGlobal.SetValue("Away Idle", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("Bleed Timestamps") == null) regKeyGlobal.SetValue("Bleed Timestamps", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("Channel Order") == null) regKeyGlobal.SetValue("Channel Order", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("Describe User Flags") == null) regKeyGlobal.SetValue("Describe User Flags", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("Display AD-Banner") == null) regKeyGlobal.SetValue("Display AD-Banner", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("Extended Whois") == null) regKeyGlobal.SetValue("Extended Whois", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("Show Undecoded") == null) regKeyGlobal.SetValue("Show Undecoded", 0, RegistryValueKind.DWord);
            if (regKeyGlobal.GetValue("UDP Port") == null) regKeyGlobal.SetValue("UDP Port", 0, RegistryValueKind.DWord);
            if ((string)regKeyGlobal.GetValue("Ignore Plugin Mask") == System.String.Empty) regKeyGlobal.SetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueKind.String);
            if ((string)regKeyGlobal.GetValue("Realm Character") == System.String.Empty) regKeyGlobal.SetValue("Realm Character", System.String.Empty, RegistryValueKind.String);
            if ((string)regKeyGlobal.GetValue("Realm Name") == System.String.Empty) regKeyGlobal.SetValue("Realm Name", System.String.Empty, RegistryValueKind.String);
            if ((string)regKeyGlobal.GetValue("Bind IP") == System.String.Empty) regKeyGlobal.SetValue("Bind IP", System.String.Empty, RegistryValueKind.String);

            // Since retrieving SubKeys is an array, declare it as such. It's an array because it's possible for more than 1 string SubKey (Profile in this case) to exist
            string[] regName = regKeyProfile.GetSubKeyNames();
            // Count the keys (int) and convert it to a string so it can be assigned to the ComboBox as text
            switch (regKeyProfile.SubKeyCount.ToString())
            {
                // If it comes back as 0, you have no profile SubKeys
                case "0":
                    lblProfiles.Text = regKeyProfile.SubKeyCount.ToString() + " profiles found";
                    break;
                // If it comes back as 1, you have 1 additional profile SubKey
                case "1":
                    lblProfiles.Text = regKeyProfile.SubKeyCount.ToString() + " profile found";
                    foreach (string Value in regName)
                    {
                        // So add that SubKey to the ComboBox
                        cboProfiles.Items.Add(Value);
                    }
                    break;
                default:
                    // If you don't have 0 or 1, you have multiple profiles (undetermined amount) so add them all in the ComboBox
                    lblProfiles.Text = regKeyProfile.SubKeyCount.ToString() + " profiles found";
                    foreach (string Value in regName)
                    {
                        cboProfiles.Items.Add(Value);
                    }
                    break;
            }

            //
            // I did it like this because it's more proper to say 0 profiles, 1 profile, and say 5 profiles. The number zero is usually described as plural (dunno why)
            // Otherwise I could of easily done this to achieve the same effect instead of doing a switch statement
            //
            // lblProfiles.Text = regKeyProfile.SubKeyCount.ToString() + " profile(s) found"
            // foreach (string Value in regName)
            // {
            //      cboProfiles.Items.Add(Value);
            // }
            //

            // Closes the registry until it's used again either by read or write
            regKeyProfile.Close();
            regKeyGlobal.Close();
        }

        private void Read()
        {
            RegistryKey regKeyGlobal = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3");
            RegistryKey regKeyProfile = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles");

            string NAME = cboProfiles.Text;

            if (cboProfiles.Text != "Global")
            {
                using (RegistryKey regKey = regKeyProfile.CreateSubKey(NAME))
                {
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
                }
            }
            else
            {
                // Get values of all the designated keys. Since the DWORD values come out as strings, they need to be converted
                cboAwayIdle.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Away Idle", 0, RegistryValueOptions.None));
                cboBleedTimestamps.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Bleed Timestamps", 0, RegistryValueOptions.None));
                cboChannelOrder.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Channel Order", 0, RegistryValueOptions.None));
                cboDescribeUserFlags.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Describe User Flags", 0, RegistryValueOptions.None));
                cboADBanner.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Display AD-Banner", 0, RegistryValueOptions.None));
                cboShowUndecoded.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Show Undecoded", 0, RegistryValueOptions.None));
                cboExtendedWhois.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("Extended Whois", 0, RegistryValueOptions.None));
                txtUDPPort.Text = Convert.ToString((Int32)regKeyGlobal.GetValue("UDP Port", 0, RegistryValueOptions.None));
                txtIgnorePluginMask.Text = (string)regKeyGlobal.GetValue("Ignore Plugin Mask", System.String.Empty, RegistryValueOptions.None);
                txtRealmCharacter.Text = (string)regKeyGlobal.GetValue("Realm Character", System.String.Empty, RegistryValueOptions.None);
                cboRealmName.Text = (string)regKeyGlobal.GetValue("Realm Name", "USEast", RegistryValueOptions.None);
                txtBindIP.Text = (string)regKeyGlobal.GetValue("Bind IP", System.String.Empty, RegistryValueOptions.None);
            }

            // Since the DWORD values come back as 1 or 0 due to how the program recognizes it, this basically translates 1 into Enabled or 0 into Disabled using a Ternary Operator
            cboBleedTimestamps.Text = cboBleedTimestamps.Text == "1" ? "Enabled" : "Disabled";
            cboAwayIdle.Text = cboAwayIdle.Text == "1" ? "Enabled" : "Disabled";
            cboChannelOrder.Text = cboChannelOrder.Text == "1" ? "Enabled" : "Disabled";
            cboShowUndecoded.Text = cboShowUndecoded.Text == "1" ? "Enabled" : "Disabled";
            cboDescribeUserFlags.Text = cboDescribeUserFlags.Text == "1" ? "Enabled" : "Disabled";
            cboADBanner.Text = cboADBanner.Text == "1" ? "Enabled" : "Disabled";
            cboExtendedWhois.Text = cboExtendedWhois.Text == "1" ? "Enabled" : "Disabled";

            regKeyGlobal.Close();
            regKeyProfile.Close();
        }

        private void Write()
        {
            bool Result;
            RegistryKey regKeyGlobal = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3");
            RegistryKey regKeyProfile = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles");

            string NAME = cboProfiles.Text;

            try
            {
                if (cboProfiles.Text == "Global")
                {
                    if (cboBleedTimestamps.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Bleed Timestamps", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboBleedTimestamps.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Bleed Timestamps", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    if (cboChannelOrder.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Channel Order", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboChannelOrder.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Channel Order", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    if (cboDescribeUserFlags.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Describe User Flags", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboDescribeUserFlags.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Describe User Flags", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    if (cboADBanner.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Display AD-Banner", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboADBanner.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Display AD-Banner", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    if (cboAwayIdle.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Away Idle", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboAwayIdle.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Away Idle", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    if (cboShowUndecoded.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Show Undecoded", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboShowUndecoded.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Show Undecoded", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    if (cboExtendedWhois.Text == "Enabled")
                    {
                        regKeyGlobal.SetValue("Extended Whois", 1, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else if (cboExtendedWhois.Text == "Disabled")
                    {
                        regKeyGlobal.SetValue("Extended Whois", 0, RegistryValueKind.DWord);
                        Result = true;
                    }
                    else Result = false;

                    regKeyGlobal.SetValue("UDP Port", txtUDPPort.Text, RegistryValueKind.DWord);
                    regKeyGlobal.SetValue("Bind IP", txtBindIP.Text, RegistryValueKind.String);
                    regKeyGlobal.SetValue("Realm Name", cboRealmName.Text, RegistryValueKind.String);
                    regKeyGlobal.SetValue("Realm Character", txtRealmCharacter.Text, RegistryValueKind.String);
                    regKeyGlobal.SetValue("Ignore Plugin Mask", txtIgnorePluginMask.Text, RegistryValueKind.String);

                    if (Result == true)
                        MessageBox.Show("Writing to registry was successful!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Writing to registry was unsuccessful!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (RegistryKey regKey = regKeyProfile.CreateSubKey(NAME))
                    {
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

                        if (Result == true)
                            MessageBox.Show("Writing to registry was successful for " + cboProfiles.Text + "!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Writing to registry was unsuccessful for " + cboProfiles.Text + "!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            regKeyGlobal.Close();
            regKeyProfile.Close();
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

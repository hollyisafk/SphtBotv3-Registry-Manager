using System;
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

                // Adds the global profile in the ComboBox for selection
                cboProfiles.Items.Add("Global");
                cboProfiles.Text = "Global";

                // Since the global profile ("SphtBotv3" SubKey) is 2 SubKeys up from the "Profiles" SubKey, it needs to be declared separately
                RegistryKey regKeyGlobal = Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree);
                RegistryKey regKeyProfile = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles", RegistryKeyPermissionCheck.ReadWriteSubTree);

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
                lblProfiles.Text = string.Format("{0} profile{1} found", regKeyProfile.SubKeyCount.ToString(), regKeyProfile.SubKeyCount != 1 ? "s" : "");
                foreach (string Value in regName)
                {
                    // So add that SubKey to the ComboBox
                    cboProfiles.Items.Add(Value);
                }

                // Closes the registry until it's used again either by read or write
                regKeyProfile.Close();
                regKeyGlobal.Close();
            }
        }

        private void Read()
        {
            // If the combo box text says "Global", then use the CurrentUser Registry, otherwise use the LocalMachine (where the profiles are held)
            bool global = cboProfiles.Text == "Global";
            RegistryKey regKey = global ? Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree) :
                                          Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\" + cboProfiles.Text, RegistryKeyPermissionCheck.ReadWriteSubTree);

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
            // DropDownList items can't be temporarily edited like DropDown style can. Thus read it straight from the Registry, then assign the value
            cboBleedTimestamps.Text = Convert.ToString((Int32)regKey.GetValue("Bleed Timestamps")) == "1" ? "Enabled" : "Disabled";
            cboAwayIdle.Text = Convert.ToString((Int32)regKey.GetValue("Away Idle")) == "1" ? "Enabled" : "Disabled";
            cboChannelOrder.Text = Convert.ToString((Int32)regKey.GetValue("Channel Order")) == "1" ? "Enabled" : "Disabled";
            cboShowUndecoded.Text = Convert.ToString((Int32)regKey.GetValue("Show Undecoded")) == "1" ? "Enabled" : "Disabled";
            cboDescribeUserFlags.Text = Convert.ToString((Int32)regKey.GetValue("Describe User Flags")) == "1" ? "Enabled" : "Disabled";
            cboADBanner.Text = Convert.ToString((Int32)regKey.GetValue("Display AD-Banner")) == "1" ? "Enabled" : "Disabled";
            cboExtendedWhois.Text = Convert.ToString((Int32)regKey.GetValue("Extended Whois")) == "1" ? "Enabled" : "Disabled";

            regKey.Close();
        }

        private void SetAbled(RegistryKey regKey, ComboBox cboB, string strKey)
        {
            regKey.SetValue(strKey, cboB.Text == "Enabled" ? 1 : 0, RegistryValueKind.DWord);
        }

        private void SetValue(RegistryKey regKey, string strKey, string Value, RegistryValueKind regKind)
        {
            regKey.SetValue(strKey, Value, regKind);
        }

        private void Write()
        {
            bool global = cboProfiles.Text == "Global";
            bool result = false;
            RegistryKey regKey = global ? Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree) :
                                          Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\" + cboProfiles.Text, RegistryKeyPermissionCheck.ReadWriteSubTree);

            try
            {
                SetAbled(regKey, cboBleedTimestamps, "Bleed Timestamps");
                SetAbled(regKey, cboChannelOrder, "Channel Order");
                SetAbled(regKey, cboDescribeUserFlags, "Describe User Flags");
                SetAbled(regKey, cboADBanner, "Display AD-Banner");
                SetAbled(regKey, cboAwayIdle, "Away Idle");
                SetAbled(regKey, cboShowUndecoded, "Show Undecoded");
                SetAbled(regKey, cboExtendedWhois, "Extended Whois");

                SetValue(regKey, "UDP Port", txtUDPPort.Text, RegistryValueKind.DWord);
                SetValue(regKey, "Bind IP", txtBindIP.Text, RegistryValueKind.String);
                SetValue(regKey, "Realm Name", cboRealmName.Text, RegistryValueKind.String);
                SetValue(regKey, "Realm Character", txtRealmCharacter.Text, RegistryValueKind.String);
                SetValue(regKey, "Ignore Plugin Mask", txtIgnorePluginMask.Text, RegistryValueKind.String);
                result = true;
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
            if (cboProfiles.Text == "Global")
                btnEdit.Enabled = false;
            else
                btnEdit.Enabled = true;
        }
    }
}

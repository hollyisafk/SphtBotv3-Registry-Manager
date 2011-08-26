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

                // Since the global profile ("SphtBotv3" SubKey) is 2 SubKeys up from the "Profiles" SubKey, it needs to be declared separately
                // Running the bot without ProfileLauncher reads from CurrentUser otherwise, PL reads the global profile from LocalMachine (this needs to be fixed)
                RegistryKey regKeyProfile = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\", RegistryKeyPermissionCheck.ReadWriteSubTree);

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

            byte[] Value = (byte[])regKey.GetValue("IRC AutoSend", new byte[] {0000});
            string Output = System.String.Empty;

            foreach (byte b in Value)
            {
                Output += (char)b;
            }

            try
            {
                // Get values of all the designated keys. Since the DWORD values come out as strings, they need to be converted
                // Since the DWORD values come back as 1 or 0 due to how the program recognizes it, this basically translates 1 into Enabled or 0 into Disabled using a Ternary Operator
                // DropDownList items can't be temporarily edited like DropDown style can. Thus read it straight from the Registry, then assign the value
                cboADBanner.Text = Convert.ToString((Int32)regKey.GetValue("Display AD-Banner", 0)) == "1" ? "Enabled" : "Disabled";
                cboAwayIdle.Text = Convert.ToString((Int32)regKey.GetValue("Away Idle", 1)) == "1" ? "Enabled" : "Disabled";
                cboBNLSAddress.Text = (string)regKey.GetValue("BNLS Address", "bnls.mattkv.net");
                cboBleedTimestamps.Text = Convert.ToString((Int32)regKey.GetValue("Bleed Timestamps", 0)) == "1" ? "Enabled" : "Disabled";
                cboCTCP.Text = Convert.ToString((Int32)regKey.GetValue("Disable CTCP", 0)) == "0" ? "Enabled" : "Disabled";
                cboChannelOrder.Text = Convert.ToString((Int32)regKey.GetValue("Channel Order", 1)) == "1" ? "Enabled" : "Disabled";
                cboColorNames.Text = Convert.ToString((Int32)regKey.GetValue("Colorful Names", 0)) == "1" ? "Enabled" : "Disabled";
                cboDescribeUserFlags.Text = Convert.ToString((Int32)regKey.GetValue("Describe User Flags", 1)) == "1" ? "Enabled" : "Disabled";
                cboExtendedWhois.Text = Convert.ToString((Int32)regKey.GetValue("Extended Whois", 1)) == "1" ? "Enabled" : "Disabled";
                cboIRCServer.Text = (string)regKey.GetValue("IRC Server", "none");
                cboNotify.Text = Convert.ToString((Int32)regKey.GetValue("Disable Windows Notify", 0)) == "0" ? "Enabled" : "Disabled";
                cboRealmName.Text = (string)regKey.GetValue("Realm Name", "USEast");
                cboServer.Text = (string)regKey.GetValue("Server", "useast.battle.net");
                cboShowUndecoded.Text = Convert.ToString((Int32)regKey.GetValue("Show Undecoded", 0)) == "1" ? "Enabled" : "Disabled";
                chkAutoRejoin.Checked = Convert.ToString((Int32)regKey.GetValue("Auto Rejoin", 1)) == "1" ? true : false;
                chkBNETAutoCon.Checked = Convert.ToString((Int32)regKey.GetValue("Connect to Battle.net", 1)) == "1" ? true : false;
                chkBanKick.Checked = Convert.ToString((Int32)regKey.GetValue("Notify 2", 1)) == "1" ? true : false;
                chkCDKey.Checked = Convert.ToString((Int32)regKey.GetValue("Spawn", 0)) == "1" ? true : false;
                chkJoinLeave.Checked = Convert.ToString((Int32)regKey.GetValue("Notify", 1)) == "1" ? true : false;
                chkUDP.Checked = Convert.ToString((Int32)regKey.GetValue("No UDP", 0)) == "1" ? true : false;
                txtBNETPassword.Text = (string)regKey.GetValue("Password", System.String.Empty);
                txtBNETUsername.Text = (string)regKey.GetValue("Username", System.String.Empty);
                txtBindIP.Text = (string)regKey.GetValue("Bind IP", System.String.Empty);
                txtBotNetPassword.Text = (string)regKey.GetValue("BotNet Account Password", System.String.Empty);
                txtBotNetServer.Text = (string)regKey.GetValue("BotNet Server", "none");
                txtBotNetUsername.Text = (string)regKey.GetValue("BotNet Account Name", System.String.Empty);
                txtCDKeyUser.Text = (string)regKey.GetValue("CD-Key User", System.String.Empty);
                txtEMail.Text = (string)regKey.GetValue("E-Mail", System.String.Empty);
                txtFinger.Text = (string)regKey.GetValue("Finger Reply", System.String.Empty);
                txtHome.Text = (string)regKey.GetValue("Home Channel", System.String.Empty);
                txtIRCAccount.Text = (string)regKey.GetValue("IRC Nickname", System.String.Empty);
                txtIRCChannels.Text = (string)regKey.GetValue("IRC Channels", System.String.Empty);
                txtIRCEmail.Text = (string)regKey.GetValue("IRC E-Mail", System.String.Empty);
                txtIRCIgnore.Text = (string)regKey.GetValue("IRC Ignore Mask", System.String.Empty);
                txtIRCName.Text = (string)regKey.GetValue("IRC Username", System.String.Empty);
                txtIRCPassword.Text = (string)regKey.GetValue("IRC Password", System.String.Empty);
                txtIgnorePluginMask.Text = (string)regKey.GetValue("Ignore Plugin Mask", System.String.Empty);
                txtMask.Text = (string)regKey.GetValue("BotNet Database Mask", System.String.Empty);
                txtNotify.Text = (string)regKey.GetValue("Notify Keyword", System.String.Empty);
                txtPerform.Text = Output;
                txtRealmCharacter.Text = (string)regKey.GetValue("Realm Character Name", System.String.Empty);
                txtUDPPort.Text = Convert.ToString((Int32)regKey.GetValue("UDP Port", 0));

                switch ((string)regKey.GetValue("BotNet Database", System.String.Empty))
                {
                    case "":
                        break;
                    default:
                        string BN = (string)regKey.GetValue("BotNet Database", System.String.Empty);
                        string[] BotNet = BN.Split(' ');

                        string Value1 = BotNet[0];
                        string Value2 = BotNet[1];

                        txtBotNetDatabase.Text = Value1;
                        txtDatabasePassword.Text = Value2;
                        break;
                }

                if (Convert.ToString((Int32)regKey.GetValue("Ignore Ping", 0)) == "1")
                    cboPing.Text = "Ignore pre-logon ping (-1ms ping)";
                else if (Convert.ToString((Int32)regKey.GetValue("Post-Reply Ping", 1)) == "1")
                    cboPing.Text = "Post-send pre-logon ping (0ms ping)";
                else if (Convert.ToString((Int32)regKey.GetValue("Ignore Ping", 0)) == "0" && Convert.ToString((Int32)regKey.GetValue("Post-Reply Ping", 1)) == "0")
                    cboPing.Text = "None";

                if ((string)regKey.GetValue("Platform", "IX86") == System.String.Empty || (string)regKey.GetValue("Platform", "IX86") == "IX86")
                    cboPlatform.Text = "Intel x86";
                else if ((string)regKey.GetValue("Platform", "IX86") == "PMAC")
                    cboPlatform.Text = "Power Macintosh";
                else if ((string)regKey.GetValue("Platform", "IX86") == "XMAC")
                    cboPlatform.Text = "Macintosh (Mac OS X)";

                if ((string)regKey.GetValue("Product Language", "enUS") == System.String.Empty || (string)regKey.GetValue("Product Language", "enUS") == "enUS")
                    cboLanguage.Text = "English (United States)";

                switch (Convert.ToString((Int32)regKey.GetValue("Product", 1)))
                {
                    case "1":
                        cboProduct.Text = "StarCraft";
                        txtCDKey.Text = (string)regKey.GetValue("StarCraft CD-Key", System.String.Empty);
                        break;
                    case "2":
                        cboProduct.Text = "StarCraft: Brood War";
                        txtCDKey.Text = (string)regKey.GetValue("Brood War CD-Key", System.String.Empty);
                        break;
                    case "3":
                        cboProduct.Text = "WarCraft II: Battle.net Edition";
                        txtCDKey.Text = (string)regKey.GetValue("WarCraft II CD-Key", System.String.Empty);
                        break;
                    case "4":
                        cboProduct.Text = "Diablo II";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo II CD-Key", System.String.Empty);
                        break;
                    case "5":
                        cboProduct.Text = "Diablo II: Lord of Destruction";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo II CD-Key", System.String.Empty);
                        txtXPCDKey.Text = (string)regKey.GetValue("Diablo II: LoD CD-Key", System.String.Empty);
                        break;
                    case "6":
                        cboProduct.Text = "StarCraft: Japan";
                        txtCDKey.Text = (string)regKey.GetValue("Japan StarCraft CD-Key", System.String.Empty);
                        break;
                    case "7":
                        cboProduct.Text = "WarCraft III: Reign of Chaos";
                        txtCDKey.Text = (string)regKey.GetValue("WarCraft III CD-Key", System.String.Empty);
                        break;
                    case "8":
                        cboProduct.Text = "WarCraft III: The Frozen Throne";
                        txtCDKey.Text = (string)regKey.GetValue("WarCraft III CD-Key", System.String.Empty);
                        txtXPCDKey.Text = (string)regKey.GetValue("The Frozen Throne CD-Key", System.String.Empty);
                        break;
                    case "9":
                        cboProduct.Text = "Diablo";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo CD-Key", System.String.Empty);
                        break;
                    case "10":
                        cboProduct.Text = "Diablo Shareware";
                        txtCDKey.Text = (string)regKey.GetValue("Diablo Shareware CD-Key", System.String.Empty);
                        break;
                    case "11":
                        cboProduct.Text = "StarCraft Shareware";
                        txtCDKey.Text = (string)regKey.GetValue("StarCraft Shareware CD-Key", System.String.Empty);
                        break;
                    default:
                        break;
                }

                switch (Convert.ToString((Int32)regKey.GetValue("Realm Character Class", 1)))
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
            if (cboB != cboNotify && cboB != cboCTCP)
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
            RegistryKey regKey = global ? Registry.CurrentUser.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3", RegistryKeyPermissionCheck.ReadWriteSubTree) :
                                          Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles\\" + cboProfiles.Text, RegistryKeyPermissionCheck.ReadWriteSubTree);

            bool result = true;
            byte[] Value = System.Text.Encoding.UTF8.GetBytes(txtPerform.Text);

            try
            {
                // SetAbled(regKey (Global or Profile), ComboBoxName, SubKeyName, Dword/String/QWord,etc)
                SetAbled(regKey, cboADBanner, "Display AD-Banner", RegistryValueKind.DWord);
                SetAbled(regKey, cboAwayIdle, "Away Idle", RegistryValueKind.DWord);
                SetAbled(regKey, cboBleedTimestamps, "Bleed Timestamps", RegistryValueKind.DWord);
                SetAbled(regKey, cboChannelOrder, "Channel Order", RegistryValueKind.DWord);
                SetAbled(regKey, cboColorNames, "Colorful Names", RegistryValueKind.DWord);
                SetAbled(regKey, cboDescribeUserFlags, "Describe User Flags", RegistryValueKind.DWord);
                SetAbled(regKey, cboExtendedWhois, "Extended Whois", RegistryValueKind.DWord);
                SetAbled(regKey, cboNotify, "Disable Windows Notify", RegistryValueKind.DWord);
                SetAbled(regKey, cboCTCP, "Disable CTCP", RegistryValueKind.DWord);
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
                SetValue(regKey, "Finger Reply", txtFinger.Text, RegistryValueKind.String);
                SetValue(regKey, "Home Channel", txtHome.Text, RegistryValueKind.String);
                SetValue(regKey, "Ignore Plugin Mask", txtIgnorePluginMask.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Channels", txtIRCChannels.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC E-Mail", txtIRCEmail.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Ignore Mask", txtIRCIgnore.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Nickname", txtIRCAccount.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Password", txtIRCPassword.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Server", cboIRCServer.Text, RegistryValueKind.String);
                SetValue(regKey, "IRC Username", txtIRCName.Text, RegistryValueKind.String);
                SetValue(regKey, "Notify Keyword", txtNotify.Text, RegistryValueKind.String);
                SetValue(regKey, "Password", txtBNETPassword.Text, RegistryValueKind.String);
                SetValue(regKey, "Realm Character Name", txtRealmCharacter.Text, RegistryValueKind.String);
                SetValue(regKey, "Realm Name", cboRealmName.Text, RegistryValueKind.String);
                SetValue(regKey, "Server", cboServer.Text, RegistryValueKind.String);
                SetValue(regKey, "Username", txtBNETUsername.Text, RegistryValueKind.String);

                regKey.SetValue("IRC AutoSend", Value, RegistryValueKind.Binary);
                regKey.SetValue("UDP Port", txtUDPPort.Text, RegistryValueKind.DWord);

                if (cboPing.Text == "Ignore pre-logon ping (-1ms ping)")
                {
                    regKey.SetValue("Ignore Ping", 1, RegistryValueKind.DWord);
                    regKey.SetValue("Post-Reply Ping", 0, RegistryValueKind.DWord);
                }
                else if (cboPing.Text == "Post-send pre-logon ping (0ms ping)")
                {
                    regKey.SetValue("Post-Reply Ping", 1, RegistryValueKind.DWord);
                    regKey.SetValue("Ignore Ping", 0, RegistryValueKind.DWord);
                }
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

                if (cboLanguage.Text == "English (United States)")
                    regKey.SetValue("Product Language", "enUS", RegistryValueKind.String);

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
                    case "WarCraft II: Battle.net Edition":
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

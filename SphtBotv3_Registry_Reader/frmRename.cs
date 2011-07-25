using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Security.Principal;
using System.Reflection;
using Microsoft.Win32;

namespace SphtBotv3_Registry_Manager
{
    public partial class frmRename : Form
    {
        private string SelectedItem;

        // Carried over from frmEdit
        public frmRename(string s)
        {
            InitializeComponent();
            SelectedItem = s;
        }

        // Process is to call RenameSubKey function which in turn calls CopyKey function which calls RecurseCopyKey function
        public bool RenameSubKey(string SubKey, string newSubKey)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles", RegistryKeyPermissionCheck.ReadWriteSubTree);
            CopyKey(SubKey, newSubKey);
            regKey.DeleteSubKeyTree(SubKey);
            return true;
        }

        public bool CopyKey(string Key, string newKey)
        {
            // Create new key
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles", RegistryKeyPermissionCheck.ReadWriteSubTree);
            RegistryKey destinationKey = regKey.CreateSubKey(newKey);

            // Open the srcKey we are copying from
            RegistryKey srcKey = regKey.OpenSubKey(Key);

            RecurseCopyKey(srcKey, destinationKey);

            return true;
        }

        private void RecurseCopyKey(RegistryKey srcKey, RegistryKey destinationKey)
        {
            // Copy all the values
            foreach (string Value in srcKey.GetValueNames())
            {
                object objValue = srcKey.GetValue(Value);
                RegistryValueKind valKind = srcKey.GetValueKind(Value);
                destinationKey.SetValue(Value, objValue, valKind);
            }

            // For Each subKey, create a new SubKey in destinationKey and call
            foreach (string srcSubKeyName in srcKey.GetSubKeyNames())
            {
                RegistryKey sourceSubKey = srcKey.OpenSubKey(srcSubKeyName);
                RegistryKey destSubKey = destinationKey.CreateSubKey(srcSubKeyName);
                RecurseCopyKey(sourceSubKey, destSubKey);
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            try
            {
                RenameSubKey(SelectedItem, txtRename.Text);
                MessageBox.Show("Profile successfully renamed!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

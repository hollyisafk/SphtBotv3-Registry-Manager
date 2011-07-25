using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Security.Principal;
using System.Reflection;
using Microsoft.Win32;

namespace SphtBotv3_Registry_Manager
{
    public partial class frmEdit : Form
    {
        private string SelectedItem;

        // Carried over from frmMain
        public frmEdit(string s)
        {
            InitializeComponent();
            SelectedItem = s;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey("Software\\Valhalla's Legends\\Spht\\SphtBotv3\\Profiles", RegistryKeyPermissionCheck.ReadWriteSubTree);

            string Message = "Are you sure?";
            string Text = "SphtBotv3 Registry Manager";
            var Result = MessageBox.Show(Message, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                this.Close();
            else
            {
                try
                {
                    regKey.DeleteSubKey(SelectedItem, true);
                    MessageBox.Show("Profile successfully deleted!", "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SphtBotv3 Registry Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            Form frm = new frmRename(SelectedItem);
            frm.Text = string.Format("Renaming profile: {0}", SelectedItem);
            frm.Show();
        }
    }
}

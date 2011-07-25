namespace SphtBotv3_Registry_Manager
{
    partial class frmRename
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRename));
            this.txtRename = new System.Windows.Forms.TextBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(12, 12);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(229, 20);
            this.txtRename.TabIndex = 0;
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(12, 38);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(94, 44);
            this.btnRename.TabIndex = 1;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(147, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 44);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // frmRename
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 93);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.txtRename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRename";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Renaming profile: TESTESTTEST";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnCancel;
    }
}
namespace SphtBotv3_Registry_Manager
{
    partial class frmChangeLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeLog));
            this.rtbChangeLog = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbChangeLog
            // 
            this.rtbChangeLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChangeLog.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbChangeLog.Location = new System.Drawing.Point(12, 12);
            this.rtbChangeLog.Name = "rtbChangeLog";
            this.rtbChangeLog.ReadOnly = true;
            this.rtbChangeLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbChangeLog.Size = new System.Drawing.Size(460, 312);
            this.rtbChangeLog.TabIndex = 0;
            this.rtbChangeLog.Text = resources.GetString("rtbChangeLog.Text");
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(192, 330);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChangeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 382);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rtbChangeLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangeLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeLog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChangeLog;
        private System.Windows.Forms.Button btnClose;
    }
}
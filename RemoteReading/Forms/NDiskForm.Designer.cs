
using JustLib.NetworkDisk.Passive;
namespace RemoteReading
{
    partial class NDiskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NDiskForm));
            this.nDiskBrowser1 = new NDiskBrowser();
            this.SuspendLayout();
            // 
            // nDiskBrowser1
            // 
            this.nDiskBrowser1.AllowDrop = true;
            this.nDiskBrowser1.AllowUploadFolder = false;
            this.nDiskBrowser1.BackColor = System.Drawing.Color.Transparent;
            this.nDiskBrowser1.Connected = true;
            this.nDiskBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nDiskBrowser1.Location = new System.Drawing.Point(4, 28);
            this.nDiskBrowser1.Name = "nDiskBrowser1";
            this.nDiskBrowser1.Size = new System.Drawing.Size(757, 542);
            this.nDiskBrowser1.TabIndex = 0;
            // 
            // NDiskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(765, 574);
            this.CloseDownBack = global::RemoteReading.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::RemoteReading.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::RemoteReading.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.nDiskBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::RemoteReading.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::RemoteReading.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::RemoteReading.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::RemoteReading.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::RemoteReading.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::RemoteReading.Properties.Resources.btn_mini_normal;
            this.Name = "NDiskForm";
            this.RestoreDownBack = global::RemoteReading.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::RemoteReading.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::RemoteReading.Properties.Resources.btn_restore_normal;
            this.Text = "我的网盘";
            this.UseCustomIcon = true;
            this.ResumeLayout(false);

        }

        #endregion

        private NDiskBrowser nDiskBrowser1;
    }
}
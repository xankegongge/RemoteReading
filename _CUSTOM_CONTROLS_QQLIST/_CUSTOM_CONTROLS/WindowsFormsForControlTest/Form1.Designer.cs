namespace WindowsFormsForControlTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.chatListBox1 = new _CUSTOM_CONTROLS.ChatListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(246, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(246, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(246, 99);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 21);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chatListBox1
            // 
            this.chatListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatListBox1.BackColor = System.Drawing.Color.White;
            this.chatListBox1.ForeColor = System.Drawing.Color.Black;
            this.chatListBox1.ItemMouseOnColor = System.Drawing.Color.Black;
            this.chatListBox1.Location = new System.Drawing.Point(12, 11);
            this.chatListBox1.Name = "chatListBox1";
            this.chatListBox1.Size = new System.Drawing.Size(228, 435);
            this.chatListBox1.TabIndex = 2;
            this.chatListBox1.Text = "chatListBox1";
            this.chatListBox1.DoubleClickSubItem += new _CUSTOM_CONTROLS.ChatListBox.ChatListEventHandler(this.chatListBox1_DoubleClickSubItem);
            this.chatListBox1.MouseEnterHead += new _CUSTOM_CONTROLS.ChatListBox.ChatListEventHandler(this.chatListBox1_MouseEnterHead);
            this.chatListBox1.MouseLeaveHead += new _CUSTOM_CONTROLS.ChatListBox.ChatListEventHandler(this.chatListBox1_MouseLeaveHead);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 457);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chatListBox1);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.Red;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private _CUSTOM_CONTROLS.ChatListBox chatListBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

    }
}


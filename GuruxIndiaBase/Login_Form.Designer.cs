namespace Gurux_Testing
{
    partial class Login_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Form));
            this.cb_userList = new System.Windows.Forms.ComboBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.b_login = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_userList
            // 
            this.cb_userList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_userList.FormattingEnabled = true;
            this.cb_userList.Location = new System.Drawing.Point(131, 65);
            this.cb_userList.Name = "cb_userList";
            this.cb_userList.Size = new System.Drawing.Size(170, 28);
            this.cb_userList.TabIndex = 0;
            // 
            // tb_password
            // 
            this.tb_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_password.Location = new System.Drawing.Point(131, 104);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(170, 26);
            this.tb_password.TabIndex = 1;
            this.tb_password.Text = "admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password :";
            // 
            // b_login
            // 
            this.b_login.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.b_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_login.ForeColor = System.Drawing.Color.White;
            this.b_login.Location = new System.Drawing.Point(131, 145);
            this.b_login.Name = "b_login";
            this.b_login.Size = new System.Drawing.Size(72, 33);
            this.b_login.TabIndex = 26;
            this.b_login.Text = "Submit";
            this.b_login.UseVisualStyleBackColor = false;
            this.b_login.Click += new System.EventHandler(this.b_login_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.OliveDrab;
            this.label3.Location = new System.Drawing.Point(103, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 27;
            this.label3.Text = "DLMS Tool";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cb_userList);
            this.panel1.Controls.Add(this.b_login);
            this.panel1.Controls.Add(this.tb_password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 194);
            this.panel1.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(229, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 33);
            this.button1.TabIndex = 28;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Login_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(360, 214);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login_Form";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOGIN";
            this.Load += new System.EventHandler(this.Login_Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_userList;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button b_login;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}
namespace ImportAgahPriceHistory
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
            this.btnRequestCaptcha = new System.Windows.Forms.Button();
            this.pbCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptcha = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudImportDays = new System.Windows.Forms.NumericUpDown();
            this.btnImportRahavard365 = new System.Windows.Forms.Button();
            this.btnImportTse = new System.Windows.Forms.Button();
            this.btnImportTseStatus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImportDays)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRequestCaptcha
            // 
            this.btnRequestCaptcha.Location = new System.Drawing.Point(12, 12);
            this.btnRequestCaptcha.Name = "btnRequestCaptcha";
            this.btnRequestCaptcha.Size = new System.Drawing.Size(150, 23);
            this.btnRequestCaptcha.TabIndex = 0;
            this.btnRequestCaptcha.Text = "RequestCaptcha";
            this.btnRequestCaptcha.UseVisualStyleBackColor = true;
            this.btnRequestCaptcha.Click += new System.EventHandler(this.btnRequestCaptcha_Click);
            // 
            // pbCaptcha
            // 
            this.pbCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCaptcha.Location = new System.Drawing.Point(168, 12);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(134, 34);
            this.pbCaptcha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbCaptcha.TabIndex = 1;
            this.pbCaptcha.TabStop = false;
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Location = new System.Drawing.Point(365, 14);
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(102, 20);
            this.txtCaptcha.TabIndex = 1;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(315, 75);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(152, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "Import Agah";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Captcha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(473, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(534, 15);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(113, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(653, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(712, 14);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(119, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(837, 12);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(97, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.Color.Purple;
            this.lblLogin.Location = new System.Drawing.Point(940, 17);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(82, 16);
            this.lblLogin.TabIndex = 11;
            this.lblLogin.Text = "Logged Out";
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 132);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(1282, 305);
            this.txtConsole.TabIndex = 12;
            this.txtConsole.TextChanged += new System.EventHandler(this.txtConsole_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Past days to import";
            // 
            // nudImportDays
            // 
            this.nudImportDays.Location = new System.Drawing.Point(114, 78);
            this.nudImportDays.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nudImportDays.Name = "nudImportDays";
            this.nudImportDays.Size = new System.Drawing.Size(120, 20);
            this.nudImportDays.TabIndex = 5;
            this.nudImportDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudImportDays.Value = new decimal(new int[] {
            365,
            0,
            0,
            0});
            // 
            // btnImportRahavard365
            // 
            this.btnImportRahavard365.Location = new System.Drawing.Point(534, 75);
            this.btnImportRahavard365.Name = "btnImportRahavard365";
            this.btnImportRahavard365.Size = new System.Drawing.Size(172, 23);
            this.btnImportRahavard365.TabIndex = 14;
            this.btnImportRahavard365.Text = "Import Rahavard365";
            this.btnImportRahavard365.UseVisualStyleBackColor = true;
            this.btnImportRahavard365.Click += new System.EventHandler(this.btnImportRahavard365_Click);
            // 
            // btnImportTse
            // 
            this.btnImportTse.Location = new System.Drawing.Point(837, 75);
            this.btnImportTse.Name = "btnImportTse";
            this.btnImportTse.Size = new System.Drawing.Size(185, 23);
            this.btnImportTse.TabIndex = 15;
            this.btnImportTse.Text = "Import TSE Legal/Natural";
            this.btnImportTse.UseVisualStyleBackColor = true;
            this.btnImportTse.Click += new System.EventHandler(this.btnImportTse_Click);
            // 
            // btnImportTseStatus
            // 
            this.btnImportTseStatus.Location = new System.Drawing.Point(1062, 75);
            this.btnImportTseStatus.Name = "btnImportTseStatus";
            this.btnImportTseStatus.Size = new System.Drawing.Size(156, 23);
            this.btnImportTseStatus.TabIndex = 16;
            this.btnImportTseStatus.Text = "Import TSE Status";
            this.btnImportTseStatus.UseVisualStyleBackColor = true;
            this.btnImportTseStatus.Click += new System.EventHandler(this.btnImportTseStatus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 449);
            this.Controls.Add(this.btnImportTseStatus);
            this.Controls.Add(this.btnImportTse);
            this.Controls.Add(this.btnImportRahavard365);
            this.Controls.Add(this.nudImportDays);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtCaptcha);
            this.Controls.Add(this.pbCaptcha);
            this.Controls.Add(this.btnRequestCaptcha);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImportDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRequestCaptcha;
        private System.Windows.Forms.PictureBox pbCaptcha;
        private System.Windows.Forms.TextBox txtCaptcha;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudImportDays;
        private System.Windows.Forms.Button btnImportRahavard365;
        private System.Windows.Forms.Button btnImportTse;
        private System.Windows.Forms.Button btnImportTseStatus;
    }
}


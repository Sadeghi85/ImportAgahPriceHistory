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
            this.btnImportRahavard365 = new System.Windows.Forms.Button();
            this.btnImportTse = new System.Windows.Forms.Button();
            this.btnImportTseStatus = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblLoginRahavard = new System.Windows.Forms.Label();
            this.btnLoginRahavard = new System.Windows.Forms.Button();
            this.txtPasswordRahavard = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUsernameRahavard = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCaptchaRahavard = new System.Windows.Forms.TextBox();
            this.pbCaptchaRahavard = new System.Windows.Forms.PictureBox();
            this.btnRequestCaptchaRahavard = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptchaRahavard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRequestCaptcha
            // 
            this.btnRequestCaptcha.Location = new System.Drawing.Point(12, 25);
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
            this.pbCaptcha.Location = new System.Drawing.Point(168, 25);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(134, 34);
            this.pbCaptcha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbCaptcha.TabIndex = 1;
            this.pbCaptcha.TabStop = false;
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Location = new System.Drawing.Point(365, 27);
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(102, 20);
            this.txtCaptcha.TabIndex = 1;
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(315, 153);
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
            this.label1.Location = new System.Drawing.Point(312, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Captcha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(473, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(534, 28);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(113, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(653, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(712, 27);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(119, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(837, 25);
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
            this.lblLogin.Location = new System.Drawing.Point(940, 30);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(82, 16);
            this.lblLogin.TabIndex = 11;
            this.lblLogin.Text = "Logged Out";
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 196);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(1282, 284);
            this.txtConsole.TabIndex = 12;
            this.txtConsole.TextChanged += new System.EventHandler(this.txtConsole_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Start Date";
            // 
            // btnImportRahavard365
            // 
            this.btnImportRahavard365.Location = new System.Drawing.Point(534, 153);
            this.btnImportRahavard365.Name = "btnImportRahavard365";
            this.btnImportRahavard365.Size = new System.Drawing.Size(172, 23);
            this.btnImportRahavard365.TabIndex = 14;
            this.btnImportRahavard365.Text = "Import Rahavard365";
            this.btnImportRahavard365.UseVisualStyleBackColor = true;
            this.btnImportRahavard365.Click += new System.EventHandler(this.btnImportRahavard365_Click);
            // 
            // btnImportTse
            // 
            this.btnImportTse.Location = new System.Drawing.Point(837, 153);
            this.btnImportTse.Name = "btnImportTse";
            this.btnImportTse.Size = new System.Drawing.Size(185, 23);
            this.btnImportTse.TabIndex = 15;
            this.btnImportTse.Text = "Import TSE Legal/Natural";
            this.btnImportTse.UseVisualStyleBackColor = true;
            this.btnImportTse.Click += new System.EventHandler(this.btnImportTse_Click);
            // 
            // btnImportTseStatus
            // 
            this.btnImportTseStatus.Location = new System.Drawing.Point(1062, 153);
            this.btnImportTseStatus.Name = "btnImportTseStatus";
            this.btnImportTseStatus.Size = new System.Drawing.Size(156, 23);
            this.btnImportTseStatus.TabIndex = 16;
            this.btnImportTseStatus.Text = "Import TSE Info";
            this.btnImportTseStatus.UseVisualStyleBackColor = true;
            this.btnImportTseStatus.Click += new System.EventHandler(this.btnImportTseStatus_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Agah:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Rahavard365:";
            // 
            // lblLoginRahavard
            // 
            this.lblLoginRahavard.AutoSize = true;
            this.lblLoginRahavard.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginRahavard.ForeColor = System.Drawing.Color.Purple;
            this.lblLoginRahavard.Location = new System.Drawing.Point(1001, 94);
            this.lblLoginRahavard.Name = "lblLoginRahavard";
            this.lblLoginRahavard.Size = new System.Drawing.Size(82, 16);
            this.lblLoginRahavard.TabIndex = 27;
            this.lblLoginRahavard.Text = "Logged Out";
            // 
            // btnLoginRahavard
            // 
            this.btnLoginRahavard.Location = new System.Drawing.Point(898, 89);
            this.btnLoginRahavard.Name = "btnLoginRahavard";
            this.btnLoginRahavard.Size = new System.Drawing.Size(97, 23);
            this.btnLoginRahavard.TabIndex = 23;
            this.btnLoginRahavard.Text = "Login";
            this.btnLoginRahavard.UseVisualStyleBackColor = true;
            this.btnLoginRahavard.Click += new System.EventHandler(this.btnLoginRahavard_Click);
            // 
            // txtPasswordRahavard
            // 
            this.txtPasswordRahavard.Location = new System.Drawing.Point(773, 91);
            this.txtPasswordRahavard.Name = "txtPasswordRahavard";
            this.txtPasswordRahavard.PasswordChar = '*';
            this.txtPasswordRahavard.Size = new System.Drawing.Size(119, 20);
            this.txtPasswordRahavard.TabIndex = 22;
            this.txtPasswordRahavard.Text = "msm1985194";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(714, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Password";
            // 
            // txtUsernameRahavard
            // 
            this.txtUsernameRahavard.Location = new System.Drawing.Point(595, 92);
            this.txtUsernameRahavard.Name = "txtUsernameRahavard";
            this.txtUsernameRahavard.Size = new System.Drawing.Size(113, 20);
            this.txtUsernameRahavard.TabIndex = 21;
            this.txtUsernameRahavard.Text = "sadeghi85@hotmail.com";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(534, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Username";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(373, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Captcha";
            // 
            // txtCaptchaRahavard
            // 
            this.txtCaptchaRahavard.Location = new System.Drawing.Point(426, 91);
            this.txtCaptchaRahavard.Name = "txtCaptchaRahavard";
            this.txtCaptchaRahavard.Size = new System.Drawing.Size(102, 20);
            this.txtCaptchaRahavard.TabIndex = 19;
            // 
            // pbCaptchaRahavard
            // 
            this.pbCaptchaRahavard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCaptchaRahavard.Location = new System.Drawing.Point(168, 89);
            this.pbCaptchaRahavard.Name = "pbCaptchaRahavard";
            this.pbCaptchaRahavard.Size = new System.Drawing.Size(200, 40);
            this.pbCaptchaRahavard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbCaptchaRahavard.TabIndex = 20;
            this.pbCaptchaRahavard.TabStop = false;
            // 
            // btnRequestCaptchaRahavard
            // 
            this.btnRequestCaptchaRahavard.Location = new System.Drawing.Point(12, 89);
            this.btnRequestCaptchaRahavard.Name = "btnRequestCaptchaRahavard";
            this.btnRequestCaptchaRahavard.Size = new System.Drawing.Size(150, 23);
            this.btnRequestCaptchaRahavard.TabIndex = 18;
            this.btnRequestCaptchaRahavard.Text = "RequestCaptcha";
            this.btnRequestCaptchaRahavard.UseVisualStyleBackColor = true;
            this.btnRequestCaptchaRahavard.Click += new System.EventHandler(this.btnRequestCaptchaRahavard_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(73, 156);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 29;
            this.dtpStartDate.Value = new System.DateTime(2016, 3, 20, 0, 0, 0, 0);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 492);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblLoginRahavard);
            this.Controls.Add(this.btnLoginRahavard);
            this.Controls.Add(this.txtPasswordRahavard);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtUsernameRahavard);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCaptchaRahavard);
            this.Controls.Add(this.pbCaptchaRahavard);
            this.Controls.Add(this.btnRequestCaptchaRahavard);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnImportTseStatus);
            this.Controls.Add(this.btnImportTse);
            this.Controls.Add(this.btnImportRahavard365);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptchaRahavard)).EndInit();
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
        private System.Windows.Forms.Button btnImportRahavard365;
        private System.Windows.Forms.Button btnImportTse;
        private System.Windows.Forms.Button btnImportTseStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLoginRahavard;
        private System.Windows.Forms.Button btnLoginRahavard;
        private System.Windows.Forms.TextBox txtPasswordRahavard;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUsernameRahavard;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCaptchaRahavard;
        private System.Windows.Forms.PictureBox pbCaptchaRahavard;
        private System.Windows.Forms.Button btnRequestCaptchaRahavard;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
    }
}


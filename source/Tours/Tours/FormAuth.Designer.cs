
namespace Tours
{
    partial class FormAuth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAuth));
            this.ArichTextBoxLogin = new System.Windows.Forms.RichTextBox();
            this.ArichTextBoxPass = new System.Windows.Forms.RichTextBox();
            this.AlinkLabelGuest = new System.Windows.Forms.LinkLabel();
            this.AlabelLogin = new System.Windows.Forms.Label();
            this.AlabelPass = new System.Windows.Forms.Label();
            this.AbuttonAuth = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ArichTextBoxLogin
            // 
            this.ArichTextBoxLogin.Location = new System.Drawing.Point(211, 88);
            this.ArichTextBoxLogin.Name = "ArichTextBoxLogin";
            this.ArichTextBoxLogin.Size = new System.Drawing.Size(191, 29);
            this.ArichTextBoxLogin.TabIndex = 0;
            this.ArichTextBoxLogin.Text = "";
            this.ArichTextBoxLogin.TextChanged += new System.EventHandler(this.ArichTextBoxLogin_TextChanged);
            // 
            // ArichTextBoxPass
            // 
            this.ArichTextBoxPass.Location = new System.Drawing.Point(211, 168);
            this.ArichTextBoxPass.Name = "ArichTextBoxPass";
            this.ArichTextBoxPass.Size = new System.Drawing.Size(191, 29);
            this.ArichTextBoxPass.TabIndex = 1;
            this.ArichTextBoxPass.Text = "";
            this.ArichTextBoxPass.TextChanged += new System.EventHandler(this.ArichTextBoxPass_TextChanged);
            // 
            // AlinkLabelGuest
            // 
            this.AlinkLabelGuest.AutoSize = true;
            this.AlinkLabelGuest.LinkColor = System.Drawing.Color.Teal;
            this.AlinkLabelGuest.Location = new System.Drawing.Point(197, 319);
            this.AlinkLabelGuest.Name = "AlinkLabelGuest";
            this.AlinkLabelGuest.Size = new System.Drawing.Size(220, 20);
            this.AlinkLabelGuest.TabIndex = 2;
            this.AlinkLabelGuest.TabStop = true;
            this.AlinkLabelGuest.Text = "Продолжить без авторизации";
            this.AlinkLabelGuest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AlinkLabelGuest_LinkClicked);
            // 
            // AlabelLogin
            // 
            this.AlabelLogin.AutoSize = true;
            this.AlabelLogin.BackColor = System.Drawing.SystemColors.Window;
            this.AlabelLogin.Location = new System.Drawing.Point(254, 65);
            this.AlabelLogin.Name = "AlabelLogin";
            this.AlabelLogin.Size = new System.Drawing.Size(110, 20);
            this.AlabelLogin.TabIndex = 3;
            this.AlabelLogin.Text = "Введите логин";
            // 
            // AlabelPass
            // 
            this.AlabelPass.AutoSize = true;
            this.AlabelPass.BackColor = System.Drawing.SystemColors.Window;
            this.AlabelPass.Location = new System.Drawing.Point(244, 145);
            this.AlabelPass.Name = "AlabelPass";
            this.AlabelPass.Size = new System.Drawing.Size(120, 20);
            this.AlabelPass.TabIndex = 4;
            this.AlabelPass.Text = "Введите пароль";
            // 
            // AbuttonAuth
            // 
            this.AbuttonAuth.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.AbuttonAuth.Location = new System.Drawing.Point(197, 279);
            this.AbuttonAuth.Name = "AbuttonAuth";
            this.AbuttonAuth.Size = new System.Drawing.Size(220, 37);
            this.AbuttonAuth.TabIndex = 5;
            this.AbuttonAuth.Text = "Войти";
            this.AbuttonAuth.UseVisualStyleBackColor = false;
            this.AbuttonAuth.Click += new System.EventHandler(this.AbuttonAuth_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 401);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // FormAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 392);
            this.Controls.Add(this.AbuttonAuth);
            this.Controls.Add(this.AlabelPass);
            this.Controls.Add(this.AlabelLogin);
            this.Controls.Add(this.AlinkLabelGuest);
            this.Controls.Add(this.ArichTextBoxPass);
            this.Controls.Add(this.ArichTextBoxLogin);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormAuth";
            this.Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ArichTextBoxLogin;
        private System.Windows.Forms.RichTextBox ArichTextBoxPass;
        private System.Windows.Forms.LinkLabel AlinkLabelGuest;
        private System.Windows.Forms.Label AlabelLogin;
        private System.Windows.Forms.Label AlabelPass;
        private System.Windows.Forms.Button AbuttonAuth;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
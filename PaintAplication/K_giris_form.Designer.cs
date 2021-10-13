
namespace PaintAplication
{
    partial class K_giris_form
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
            this.button1 = new System.Windows.Forms.Button();
            this.UserTxt = new System.Windows.Forms.TextBox();
            this.sifreTxt = new System.Windows.Forms.TextBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.linkLabelSifremiUnuttum = new System.Windows.Forms.LinkLabel();
            this.linkLabelKAyitOl = new System.Windows.Forms.LinkLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::PaintAplication.Properties.Resources.kapat;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(310, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 34);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserTxt
            // 
            this.UserTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.UserTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UserTxt.ForeColor = System.Drawing.Color.Silver;
            this.UserTxt.Location = new System.Drawing.Point(29, 333);
            this.UserTxt.MaxLength = 50;
            this.UserTxt.Name = "UserTxt";
            this.UserTxt.Size = new System.Drawing.Size(177, 24);
            this.UserTxt.TabIndex = 1;
            this.UserTxt.Text = "Kullanıcı Adı";
            this.UserTxt.Click += new System.EventHandler(this.kAdiTxt_Click);
            // 
            // sifreTxt
            // 
            this.sifreTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.sifreTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sifreTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sifreTxt.ForeColor = System.Drawing.Color.Silver;
            this.sifreTxt.Location = new System.Drawing.Point(29, 385);
            this.sifreTxt.MaxLength = 70;
            this.sifreTxt.Name = "sifreTxt";
            this.sifreTxt.Size = new System.Drawing.Size(177, 24);
            this.sifreTxt.TabIndex = 2;
            this.sifreTxt.Text = "Parola";
            this.sifreTxt.UseSystemPasswordChar = true;
            this.sifreTxt.Click += new System.EventHandler(this.sifreTxt_Click);
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(180)))), ((int)(((byte)(50)))));
            this.loginBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loginBtn.FlatAppearance.BorderSize = 0;
            this.loginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loginBtn.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.loginBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.loginBtn.Location = new System.Drawing.Point(226, 526);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(118, 44);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "Giriş";
            this.loginBtn.UseVisualStyleBackColor = false;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // linkLabelSifremiUnuttum
            // 
            this.linkLabelSifremiUnuttum.ActiveLinkColor = System.Drawing.Color.Silver;
            this.linkLabelSifremiUnuttum.AutoSize = true;
            this.linkLabelSifremiUnuttum.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelSifremiUnuttum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.linkLabelSifremiUnuttum.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(180)))), ((int)(((byte)(50)))));
            this.linkLabelSifremiUnuttum.Location = new System.Drawing.Point(26, 458);
            this.linkLabelSifremiUnuttum.Name = "linkLabelSifremiUnuttum";
            this.linkLabelSifremiUnuttum.Size = new System.Drawing.Size(135, 16);
            this.linkLabelSifremiUnuttum.TabIndex = 4;
            this.linkLabelSifremiUnuttum.TabStop = true;
            this.linkLabelSifremiUnuttum.Text = "Şifrenimi Unuttun ?";
            this.linkLabelSifremiUnuttum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSifremiUnuttum_LinkClicked);
            // 
            // linkLabelKAyitOl
            // 
            this.linkLabelKAyitOl.ActiveLinkColor = System.Drawing.Color.Silver;
            this.linkLabelKAyitOl.AutoSize = true;
            this.linkLabelKAyitOl.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelKAyitOl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.linkLabelKAyitOl.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(180)))), ((int)(((byte)(50)))));
            this.linkLabelKAyitOl.Location = new System.Drawing.Point(26, 542);
            this.linkLabelKAyitOl.Name = "linkLabelKAyitOl";
            this.linkLabelKAyitOl.Size = new System.Drawing.Size(61, 16);
            this.linkLabelKAyitOl.TabIndex = 5;
            this.linkLabelKAyitOl.TabStop = true;
            this.linkLabelKAyitOl.Text = "Kayıt Ol";
            this.linkLabelKAyitOl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelKAyitOl_LinkClicked);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(212, 387);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 22);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Göster";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // K_giris_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = global::PaintAplication.Properties.Resources.login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(358, 585);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.linkLabelKAyitOl);
            this.Controls.Add(this.linkLabelSifremiUnuttum);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.sifreTxt);
            this.Controls.Add(this.UserTxt);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "K_giris_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K_giris_form";
            this.Load += new System.EventHandler(this.K_giris_form_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.K_giris_form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.K_giris_form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.K_giris_form_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox UserTxt;
        private System.Windows.Forms.TextBox sifreTxt;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.LinkLabel linkLabelSifremiUnuttum;
        private System.Windows.Forms.LinkLabel linkLabelKAyitOl;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

namespace PaintAplication
{
    partial class SifremiUnuttum
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
            this.comboBoxSoru = new System.Windows.Forms.ComboBox();
            this.kapat = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.textBoxCevap = new System.Windows.Forms.TextBox();
            this.textBoxSifretkr = new System.Windows.Forms.TextBox();
            this.textBoxKadi = new System.Windows.Forms.TextBox();
            this.textBoxMail = new System.Windows.Forms.TextBox();
            this.textBoxSifre = new System.Windows.Forms.TextBox();
            this.labelCevap = new System.Windows.Forms.Label();
            this.labelSoru = new System.Windows.Forms.Label();
            this.labelSifreTekrar = new System.Windows.Forms.Label();
            this.labelMail = new System.Windows.Forms.Label();
            this.labelSifre = new System.Windows.Forms.Label();
            this.labelKullanıcıAdı = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxSoru
            // 
            this.comboBoxSoru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSoru.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBoxSoru.FormattingEnabled = true;
            this.comboBoxSoru.Items.AddRange(new object[] {
            "En Sevdiğiniz Renk ?",
            "En Sevdiğiniz Hayvan ?",
            "İlk Evcil Hayvanınız ?"});
            this.comboBoxSoru.Location = new System.Drawing.Point(117, 359);
            this.comboBoxSoru.Name = "comboBoxSoru";
            this.comboBoxSoru.Size = new System.Drawing.Size(200, 26);
            this.comboBoxSoru.TabIndex = 26;
            // 
            // kapat
            // 
            this.kapat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(180)))), ((int)(((byte)(50)))));
            this.kapat.BackgroundImage = global::PaintAplication.Properties.Resources.kapat;
            this.kapat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.kapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kapat.ForeColor = System.Drawing.Color.White;
            this.kapat.Location = new System.Drawing.Point(322, 12);
            this.kapat.Name = "kapat";
            this.kapat.Size = new System.Drawing.Size(34, 34);
            this.kapat.TabIndex = 29;
            this.kapat.UseVisualStyleBackColor = false;
            this.kapat.Click += new System.EventHandler(this.kapat_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(180)))), ((int)(((byte)(50)))));
            this.btnGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuncelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnGuncelle.Location = new System.Drawing.Point(143, 436);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(145, 38);
            this.btnGuncelle.TabIndex = 28;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // textBoxCevap
            // 
            this.textBoxCevap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxCevap.Location = new System.Drawing.Point(117, 389);
            this.textBoxCevap.MaxLength = 50;
            this.textBoxCevap.Name = "textBoxCevap";
            this.textBoxCevap.Size = new System.Drawing.Size(200, 24);
            this.textBoxCevap.TabIndex = 27;
            // 
            // textBoxSifretkr
            // 
            this.textBoxSifretkr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSifretkr.Location = new System.Drawing.Point(117, 329);
            this.textBoxSifretkr.MaxLength = 70;
            this.textBoxSifretkr.Name = "textBoxSifretkr";
            this.textBoxSifretkr.Size = new System.Drawing.Size(200, 24);
            this.textBoxSifretkr.TabIndex = 25;
            // 
            // textBoxKadi
            // 
            this.textBoxKadi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxKadi.Location = new System.Drawing.Point(117, 237);
            this.textBoxKadi.MaxLength = 50;
            this.textBoxKadi.Name = "textBoxKadi";
            this.textBoxKadi.Size = new System.Drawing.Size(200, 24);
            this.textBoxKadi.TabIndex = 22;
            // 
            // textBoxMail
            // 
            this.textBoxMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxMail.Location = new System.Drawing.Point(117, 269);
            this.textBoxMail.MaxLength = 70;
            this.textBoxMail.Name = "textBoxMail";
            this.textBoxMail.Size = new System.Drawing.Size(200, 24);
            this.textBoxMail.TabIndex = 23;
            // 
            // textBoxSifre
            // 
            this.textBoxSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSifre.Location = new System.Drawing.Point(117, 299);
            this.textBoxSifre.MaxLength = 70;
            this.textBoxSifre.Name = "textBoxSifre";
            this.textBoxSifre.Size = new System.Drawing.Size(200, 24);
            this.textBoxSifre.TabIndex = 24;
            // 
            // labelCevap
            // 
            this.labelCevap.AutoSize = true;
            this.labelCevap.BackColor = System.Drawing.Color.Transparent;
            this.labelCevap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelCevap.ForeColor = System.Drawing.Color.White;
            this.labelCevap.Location = new System.Drawing.Point(44, 393);
            this.labelCevap.Name = "labelCevap";
            this.labelCevap.Size = new System.Drawing.Size(55, 18);
            this.labelCevap.TabIndex = 19;
            this.labelCevap.Text = "Cevap";
            // 
            // labelSoru
            // 
            this.labelSoru.AutoSize = true;
            this.labelSoru.BackColor = System.Drawing.Color.Transparent;
            this.labelSoru.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSoru.ForeColor = System.Drawing.Color.White;
            this.labelSoru.Location = new System.Drawing.Point(55, 363);
            this.labelSoru.Name = "labelSoru";
            this.labelSoru.Size = new System.Drawing.Size(44, 18);
            this.labelSoru.TabIndex = 18;
            this.labelSoru.Text = "Soru";
            // 
            // labelSifreTekrar
            // 
            this.labelSifreTekrar.AutoSize = true;
            this.labelSifreTekrar.BackColor = System.Drawing.Color.Transparent;
            this.labelSifreTekrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSifreTekrar.ForeColor = System.Drawing.Color.White;
            this.labelSifreTekrar.Location = new System.Drawing.Point(2, 332);
            this.labelSifreTekrar.Name = "labelSifreTekrar";
            this.labelSifreTekrar.Size = new System.Drawing.Size(97, 18);
            this.labelSifreTekrar.TabIndex = 17;
            this.labelSifreTekrar.Text = "Şifre Tekrar";
            // 
            // labelMail
            // 
            this.labelMail.AutoSize = true;
            this.labelMail.BackColor = System.Drawing.Color.Transparent;
            this.labelMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelMail.ForeColor = System.Drawing.Color.White;
            this.labelMail.Location = new System.Drawing.Point(56, 272);
            this.labelMail.Name = "labelMail";
            this.labelMail.Size = new System.Drawing.Size(39, 18);
            this.labelMail.TabIndex = 16;
            this.labelMail.Text = "Mail";
            // 
            // labelSifre
            // 
            this.labelSifre.AutoSize = true;
            this.labelSifre.BackColor = System.Drawing.Color.Transparent;
            this.labelSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSifre.ForeColor = System.Drawing.Color.White;
            this.labelSifre.Location = new System.Drawing.Point(56, 302);
            this.labelSifre.Name = "labelSifre";
            this.labelSifre.Size = new System.Drawing.Size(43, 18);
            this.labelSifre.TabIndex = 15;
            this.labelSifre.Text = "Şifre";
            // 
            // labelKullanıcıAdı
            // 
            this.labelKullanıcıAdı.AutoSize = true;
            this.labelKullanıcıAdı.BackColor = System.Drawing.Color.Transparent;
            this.labelKullanıcıAdı.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKullanıcıAdı.ForeColor = System.Drawing.Color.White;
            this.labelKullanıcıAdı.Location = new System.Drawing.Point(0, 239);
            this.labelKullanıcıAdı.Name = "labelKullanıcıAdı";
            this.labelKullanıcıAdı.Size = new System.Drawing.Size(99, 18);
            this.labelKullanıcıAdı.TabIndex = 14;
            this.labelKullanıcıAdı.Text = "Kullanıcı Adı";
            // 
            // SifremiUnuttum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PaintAplication.Properties.Resources.resetpass;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(368, 561);
            this.Controls.Add(this.comboBoxSoru);
            this.Controls.Add(this.kapat);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.textBoxCevap);
            this.Controls.Add(this.textBoxSifretkr);
            this.Controls.Add(this.textBoxKadi);
            this.Controls.Add(this.textBoxMail);
            this.Controls.Add(this.textBoxSifre);
            this.Controls.Add(this.labelCevap);
            this.Controls.Add(this.labelSoru);
            this.Controls.Add(this.labelSifreTekrar);
            this.Controls.Add(this.labelMail);
            this.Controls.Add(this.labelSifre);
            this.Controls.Add(this.labelKullanıcıAdı);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SifremiUnuttum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SifremiUnuttum";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SifremiUnuttum_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SifremiUnuttum_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SifremiUnuttum_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSoru;
        private System.Windows.Forms.Button kapat;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.TextBox textBoxCevap;
        private System.Windows.Forms.TextBox textBoxSifretkr;
        private System.Windows.Forms.TextBox textBoxKadi;
        private System.Windows.Forms.TextBox textBoxMail;
        private System.Windows.Forms.TextBox textBoxSifre;
        private System.Windows.Forms.Label labelCevap;
        private System.Windows.Forms.Label labelSoru;
        private System.Windows.Forms.Label labelSifreTekrar;
        private System.Windows.Forms.Label labelMail;
        private System.Windows.Forms.Label labelSifre;
        private System.Windows.Forms.Label labelKullanıcıAdı;
    }
}
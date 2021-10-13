
namespace PaintAplication
{
    partial class LisansAktiflestirForm
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
            this.kapat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lisansgirTXT = new System.Windows.Forms.TextBox();
            this.etkinlestir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // kapat
            // 
            this.kapat.BackColor = System.Drawing.Color.Transparent;
            this.kapat.BackgroundImage = global::PaintAplication.Properties.Resources.kapat;
            this.kapat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.kapat.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.kapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kapat.Location = new System.Drawing.Point(650, 12);
            this.kapat.Name = "kapat";
            this.kapat.Size = new System.Drawing.Size(34, 34);
            this.kapat.TabIndex = 0;
            this.kapat.UseVisualStyleBackColor = false;
            this.kapat.Click += new System.EventHandler(this.kapat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Matura MT Script Capitals", 12.15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(85, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(504, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hesabınızı premium\'a yükseltmek için lisans anahtarınızı giriniz.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Matura MT Script Capitals", 10.15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(19, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lisans anahtarı :";
            // 
            // lisansgirTXT
            // 
            this.lisansgirTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lisansgirTXT.Location = new System.Drawing.Point(140, 178);
            this.lisansgirTXT.Name = "lisansgirTXT";
            this.lisansgirTXT.Size = new System.Drawing.Size(449, 27);
            this.lisansgirTXT.TabIndex = 3;
            // 
            // etkinlestir
            // 
            this.etkinlestir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(180)))), ((int)(((byte)(50)))));
            this.etkinlestir.FlatAppearance.BorderSize = 0;
            this.etkinlestir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.etkinlestir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.etkinlestir.Location = new System.Drawing.Point(520, 248);
            this.etkinlestir.Name = "etkinlestir";
            this.etkinlestir.Size = new System.Drawing.Size(100, 34);
            this.etkinlestir.TabIndex = 4;
            this.etkinlestir.Text = "Etkinleştir";
            this.etkinlestir.UseVisualStyleBackColor = false;
            this.etkinlestir.Click += new System.EventHandler(this.etkinlestir_Click);
            // 
            // LisansAktiflestirForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PaintAplication.Properties.Resources.lisansaktifbackraund;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(696, 375);
            this.Controls.Add(this.kapat);
            this.Controls.Add(this.etkinlestir);
            this.Controls.Add(this.lisansgirTXT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LisansAktiflestirForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LisansAktiflestirForm";
            this.Load += new System.EventHandler(this.LisansAktiflestirForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button kapat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lisansgirTXT;
        private System.Windows.Forms.Button etkinlestir;
    }
}
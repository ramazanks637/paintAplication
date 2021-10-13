using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintAplication.classes;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PaintAplication
{
    public partial class K_giris_form : Form
    {
       
        bool move;
        int mouse_x;
        int mouse_y;



        public K_giris_form()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void K_giris_form_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }


        private void K_giris_form_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void K_giris_form_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
           
        }

        // TextBox için Placeholder özelliği
        private void sifreTxt_Click(object sender, EventArgs e)
        {
            if (UserTxt.Text == "")
            {
                UserTxt.Text = "Kullanıcı Adı";
            }

            sifreTxt.Text = "";
        }
        // TextBox için Placeholder özelliği
        private void kAdiTxt_Click(object sender, EventArgs e)
        {
            if (UserTxt.Text == "Kullanıcı Adı")
            {
                UserTxt.Text = "";
                UserTxt.ForeColor = Color.Silver;

            }
            else if (UserTxt.Text == "")
            {
                UserTxt.Text = "Kullanıcı Adı";
            }
            if (sifreTxt.Text == "")
            {
                sifreTxt.Text = "Parola";
            }
        }

        // şifreyi gizle / göster tuşunun fonksiyonu
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isVisible = checkBox1.CheckState == CheckState.Unchecked;
            sifreTxt.UseSystemPasswordChar = isVisible;
            checkBox1.Text = isVisible ? "Göster" : "Gizle";
        
        }

       
        private void loginBtn_Click(object sender, EventArgs e)
        {
            Kullanici_islemleri k = new Kullanici_islemleri();
            k.login(UserTxt, sifreTxt);
            this.Hide();
            
        }


        private void linkLabelSifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new SifremiUnuttum().ShowDialog();
        }

        private void linkLabelKAyitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new UyeOl().ShowDialog();

        }

        private void K_giris_form_Load(object sender, EventArgs e)
        {
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintAplication.classes;
using MySql.Data.MySqlClient;

namespace PaintAplication
{
    public partial class UyeOl : Form
    {

       
        bool move;
        int mouse_x;
        int mouse_y;


        public UyeOl()
        {
            InitializeComponent();
        }

        private void UyeOl_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void UyeOl_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void UyeOl_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            this.Hide();
            new K_giris_form().ShowDialog();
        }

        
        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            Kullanici_islemleri k = new Kullanici_islemleri();
            k.yenikullanıcı(textBoxAd, textBoxSoyad, textBoxKadi, textBoxMail, textBoxSifre, textBoxSifretkr, comboBoxSoru, textBoxCevap);

        }
    }
}

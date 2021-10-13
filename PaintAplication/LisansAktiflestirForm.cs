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
    public partial class LisansAktiflestirForm : Form
    {
        MySqlConnection baglanti;
        MySqlCommand komut;
        MySqlDataReader read;
        
        int userId;
        public LisansAktiflestirForm()
        {
            InitializeComponent();
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void etkinlestir_Click(object sender, EventArgs e)
        {
            
            baglanti = dbBaglanti.veritabanibagla();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from lisanslar where lisans_key='" + lisansgirTXT.Text + "'";
            read = komut.ExecuteReader();
            // Girilen lisans anahtarı veritabanında var mı ? 
            if (read.Read()==true)
            {
                baglanti.Close();
                baglanti.Open();
                komut.CommandText = "select * from lisanslar where lisans_key='" + lisansgirTXT.Text + "' and lisans_durum=1 ";
                read = komut.ExecuteReader();
                // Girilen lisans anahtarı veritabanında var ve kullanılmış mı?
                if (read.Read()==false)
                {
                    // Lisansı kullanılmış işaretle ve kullanıcıyı premium yap, uygulamayı yeniden başlat
                    baglanti.Close();
                    baglanti.Open();
                    komut.CommandText = "update lisanslar set lisans_durum = 1 where lisans_key='" + lisansgirTXT.Text + "'";
                    read = komut.ExecuteReader();
                    baglanti.Close();
                    baglanti.Open();
                    komut.CommandText = "update kullanicilar set kullanici_durum = 1 where kullanici_id= '" + userId + "' ";
                    read = komut.ExecuteReader();
                    baglanti.Close();
                    MessageBox.Show("Premium Hesaba Geçtiniz ");
                    Properties.Settings.Default.lisans = 1;
                    Properties.Settings.Default.Save();
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("Bu Lisans anahtarı daha önceden Kullanılmıştır");
                }
                
            }
            else
            {
                MessageBox.Show("Doğru lisans anahtarı giriniz");
            }

        }

    
        
    private void LisansAktiflestirForm_Load(object sender, EventArgs e)
        {
            userId = Properties.Settings.Default.userId;
        }
    }
}

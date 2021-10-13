using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PaintAplication.classes
{
    class Kullanici_islemleri
    {
        MySqlConnection baglanti;
        MySqlCommand komut;
        MySqlDataReader read;
        string serialKey;
        LisansAktiflestirForm frm = new LisansAktiflestirForm();

        // Benzersiz bir serial key oluşturuyorum.
        public string lisansolustur()
        {
            baglanti = dbBaglanti.veritabanibagla();
            while (true)
            {
                // String bir lisans dizisi olutşurur.
                SKGL.Generate generate = new SKGL.Generate();
                generate.secretPhase = "ASC";
                serialKey = generate.doKey(Convert.ToInt32(365));

                // oluşturulan lisans daha önce oluşturmuş mu kontrol eder
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select * from lisanslar where lisans_key='" + serialKey + "'";
                read = komut.ExecuteReader();
                // Eğer daha önce oluşturulmamış ise döngüden çıkıyor.
                if (read.Read() == false) break;
            }
            baglanti.Close();
            baglanti.Open();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            // Benzersiz lisansı veritabanına kaydeder
            komut.CommandText = "insert into lisanslar (lisans_key) values('" + serialKey + "')";
            read = komut.ExecuteReader();
            baglanti.Close();
            return serialKey;
        }

        public void cikisYap()
        {
            Properties.Settings.Default.userId = 0;
            Properties.Settings.Default.lisans = 0;
            Properties.Settings.Default.Save();
            Application.Restart();
        }

        public void login(TextBox kullanıcıadı, TextBox parola)
        {

            baglanti = dbBaglanti.veritabanibagla();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from kullanicilar where kullanici_user='" + kullanıcıadı.Text + "' and kullanici_sifre='" + parola.Text + "'";
            read = komut.ExecuteReader();
            if (read.Read() == true)
            {
                Properties.Settings.Default.userId = read.GetInt32("kullanici_id");
                Properties.Settings.Default.lisans = read.GetInt32("kullanici_durum");
                Properties.Settings.Default.Save();
                baglanti.Close();

                if (Properties.Settings.Default.lisans == 1)
                {
                    PremiumForm Form1 = new PremiumForm();
                    Form1.Show();
                }
                else
                {
                    DemoForm1 DemoForm1 = new DemoForm1();
                    DemoForm1.Show();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Hata");
            }
            baglanti.Close();
        }

        // giriş yapmış olan kullanıcın herhangi bir metinsel kullanıcı bilgisini döndürür.
        public String currentUserStringValue(String columnName)
        {
            String data = "";
            int userId = Properties.Settings.Default.userId;
            baglanti = dbBaglanti.veritabanibagla();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            //Giriş yapmış olan kullanıcın tüm değerlerini veritabanından çekiyor
            komut.CommandText = "select * from kullanicilar where kullanici_id='" + Convert.ToString(userId) + "'";
            read = komut.ExecuteReader();
            if (read.Read() == true)
            {
                // Fonksiyonda parametre olarak verilen kolon adına göre değerini çekiyor.
                data = read.GetString(columnName);
            }
            baglanti.Close();
            return data;
        }

        // giriş yapmış olan kullanıcın herhangi bir sayısal kullanıcı bilgisini döndürür.
        public int currentUserIntValue(String columnName)
        {
            int data = 0;
            int userId = Properties.Settings.Default.userId;
            baglanti = dbBaglanti.veritabanibagla();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            //Giriş yapmış olan kullanıcın tüm değerlerini veritabanından çekiyor
            komut.CommandText = "select * from kullanicilar where kullanici_id='" + Convert.ToString(userId) + "'";
            read = komut.ExecuteReader();
            if (read.Read() == true)
            {
                // Fonksiyonda parametre olarak verilen kolon adına göre değerini çekiyor.
                data = read.GetInt32(columnName);
            }
            baglanti.Close();
            return data;
        }



        public void yenikullanıcı(TextBox ad, TextBox soyad, TextBox kullanıcıadı, TextBox Mail, TextBox sifre, TextBox sifretkr, ComboBox soru, TextBox cevap)
        {
            baglanti = dbBaglanti.veritabanibagla();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            // Kullanıcı varmı kontrol ediyor
            komut.CommandText = "select *from kullanicilar where kullanici_user='" + kullanıcıadı.Text + "' or kullanici_mail='" + Mail.Text + "'";
            read = komut.ExecuteReader();
            if (read.Read() == false)
            {
                // Mail doğru mu kontorl ediyor
                if (Mail.Text.Contains("@"))
                {
                    if (Mail.Text.Contains(".com"))
                    {

                        if (sifre.Text == sifretkr.Text)
                        {
                            baglanti.Close();
                            baglanti.Open();
                            komut = new MySqlCommand();
                            komut.Connection = baglanti;
                            //Kullanıcıyı kaydediyor
                            komut.CommandText = "insert into kullanicilar (kullanici_ad,kullanici_soyad,kullanici_user,kullanici_mail,kullanici_sifre,kullanici_sifretkr,kullanici_soru,kullanici_cevap) values('" + ad.Text + "','" + soyad.Text + "','" + kullanıcıadı.Text + "','" + Mail.Text + "','" + sifre.Text + "','" + sifretkr.Text + "','" + soru.Text + "','" + cevap.Text + "')";
                            read = komut.ExecuteReader();
                            baglanti.Close();
                            MessageBox.Show("Kullanıcı Eklendi ");
                        }
                        else
                        {
                            MessageBox.Show("Şifreler Uyuşmuyor !!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mail Adresi Biçimi Yanlış !!!");
                    }
                }
                else
                {
                    MessageBox.Show("Mail Adresi Biçimi Yanlış !!!");
                }

            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Mail kayıtlarımızda mevcuttur !!!");
            }
        }


        public void sifremiunuttum(TextBox kullanıcıadı, TextBox Mail, TextBox sifre, TextBox sifretkr, ComboBox soru, TextBox cevap)
        {
            if (sifre.Text == sifretkr.Text)
            {
                baglanti = dbBaglanti.veritabanibagla();
                //Kullanıcı var mı kontrol eder
                komut = new MySqlCommand("select *from kullanicilar where kullanici_user='" + kullanıcıadı.Text + "'", baglanti);
                read = komut.ExecuteReader();

                if (read.Read() == true)
                {
                    // Kullanıcının güvenlik soruları doğru mu kotnrol eder
                    if (soru.Text == read["kullanici_soru"].ToString() && cevap.Text == read["kullanici_cevap"].ToString())
                    {

                        // Mail doğru mu kontrol eder
                        if (Mail.Text.Contains("@"))
                        {
                            if (Mail.Text.Contains(".com"))
                            {
                                baglanti.Close();
                                baglanti.Open();
                                // Şifreyi günceller
                                komut = new MySqlCommand("update kullanicilar set  kullanici_user='" + kullanıcıadı.Text + "',kullanici_mail='" + Mail.Text + "',kullanici_sifre='" + sifre.Text + "',kullanici_sifretkr='" + sifretkr.Text + "',kullanici_soru='" + soru.Text + "',kullanici_cevap='" + cevap.Text + "' where kullanici_user='" + kullanıcıadı.Text + "' ", baglanti);
                                read = komut.ExecuteReader();
                                MessageBox.Show("İşlem Başarılı");
                                baglanti.Close();
                            }
                            else
                            {
                                MessageBox.Show("Mail Adresi Biçimi Yanlış !!!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mail Adresi Biçimi Yanlış !!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Soru Ve Cevap Uyuşmuyor", "Hata1");
                    }
                }
                else
                {
                    MessageBox.Show("Bilgilerinizi kontrol ediniz.", "Hata2");
                }
                baglanti.Close();

            }

            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor !!!", "Hata3");
            }


        }




    }
}

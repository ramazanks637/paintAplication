using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Net;
using System.Net.Mail;
using PaintAplication.classes;

namespace PaintAplication
{
    public partial class SatinAl : KryptonForm
    {
        String email = "umuterzi3492@gmail.com", sifre = "onur359532";
        SmtpClient sc = new SmtpClient();

        public SatinAl()
        {
            InitializeComponent();
        }

        private void SatinAl_Load(object sender, EventArgs e)
        {
            // mail sunucuları ayarı
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential(email, sifre);
        }

        private void satinAlButton2_Click(object sender, EventArgs e)
        {
            if(kartnotxt.Text == "" || cvvtxt.Text == "" || aycmb.Text == "" || yilcmb.Text == ""){
                MessageBox.Show("Boş Alan Bırakmayınız");
            }else {
                if(odendicb.Checked == true){
                    MessageBox.Show("Ödeme Başarılı");

                    //Benzersiz lisans kodu oluşturan fonksiyonun çağırılması
                    String lisansKey = new Kullanici_islemleri().lisansolustur();

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(email, "Paint Uygulaması");
                    // statik olarak yazmak yerine aşağıdaki fonksiyon ile giriş yapmış olduğumuz kullanıcı mailini çekiyoruz.
                    mail.To.Add(new Kullanici_islemleri().currentUserStringValue("kullanici_mail"));
                    /* mail.To.Add("ensarkurt@gmail.com"); */// kaç tane yazarak o kadar maile lisans gönderir.
                    mail.Subject = "Premium Lisans Kodu"; 
                    // html kodu eklenebilirmi sorusu 
                    mail.IsBodyHtml = true; 
                    mail.Body = "<b>Lisans Kodunuz</b>: '"+lisansKey+"'";
                    sc.Send(mail);
                }else {
                    MessageBox.Show("Ödeme Başarısız");
                }
            }
        }
    }
}

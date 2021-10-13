using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintAplication
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value >=100) {
                int userId =  Properties.Settings.Default.userId;
                // Kullanıcı giriş yapmış mı yapmamış mı kontrol ediyor
                if (userId != 0){
                    // Kulanıcı giriş yapmış ise
                    int lisans = Properties.Settings.Default.lisans;
                    if(lisans == 1) {
                        // Lisanslı kullanıcı ise 
                        this.Hide();
                        PremiumForm Form1 = new PremiumForm();
                        Form1.Show();
                    }
                    else
                    {
                        // Lisanslı kullanıcı değil ise 
                        this.Hide();
                        DemoForm1 DemoForm1 = new DemoForm1();
                        DemoForm1.Show();
                    }
                }else {
                    // Kullanıcı giriş yapmamış ise
                    this.Hide();
                    K_giris_form K_giris_form = new K_giris_form();
                    K_giris_form.Show();
                }
                timer1.Stop();        
            }else {
                progressBar1.Value+=5;
            }
        }

        private void Loading_Load(object sender, EventArgs e)
        {

        }
    }
}

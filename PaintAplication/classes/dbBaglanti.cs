using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;


namespace PaintAplication
{
    class dbBaglanti
    {

        public static MySqlConnection veritabanibagla()
        {

            MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=paint_kullanici;SslMode=none";
            conn = new MySqlConnection();
            conn.ConnectionString = myConnectionString;
            try
                {
                    
                    conn.Open();
                    //MessageBox.Show("bağlantı sağlandı");
                    
            }
                catch (MySqlException ex)
                {
                
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("bağlantı olmadı");

                }
            return conn;
        }




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace yemek
{
    
    public partial class turekle : Form
    {
        SqlConnection con = new SqlConnection("Data Source= .; initial Catalog=odev;integrated security=true");
        public turekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // öncelikle textboxın boş olup olmadığını kontrol edelim
            if (textBox1.Text == "")
                MessageBox.Show("Lütfen yiyecek türünü giriniz.", "BOŞ ALANLAR VAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // boş değilse sql komutu ile ekleme işlemi yapılır
                SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");
                SqlCommand cmd = new SqlCommand(" insert into tarifturu (tarifturu) values ( '" + textBox1.Text + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                label2.Text = "tarif türü başarıyla eklenmiştir. :):):)";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        }
    }


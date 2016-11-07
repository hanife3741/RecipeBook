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
    public partial class turduzenle : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");
        public turduzenle()
        {
            InitializeComponent();
            Doldur();
        }
         private void Doldur()
        {
            //Form açılır açılmaz combobox, tarifturu Tablosundaki veriler ile doldurulur
            SqlDataAdapter adap = new SqlDataAdapter("select * from tarifturu order by tarifturu", con);

            DataTable dt = new DataTable();
            adap.Fill(dt);

            //Verileri comboboxa atmak için DataSource kullanılır.
            //DataSource verikaynağı demektir.
            comboBox1.DataSource = dt;

            //Comboboxta iki tane veri tutulabilir. 
            // 1) kullanıcı tarafından görünen = DisplayMember
            // 2) kullanıcı tarafından görünmeyen ama kod kısmında kullanılabilen veri = ValueMember
            comboBox1.DisplayMember = "tarifturu";
            comboBox1.ValueMember = "tarifturuID"; // -> value member değerine attığımız için id yi buradan alacağız
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int turID = int.Parse(comboBox1.SelectedValue.ToString());

            //Öncelikle textboxın boş olup olmadığını kontrol edelim
            if ( textBox1.Text == "")
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz.", "BOŞ ALANLAR VAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // değiştirme buradaki sql komutu ile yapılacak.
                SqlCommand cmd = new SqlCommand("update tarifturu set tarifturu = '" + textBox1.Text + "' where tarifturuID = " + turID + "", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                lbl_uyari.Text = "Tarif türü başarıyla düzenlenmiştir. :):):)";
                Doldur();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
       
    }
}

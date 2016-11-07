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
    public partial class giris : Form
    {
        SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = odev;Integrated Security = True");
        SqlCommand komut;
        public giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanici, sifre;
            kullanici = textBox1.Text;
            sifre = textBox2.Text;
            komut = new SqlCommand("select count(*) from hesap where kullaniciadi=@kullaniciadi and sifre=@sifre",con);
            komut.Parameters.AddWithValue("@kullaniciadi", kullanici);
            komut.Parameters.AddWithValue("@sifre", sifre);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int ks = int.Parse(komut.ExecuteScalar().ToString());
            if (ks == 0)
                MessageBox.Show("Uygun kayıt bulunmamaktadır.Lütfen bilgilerinizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.Hide();
                anamenu frm2 = new anamenu();
                frm2.Show();//göster anlamında show kullandık.
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             string kullanici, sifre;
            kullanici = textBox1.Text;
            sifre = textBox2.Text;
            if (kullanici == "" || sifre == "")
            {
                MessageBox.Show("isim alanı boş olamaz");
                return;
            }
            if (kullanici == "@kullaniciadi")
            {
                MessageBox.Show("bu isimde başka bir kullanıcı var! Lütfen başka bir isim giriniz..");
                return;
            }
            komut = new SqlCommand("insert into hesap(kullaniciadi,sifre) values ('"+kullanici+"','"+sifre+"')" , con);
            if (con.State == ConnectionState.Closed) //baglanti kapalıysa baglantiyi açtik
                con.Open();
            komut.ExecuteNonQuery();
            label3.Text="bilgileriniz basarıyla eklenmiştir giriş yapabilirsiniz..";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("O SENİN PROBLEMİN :P","OSP",MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button3);
        }

        private void giris_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class kullaniciduzenle : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");
        public kullaniciduzenle()
        {
            InitializeComponent();
       
        
            //Form açılır açılmaz combobox, hesap tablosundaki veriler ile doldurulur
            SqlDataAdapter adap = new SqlDataAdapter("select * from hesap order by kullaniciadi", con);

            DataTable dt = new DataTable();
            adap.Fill(dt);

            //Verileri comboboxa atmak için DataSource kullanılır.
            //Datasource verikaynağı demektir.
            comboBox1.DataSource = dt;

            //Comboboxta iki tane veri tutulabilir. 
            // 1) kullanıcı tarafından görünen = DisplayMember
            // 2) kullanıcı tarafından görünmeyen ama kod kısmında kullanılabilen veri = ValueMember
            comboBox1.DisplayMember = "kullaniciadi";
            comboBox1.ValueMember = "kID"; // -> value member değerine attığımız için id yi buradan alacağız
    }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Comboboxtaki seçili veri değiştikçe textboxlar dolacak 

            //comboBox1.SelectedValue ifadesi object türünde bir değer geriye döndürüğü için ilk önce stringe çevirip daha sonra int türüne dönüştürüyoruz.
            //Çünkü objekt türü direkt olarak int türüne dönüştürelemez.
            int kID;
            string a=comboBox1.SelectedValue.ToString();
            kID =int.Parse(a);
            // -> valueMember'deki değer için SelectedValue

            SqlDataAdapter adap = new SqlDataAdapter("select * from hesap where kID = " + kID + "", con);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            //DataTable, tablo gibi düşünürsek satır ve sütünlardan oluşur
            //Datarow datatable'ın bir satırı demek
            //Yani foreach ile datatablo'daki satırları dolaşacağız

            //foreach ile dt.Rows ifadesinden dolayı döngümüz oluşturduğumuzdaki DataTabledaki her satırı teker teker dolaşaktır.
            //Satırlar dolaşılacağı için geriye DataRow türünde bir değer dönecektir. ve dönen değeri de item değişkenine aktardık.
            foreach (DataRow item in dt.Rows)
            {
                //Textboxlar doldurulur
                //Satırda 4 tane sütun var idleri sırasıyla 
                // [0] -> kID, [1] -> kullaniciadi, [2] -> sifre

                //item bir satır olduğu için [1] ifadesi ile birinci indexteki sütuna yani 2. sutuna erişmekteyiz.

           textBox1.Text = item[1].ToString();
           textBox2.Text = item[2].ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           int kID = int.Parse(comboBox1.SelectedValue.ToString());

            //Öncelikle textboxların boş olup olmadığını kontrol edelim
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz.", "BOŞ ALANLAR VAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Değiştirme buradaki sql komutu ile yapılacak.
                SqlCommand cmd = new SqlCommand("update hesap set kullaniciadi = '" + textBox1.Text + "', sifre = '" + textBox2.Text + "' where kID = " + kID, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_uyari.Text = "Kullanıcı bilgileri başarıyla düzenlenmiştir. :):):)";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        
}
 }
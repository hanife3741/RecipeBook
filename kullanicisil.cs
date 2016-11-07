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
    public partial class kullanicisil : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");
        public kullanicisil()
        {
            InitializeComponent();
            Doldur_cb();
            Doldur_lbl();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Doldur_lbl();
        }
        private void Doldur_lbl()
    {
        //Comboboxtaki seçili veri değiştikçe labeller dolacak 

        int kID = int.Parse(comboBox1.SelectedValue.ToString());
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
            // label'ler doldurulur
            // satırda 4 tane sütun var idleri sırasıyla 
            // [0] -> kID, [1] -> kullaniciadi, [2] -> sifre

            //item bir satır olduğu için [1] ifadesi ile birinci indexteki sütuna yani 2. sutuna erişmekteyiz.

            lbl_kullanici.Text = item[1].ToString();
            lbl_sfre.Text = item[2].ToString();
        }

    }

        private void button1_Click(object sender, EventArgs e)
        {
            int kID = int.Parse(comboBox1.SelectedValue.ToString());

            // değiştirme buradaki sql komutu ile yapılacak.
            SqlCommand cmd = new SqlCommand("delete from hesap where kID = " + kID, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //Silme işlemi yapıldıktan sonra combobox yeniden doldurulur
            // Doldur_cb() şeklinde bir metot ile combobox yeniden doldurulur
            Doldur_cb();
            MessageBox.Show("Kullanıcı silinmiştir.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Doldur_cb()
        {
            //Form açılır açılmaz combobox, hesap tablosundaki veriler ile doldurulur
            SqlDataAdapter adap = new SqlDataAdapter("select * from hesap order by kullaniciadi", con);

            DataTable dt = new DataTable();
            adap.Fill(dt);

            //Verileri comboboxa atmak için DataSource kullanılır.
            comboBox1.DataSource = dt;

            //Comboboxta iki tane veri tutulabilir. 
            // 1) kullanıcı tarafından görünen = DisplayMember
            // 2) kullanıcı tarafından görünmeyen ama kod kısmında kullanılabilen veri = ValueMember
            comboBox1.DisplayMember = "kullaniciadi";
            comboBox1.ValueMember = "kID";
            // -> value member değerine attığımız için id yi buradan alacağız

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void kullanicisil_Load(object sender, EventArgs e)
        {

        }


   
    }
}

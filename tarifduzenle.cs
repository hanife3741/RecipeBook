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
    public partial class tarifduzenle : Form
    {

        SqlConnection con = new SqlConnection("Server=HANIFE;Database=odev;Trusted_Connection=True;");
        DataTable dt;
        SqlDataAdapter sda;
        public tarifduzenle()
        {
            InitializeComponent();
            GridDoldur();
        }
         private void GridDoldur()
         {
        // gridview, tarifler tablosundan gelen veriler ile dordurulur
            // bu sırada tarifturuID yardımıyla diğer tablo da çağırılır
            sda = new SqlDataAdapter("SELECT tarifler.tarifID, tarifler.tarifadi, tarifler.malzemeler, tarifler.hazirlanisi, tarifturu.tarifturuID, tarifturu.tarifturu FROM tarifler INNER JOIN tarifturu ON tarifler.tarifturuID = tarifturu.tarifturuID", con);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            // tarifID ile tarifturuID nin kullanıcılar tarafından 
            // görünmesini istemediğimiz için bu sütunların Visible = false yapılır 
            dataGridView1.Columns["tarifID"].Visible = false;
            dataGridView1.Columns["tarifturuID"].Visible = false;
         }

         private void button1_Click(object sender, EventArgs e)
         {
             // öncelikle textboxlar kontrol edilir
             if (txttrfadi.Text.Trim() == "" || txtmlzmlr.Text.Trim() == "" || txthazirlanisi.Text.Trim() == "")
                 MessageBox.Show("Lütfen bütün alanları doldurunuz", "BOŞ ALANLAR VAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
             else
             {
                 // eğer textboxlar boş değilse duzenleme işlemi yapılır
                 string tarifadi = txttrfadi.Text.Trim();
                 string malzemeler = txtmlzmlr.Text.Trim();
                 string hazirlanisi = txthazirlanisi.Text.Trim();
                 int ZorlukDerecesi = 0;
                 if (radioButton1.Checked)
                 {
                     ZorlukDerecesi = 1;
                 }
                 else
                     if (radioButton2.Checked)
                     {
                         ZorlukDerecesi = 2;
                     }
                     else
                         if (radioButton3.Checked)
                         {
                             ZorlukDerecesi = 3;
                         }
                         else
                             if (radioButton4.Checked)
                             {
                                 ZorlukDerecesi = 4;
                             }
                             else
                                 if (radioButton5.Checked)
                                 {
                                     ZorlukDerecesi = 5;
                                 }

                 // kontrol
                 if (ZorlukDerecesi == 0)
                 {
                     MessageBox.Show("zorluk derecesinizi seçiniz");
                     return;
                 }

                 int turu = int.Parse(comboBox1.SelectedValue.ToString());

                 //DataGridView de, seçili ilk satırdanki tarifID sütunundaki değeri alıp, önce string sonra inte çeviriyoruz.
                 int tarifID = int.Parse(dataGridView1.SelectedRows[0].Cells["tarifID"].Value.ToString());

                 SqlCommand cmd = new SqlCommand("update tarifler Set tarifadi =@adi, malzemeler = @malzemeler, hazirlanisi = @hazirlanisi, ZorlukDerecesi = @ZorlukDerecesi,tarifturuID = @tarifturu where tarifID = @tarifID", con);
                 cmd.Parameters.AddWithValue("@adi", tarifadi);
                 cmd.Parameters.AddWithValue("@malzemeler", malzemeler);
                 cmd.Parameters.AddWithValue("@hazirlanisi", hazirlanisi);
                 cmd.Parameters.AddWithValue("@ZorlukDerecesi", ZorlukDerecesi);
                 cmd.Parameters.AddWithValue("@tarifturu", turu);
                 cmd.Parameters.AddWithValue("@tarifID", tarifID);

                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();
                 lbl_uyari.Text = "Tarif başarıyla güncellenmiştir. :):):)";

                 GridDoldur();
             }
         }
   

        private void dataGridView1_SelectedChanged(object sender, EventArgs e)
        {
        // gridviewde seçilen satır değiştikçe textboxlar doldurulur
            //Eğer hiç satır seçilmemişse işlem yapma. Return işlemi sonlandırır. Aşağıdaki kodlar çalışmaz.
            if (dataGridView1.SelectedRows.Count == 0)
                return; 
            
            //DataGridView de, seçili ilk satırdanki tarifID sütunundaki değeri alıp, önce string sora inte çeviriyoruz.
            int tarifID = int.Parse(dataGridView1.SelectedRows[0].Cells["tarifID"].Value.ToString());

            //DataGridView de, seçili ilk satırdanki tarifadi sütunundaki değeri alıp, stringe çevirip textboxa yazıyoruz.
            txttrfadi.Text = dataGridView1.SelectedRows[0].Cells["tarifadi"].Value.ToString();
            txtmlzmlr.Text = dataGridView1.SelectedRows[0].Cells["malzemeler"].Value.ToString();
            txthazirlanisi.Text = dataGridView1.SelectedRows[0].Cells["hazirlanisi"].Value.ToString();
            //Türlerin olduğu comboboxı doldurmak için türleri getiriyoruz.
            sda = new SqlDataAdapter("select * from tarifturu order by tarifturu", con);
            dt = new DataTable();
            sda.Fill(dt);
            // verileri comboboxa atmak için DataSource kullanılır.
            comboBox1.DataSource = dt;

            // comboboxta iki tane veri tutulabilir. 
            // 1) kullanıcı tarafından görünen = DisplayMember
            // 2) kullanıcı tarafından görünmeyen ama kod kısmında kullanılabilen veri = ValueMember
            comboBox1.ValueMember = "tarifturuID";
            comboBox1.DisplayMember = "tarifturu";

            //DataGridView de tarifturuID yi gizlemiştik. 
            //Seçili kolondaki tarifturuID kolunundaki değeri alıyoruz.
            //Gelen değer seçili satırdaki tarifin türünün idsidir. Çünkü DataGridViewde gizlediğimiz tarifturuID sütununa tarifin tarifturuIDsini aramıştık.
            //Comboboxında Value değerleri İDlerinden oluştuğu için seçili elemanın value değerini DataGridView de seçtiğimiz elemanın tarifturuIDsi yapıyoruz.
            comboBox1.SelectedValue = dataGridView1.SelectedRows[0].Cells["tarifturuID"].Value.ToString();
        }

    }
}
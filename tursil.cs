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
    public partial class tursil : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");
        public tursil()
        {
            InitializeComponent();
            Doldur();
        }

        private void Doldur()
        {
            // form açılır açılmaz combobox, tarifturu Tablosundaki veriler ile doldurulur
            SqlDataAdapter adap = new SqlDataAdapter("select * from tarifturu order by tarifturu", con);

            DataTable dt = new DataTable();
            adap.Fill(dt);

            // verileri comboboxa atmak için DataSource kullanılır.
            comboBox1.DataSource = dt;

            // comboboxta iki tane veri tutulabilir. 
            // 1) kullanıcı tarafından görünen = DisplayMember
            // 2) kullanıcı tarafından görünmeyen ama kod kısmında kullanılabilen veri = ValueMember
            comboBox1.DisplayMember = "tarifturu";
            comboBox1.ValueMember = "tarifturuID"; // -> value member değerine attığımız için id yi buradan alacağız
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            int turID = int.Parse(comboBox1.SelectedValue.ToString());

            //tarif türünü silmeden önce ilk önce o türe ait tarifleri silmeliyiz. Aksi halde tariflerde yazan tarifturuID deki değere Karşılık tarifturu Tablosunda bir değer olmaz ve bu da hataya yol açar.
            YiyecekleriSil(turID);
            YiyecekTuruSil(turID);

            MessageBox.Show("Tarif türü başarıyla silinmiştir.", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Doldur();
        }
        private void YiyecekTuruSil(int turID)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tarifturu WHERE tarifturuID = @turID", con);
            cmd.Parameters.AddWithValue("@turID", turID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void YiyecekleriSil(int turID)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tarifler WHERE tarifturuID = @turID", con);
            //aşağıdaki ifade  Sql sorgusundaki  @turID  ifadesi bir parametredir ve değeri turID ' dir diyor.
            cmd.Parameters.AddWithValue("@turID", turID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

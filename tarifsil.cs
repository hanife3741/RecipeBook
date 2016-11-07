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
    public partial class tarifsil :Form
    { 
       

        public tarifsil()
        {
            InitializeComponent();
            GridDoldur();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");
        DataTable dt;
        SqlDataAdapter sda;

        int ID;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tarifler WHERE tarifID = @tarifID", con);
            cmd.Parameters.AddWithValue("@tarifID", ID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("tarif başarıyla silinmiştir.", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GridDoldur();
        }
        private void GridDoldur()
        {
            // gridview, tarifler tablosundan gelen veriler ile dordurulur
            // bu sırada tarifturuID yardımıyla diğer tablo da çağırılır
            sda = new SqlDataAdapter("SELECT tarifler.tarifID, tarifler.tarifadi, tarifturu.tarifturuID,tarifturu.tarifturu FROM tarifler INNER JOIN tarifturu ON  tarifler.tarifturuID = tarifturu.tarifturuID", con);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            // tarifID ile tarifturuID nin kullanıcılar tarafından 
            // görünmesini istemediğimiz için bu sütunların Visible = false yapılır 
            dataGridView1.Columns["tarifID"].Visible = false;
            dataGridView1.Columns["tarifturuID"].Visible = false;
        }
        private void dataGridView1_SelectedChanged(object sender, EventArgs e)
        {

            // gridviewde seçilen satır değiştikçe textboxlar doldurulur
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            //DataGridView de, seçili ilk satırdanki tarifID sütunundaki değeri alıp, önce string sora inte çeviriyoruz.
            ID = int.Parse(dataGridView1.SelectedRows[0].Cells["tarifID"].Value.ToString());

            lbltrftr.Text = dataGridView1.SelectedRows[0].Cells["tarifturu"].Value.ToString();
            lbltrfadi.Text = dataGridView1.SelectedRows[0].Cells["tarifadi"].Value.ToString();
            //sda = new SqlDataAdapter("select * from tarifturu order by tarifturu", con);
            //DataTable dt = new DataTable();

            //sda.Fill(dt);
        }

       
    }
}

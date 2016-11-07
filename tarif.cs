using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace yemek
{
    public partial class tarif : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=odev;Trusted_Connection=True;");

        public tarif()
        {
            InitializeComponent();
            //İlk önce tarif türü doldurulur.
            tarifturuDoldur();

            //Daha sonra tür idden seçili olan türün idsi alınır(yeni dolduğu için ilk elemanın idsi yani).
            int ID = int.Parse(cmbtrftr.SelectedValue.ToString());
            //Seçilen türe göre tarifler doldurulur.
            tarifDoldur(ID);
        }

        private void tarifDoldur(int turID)
        {
            //throw new NotImplementedException();
            SqlDataAdapter adap = new SqlDataAdapter("select * from tarifler where tarifturuID = " + turID + " order by tarifadi", con);

            DataTable dt = new DataTable();
            adap.Fill(dt);

            cmbtrfadi.DataSource = dt;
            cmbtrfadi.DisplayMember = "tarifadi";
            cmbtrfadi.ValueMember = "tarifID"; // -> value member değerine attığımız için id yi buradan alacağız

        }

        private void tarifturuDoldur()
        {
            //throw new NotImplementedException();
            SqlDataAdapter adap = new SqlDataAdapter("select * from tarifturu order by tarifturu", con);

            DataTable dt = new DataTable();
            adap.Fill(dt);

            cmbtrftr.DataSource = dt;
            cmbtrftr.DisplayMember = "tarifturu";
            cmbtrftr.ValueMember = "tarifturuID"; // -> value member değerine attığımız için id yi buradan alacağız
        }

        private void cmbtrftr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seçilen türe göre tariflerler getirilmeli.
            //Seçilen türün value değeri alınır. Yani value değerinde tarifturuIDsini tuttuğumuz için tarifturuID ye ulaşmış oluruz.
            if (cmbtrftr.SelectedValue.ToString() != "")
            {
                int ID = int.Parse(cmbtrftr.SelectedValue.ToString());
                tarifDoldur(ID);
            }
        }

        private void cmbtrfadi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seçilen türe göre tarifler getirilmeli.
            //Seçilen türün value değeri alınır. Yani value değerinde tarifturuIDsini tuttuğumuz için tarifturuID ye ulaşmış oluruz.
            if (cmbtrfadi.SelectedValue.ToString() != "")
            {
                int trfID = int.Parse(cmbtrfadi.SelectedValue.ToString());
                textBox1.Text = "";
                textBox2.Text = "";
                SqlCommand komut2 = new SqlCommand("select tarifadi,malzemeler,hazirlanisi from tarifler where tarifID=" + trfID + "", con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = komut2.ExecuteReader();
                while (dr.Read())
                {
                    tarif trf = new tarif();
                    trf.Text = dr[0].ToString();
                    textBox1.Text = dr[1].ToString();
                    textBox2.Text = dr[2].ToString();
                    textBox3.Text = "tarif adı \n" + trf + " \n malzemeler: \n" + textBox1 + "\n hazirlanisi" + textBox2;
                    textBox3.Enabled = false;
                }
                con.Close();
            }
        }

      

        int toplamsayfa = 2;
        int sayfano = 1;
      
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //yazi fontumuzu ayarliyoruz
            Graphics gr = e.Graphics;
            //yazi tipi Trebuchet MS, boyutu 30 ve bold karakterlerle yazilicak
            Font f = new Font("Trebuchet MS", 30, FontStyle.Bold);
            //yazdirma alanimizin ozellıklerını belirliyoruz
            Rectangle yazdirma_alani = new Rectangle();
            yazdirma_alani.X = this.printDocument1.DefaultPageSettings.Margins.Left;
            yazdirma_alani.Y = this.printDocument1.DefaultPageSettings.Margins.Top;
            yazdirma_alani.Width = this.printDocument1.DefaultPageSettings.PaperSize.Width - this.printDocument1.DefaultPageSettings.Margins.Left - this.printDocument1.DefaultPageSettings.Margins.Right;
            yazdirma_alani.Height = this.printDocument1.DefaultPageSettings.PaperSize.Height - this.printDocument1.DefaultPageSettings.Margins.Top - this.printDocument1.DefaultPageSettings.Margins.Bottom;
            if (this.printDocument1.DefaultPageSettings.Landscape)
            {
                int tempwidth = yazdirma_alani.Width;
                yazdirma_alani.Width = yazdirma_alani.Height;
                yazdirma_alani.Height = tempwidth;
            }
            //yazdirma alaninin kenarındaki cizgilerin ozelliklerini ayarliyoruz
            Pen p = new Pen(Color.Pink);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            gr.DrawRectangle(p, yazdirma_alani);
            gr.DrawString(this.textBox3.Text,f, Brushes.Black, (yazdirma_alani.X + 10), (yazdirma_alani.Y + 20));
            sayfano++;
            if (sayfano < toplamsayfa)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
            PageSettings ayarlar = this.pageSetupDialog1.PageSettings;
            this.printDocument1.DefaultPageSettings = ayarlar;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.printDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
        }
    }
}

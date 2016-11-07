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
    public partial class anamenu : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source = .;Initial Catalog =odev;Integrated Security = True");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public anamenu()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = " HOŞGELDİNİZ "; 
            timer1.Enabled = true;
            timer2.Start();
            
        }

        private void tarifEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tarifekle frm3 = new tarifekle();
            frm3.Show();

        }

        private void tarifDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tarifduzenle frmx = new tarifduzenle();
            frmx.Show();
        }

        private void turEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turekle frm = new turekle();
            frm.Show();
        }

        private void tarifSilToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           tarifsil frmsil = new tarifsil();
           frmsil.Show();
        }

        private void turDuzenleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            turduzenle frmtr = new turduzenle();
            frmtr.Show();
        }

        private void turSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tursil frmsil = new tursil();
            frmsil.Show();
        }

        private void kullanıcıDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kullaniciduzenle kd = new kullaniciduzenle();
            kd.Show();
        }

        private void kullanıcıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kullanicisil ks = new kullanicisil();
            ks.Show();
        }
        private void tarifGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tarif frm4 = new tarif();
            frm4.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToShortDateString();
        }

        private void kullanıcıHesaplarıToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

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
    public partial class tarifekle : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.; initial Catalog=odev;integrated security=true");
        public tarifekle()
        {
            
            InitializeComponent();
            SqlCommand komut2 = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter("select * from  tarifturu order by tarifturuID", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "tarifturuID";
            comboBox1.DisplayMember = "tarifturu";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string tarifadi,malzemeler,hazirlanisi,tariftr;
            int ZorlukDerecesi,tarifturuID;
            tariftr = comboBox1.Text;
            tarifturuID = int.Parse(comboBox1.SelectedValue.ToString());
            tarifadi = txttrfadi.Text;
            malzemeler = txtmlzm.Text;
            hazirlanisi = txttrf.Text;
            ZorlukDerecesi = 0;
            if (radioButton1.Checked)
            {
                ZorlukDerecesi = 1;
            } else 
                if (radioButton2.Checked) 
                {
                    ZorlukDerecesi = 2;
                } else
                    if (radioButton3.Checked)
                    {
                        ZorlukDerecesi = 3;
                    } else
                        if (radioButton4.Checked)
                        {
                            ZorlukDerecesi = 4;
                        } else
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
            {
                
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand komut2 = new SqlCommand("insert into tarifler(tarifadi,malzemeler,hazirlanisi,tarifturu,ZorlukDerecesi,tarifturuID) values ('" + tarifadi + "','" + malzemeler + "','" + hazirlanisi + "','" + tariftr + "','" + ZorlukDerecesi.ToString() + "','"+tarifturuID.ToString()+"')", con);
                komut2.ExecuteNonQuery();
                label4.Text = "kaydedilmiştir";
                txtmlzm.Text = "";
                txttrf.Text = "";
                txttrfadi.Text = "";
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = true;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                label4.Text = "";
            }
          
            }
        }
    }

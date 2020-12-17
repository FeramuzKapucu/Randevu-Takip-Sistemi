using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Belediye_Proje
{
    public partial class FrmVatandasGiris : Form
    {
        public FrmVatandasGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();


        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmGiris giris = new FrmGiris();
            giris.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmVatandasKayit vatandasKayit = new FrmVatandasKayit();
            vatandasKayit.Show();
            this.Hide();
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            if (!(txtsifre.Text == "") && !(msktc.Text == ""))
            {
                SqlCommand komut = new SqlCommand("Select * from Tbl_Vatandaslar where VatandasTc=@v1 and VatandasSifre=@v2", bgl.baglanti());

                komut.Parameters.AddWithValue("@v1", msktc.Text);
                komut.Parameters.AddWithValue("@v2", txtsifre.Text);

                SqlDataReader dr = komut.ExecuteReader();
                if(dr.Read())
                {
                    FrmVatandasDetay vatandasDetay = new FrmVatandasDetay();
                    vatandasDetay.tc = msktc.Text;
                    vatandasDetay.Show();
                    this.Hide();
                }
                
                else
                {
                    MessageBox.Show("T.C no ya da şifre hatalı");
                    txtsifre.Clear();
                    msktc.Clear();
                    msktc.Focus();
                }
                bgl.baglanti().Close();
                
            }
            else
            {
                MessageBox.Show("Giriş yapmak için tüm alanları belirtildiği gibi ve eksiksiz doldurun");
                txtsifre.Clear();
                msktc.Clear();
                msktc.Focus();
            }
        }
    }
}

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
    public partial class FrmYoneticiGiris : Form
    {
        public FrmYoneticiGiris()
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

        private void btngiris_Click(object sender, EventArgs e)
        {
            if (!(txtsifre.Text=="") && !(msktc.Text==""))
            {
                SqlCommand komut = new SqlCommand("Select * from Tbl_Yönetici where ÇalışanTc=@y1 and ÇalışanSifre=@y2", bgl.baglanti());
                komut.Parameters.AddWithValue("@y1", msktc.Text);
                komut.Parameters.AddWithValue("@y2", txtsifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if(dr.Read())
                {
                    FrmYoneticiDetay yoneticiDetay = new FrmYoneticiDetay();
                    yoneticiDetay.tc = msktc.Text;
                    yoneticiDetay.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Hatalı Tc ya da Şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsifre.Clear();
                    msktc.Clear();
                    msktc.Focus();
                }

            }

            else
            {
                MessageBox.Show("Giriş Yapabilmeniz İçin Tüm Alanların Belirtildiği Gibi Doldurulması Gereklidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsifre.Clear();
                msktc.Clear();
                msktc.Focus();
            }
        }
    }
}

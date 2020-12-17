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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
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
            if (!(txtsifre.Text == "") && !(msktc.Text == ""))
            {
                SqlCommand komut = new SqlCommand("Select * from Tbl_Sekreter where SekreterTc=@s1 and SekreterSifre=@s2", bgl.baglanti());
                komut.Parameters.AddWithValue("@s1", msktc.Text);
                komut.Parameters.AddWithValue("@s2", txtsifre.Text);
                SqlDataReader reader = komut.ExecuteReader();
                if(reader.Read())
                {
                    FrmSekreterDetay sekreterDetay = new FrmSekreterDetay();
                    sekreterDetay.tc3 = msktc.Text;
                    sekreterDetay.Show();
                    this.Hide();

                }
              
                else
                {
                    MessageBox.Show("Hatalı TC ya da şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsifre.Clear();
                    msktc.Clear();
                    msktc.Focus();
                }
                bgl.baglanti().Close();

            }
            else
            {
                MessageBox.Show("Giriş yapmak için tüm alanları eksiksiz ve doğru birşekide doldurun lütfen", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                msktc.Clear();
                txtsifre.Clear();
                msktc.Focus();
            }

        }
    }
}

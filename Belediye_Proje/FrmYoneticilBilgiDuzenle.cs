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
    public partial class FrmYoneticilBilgiDuzenle : Form
    {
        public FrmYoneticilBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string ytc;

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmYoneticiDetay gorevliDetay = new FrmYoneticiDetay();
            gorevliDetay.tc = ytc;
            gorevliDetay.Show();
            this.Hide();
        }

        private void FrmYoneticilBilgiDuzenle_Load(object sender, EventArgs e)
        {
            msktc.Text = ytc;
            SqlCommand komut = new SqlCommand("Select ÇalışanAd,ÇalışanSoyad,ÇalışanSifre From Tbl_Yönetici where ÇalışanTc=@y1", bgl.baglanti());
            komut.Parameters.AddWithValue("@y1", ytc);
            SqlDataReader reader = komut.ExecuteReader();
            while(reader.Read())
            {
                txtisim.Text = reader[0].ToString();
                txtsoyisim.Text = reader[1].ToString();
                txtsifre.Text = reader[2].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (!(txtyenisifre.Text==""))
            {
                DialogResult result;
                result = MessageBox.Show("Şifre Günceleme İşleminiOnaylıyor Musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                try
                {
                    if (result == DialogResult.Yes)
                    {
                        SqlCommand komut = new SqlCommand("Update Tbl_Yönetici set ÇalışanSifre=@y1 where ÇalışanTc=@y2", bgl.baglanti());
                        komut.Parameters.AddWithValue("@y1", txtyenisifre.Text);
                        komut.Parameters.AddWithValue("@y2", msktc.Text);
                        komut.ExecuteNonQuery();
                        bgl.baglanti().Close();

                        MessageBox.Show("Şifreniz Yenilenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtyenisifre.Clear();
                        txtyenisifre.Focus();

                        SqlCommand komut2 = new SqlCommand("Select Çalışansifre from Tbl_Yönetici where ÇalışanTc=@p1", bgl.baglanti());
                        komut2.Parameters.AddWithValue("@p1", msktc.Text);
                        SqlDataReader dr = komut2.ExecuteReader();
                        while (dr.Read())
                        {
                            txtsifre.Text = dr[0].ToString();
                        }
                        bgl.baglanti().Close();


                    }
                    else
                    {
                        MessageBox.Show("Şifre Değiştirme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtyenisifre.Clear();
                        txtyenisifre.Focus();
                    }

                }
                catch (System.Data.SqlClient.SqlException)
                {

                    MessageBox.Show("Şifreniz 15 karakteri geçmemelidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtyenisifre.Clear();
                    txtyenisifre.Focus();
                }


            }
            else
            {
                MessageBox.Show("Şifrenizi Güncellemek İçin Lütfen Yeni Bir Şifre Girin","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtyenisifre.Clear();
                txtyenisifre.Focus();
            }
        }
    }
}

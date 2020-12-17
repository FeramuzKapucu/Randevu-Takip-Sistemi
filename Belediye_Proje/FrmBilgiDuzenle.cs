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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc2;

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmVatandasDetay vatandasDetay = new FrmVatandasDetay();
            vatandasDetay.tc = tc2;
            vatandasDetay.Show();
            this.Hide();
        }

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            msktc.Text = tc2;
            SqlCommand komut = new SqlCommand("Select VatandasAd,VatandasSoyad,VatandasTelefon,VatandasSifre,VatandasCinsiyet from TBl_Vatandaslar where VatandasTc=@v1", bgl.baglanti());
            komut.Parameters.AddWithValue("@v1",tc2);
            SqlDataReader reader = komut.ExecuteReader();

            while (reader.Read())
            {
                txtisim.Text = reader[0].ToString();
                txtsoyisim.Text = reader[1].ToString();
               msktlfn.Text = reader[2].ToString();
                txtsifre.Text = reader[3].ToString();
                cmcinsiyet.Text = reader[4].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        { // Alanlar boş değilse güncelleme için sor
            if(!(txtisim.Text=="")&& !(txtsoyisim.Text=="")&&!(msktc.Text=="")&&!(txtsifre.Text==""))
            {
                DialogResult result;
                result = MessageBox.Show("Bilgilerinizi Güncellemek İstediğinizde Emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result ==DialogResult.Yes) 
                {
                    // cevap evet ise güncelleme yap
                    SqlCommand komut = new SqlCommand("Update Tbl_Vatandaslar  Set VatandasAd=@v1,VatandasSoyad=@v2,VatandasTelefon=@v3,VatandasSifre=@v4,VatandasCinsiyet=@v5 where VatandasTc=@v6", bgl.baglanti());
                    komut.Parameters.AddWithValue("@v1", txtisim.Text);
                    komut.Parameters.AddWithValue("@v2", txtsoyisim.Text);
                    komut.Parameters.AddWithValue("@v3", msktlfn.Text);
                    komut.Parameters.AddWithValue("@v4", txtsifre.Text);
                    komut.Parameters.AddWithValue("@v5", cmcinsiyet.Text);
                    komut.Parameters.AddWithValue("@v6", msktc.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Bilgileriniz Güncellenmişitir.");
                    bgl.baglanti().Close();
                }
                else
                {  // değilse eski bilgileri geri getir
                    SqlCommand komut = new SqlCommand("Select VatandasAd,VatandasSoyad,VatandasTelefon,VatandasSifre,VatandasCinsiyet from TBl_Vatandaslar where VatandasTc=@v1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@v1", tc2);
                    SqlDataReader reader = komut.ExecuteReader();

                    while (reader.Read())
                    {
                        txtisim.Text = reader[0].ToString();
                        txtsoyisim.Text = reader[1].ToString();
                        msktlfn.Text = reader[2].ToString();
                        txtsifre.Text = reader[3].ToString();
                        cmcinsiyet.Text = reader[4].ToString();
                    }
                    bgl.baglanti().Close();
                

            }

            }
            else
            {
                MessageBox.Show("Bilgilerinizi güncellemek için tüm alnları doldurunuz lütfen", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

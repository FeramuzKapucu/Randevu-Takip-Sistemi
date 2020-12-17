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
    public partial class FrmVatandasKayit : Form
    {
        public FrmVatandasKayit()
        {
            InitializeComponent();
        }
        void temizle()
        {
            txtisim.Clear();
            txtsoyisim.Clear();
            txtsifre.Clear();
            msktc.Clear();
            msktlfn.Clear();

            txtisim.Focus();
        }
        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmVatandasGiris vatandasGiris = new FrmVatandasGiris();
            vatandasGiris.Show();
            this.Hide();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
           try
            {
                //Alnların boşluğunu kontrol eder.
                if (!(txtisim.Text == "") && !(txtsoyisim.Text == "") && !(msktc.Text == "") && !(msktlfn.Text == "") && !(txtsifre.Text == "") && !(cmcinsiyet.Text == ""))
                {
                    SqlCommand komut = new SqlCommand("insert into Tbl_Vatandaslar (VatandasAD,VatandasSoyad,VatandasTc,VatandasTelefon,VatandasSifre,VatandasCinsiyet) values (@v1,@v2,@v3,@v4,@v5,@v6)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@v1", txtisim.Text);
                    komut.Parameters.AddWithValue("@v2", txtsoyisim.Text);
                    komut.Parameters.AddWithValue("@v3", msktc.Text);
                    komut.Parameters.AddWithValue("@v4", msktlfn.Text);
                    komut.Parameters.AddWithValue("@v5", txtsifre.Text);
                    komut.Parameters.AddWithValue("@v6", cmcinsiyet.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Bir Yeni Kayı Eklendi.");
                    temizle();



                }
                else
                {
                    //Alnlardan biri noş ise

                    MessageBox.Show("Lütfen Tüm Alanları Belirtildiği Biçimde  Doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    temizle();

                }
            }
            // Girilen T. C numarası Sisteme kayılı ise üye olmayı engeller
             catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Girilen T.C numarası zaten sisteme kayıtlı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                temizle();
            }

        }
    }
}ee

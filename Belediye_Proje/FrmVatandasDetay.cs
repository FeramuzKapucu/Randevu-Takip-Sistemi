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
    public partial class FrmVatandasDetay : Form
    {
        public FrmVatandasDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc;
      

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmVatandasGiris vatandasGiris = new FrmVatandasGiris();
            vatandasGiris.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle bilgiDuzenle = new FrmBilgiDuzenle();
            bilgiDuzenle.tc2 = tc;
            bilgiDuzenle.Show();
            this.Hide();
        }

        private void FrmVatandasDetay_Load(object sender, EventArgs e)
        {
            // Form Yüklendiğinde isim ve Tc nin gelmesi
            lbltc.Text = tc;
            SqlCommand komut = new SqlCommand("Select VatandasAd,VatandasSoyad from Tbl_Vatandaslar where VatandasTC=@v1", bgl.baglanti());
            komut.Parameters.AddWithValue("@v1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblisimsoyisim.Text = dr[0] + " " + dr[1];
                
            }
            bgl.baglanti().Close();

            // Randevu Geçmişi 

            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Randevular where VatandasTc=" + tc, bgl.baglanti());
            adapter.Fill(data);
            dataGridView1.DataSource = data;


            // Birimlerin Combobox a yüklenmesi

            SqlCommand komut2 = new SqlCommand("Select Birimad from Tbl_Birimler ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbirim.Items.Add(dr2[0]);
                
            }

            bgl.baglanti().Close();


        }

         

        private void cmbbirim_SelectedValueChanged(object sender, EventArgs e)
        {
             // Seçilen birime göre yöneticileri combobox a çekme
            cmbgorevli.Items.Clear();
            cmbgorevli.Text = "";
            SqlCommand komut = new SqlCommand("Select ÇalışanAd,ÇalışanSoyad from Tbl_Yönetici where ÇalışanBirim=@y1", bgl.baglanti());
            komut.Parameters.AddWithValue("@y1", cmbbirim.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbgorevli.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();

        }

        private void cmbgorevli_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBirim='" + cmbbirim.Text+"'"+"and RandevuYönetici='"+cmbgorevli.Text+ "' and RandevuDurum=0", bgl.baglanti());
            adapter.Fill(data);
            dataGridView2.DataSource = data;

            SqlCommand komut = new SqlCommand("Select Randevuid from Tbl_Randevular where RandevuYönetici=@r1 and RandevuDurum='0'", bgl.baglanti());
            komut.Parameters.AddWithValue("@r1", cmbgorevli.Text);
            SqlDataReader rd = komut.ExecuteReader();
            while(rd.Read())
            {
                cmbid.Items.Add(rd[0]);
            }
        }

       

        private void btnrandevu_Click(object sender, EventArgs e)
        {
            if(!(cmbid.Text=="")&& !(rchkonu.Text==""))
            {
                try //200 karakter kontolu
                {
                    DialogResult result;
                    result = MessageBox.Show("Randevuyu almak istediğinizden emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    { 
                        // randevu alınmak isteniyorsa durum 1 yapılıyo konu giriliyp ve vatndas tcg iriliypor 
                        SqlCommand komut = new SqlCommand("Update Tbl_Randevular set RandevuDurum=1,VatandasTc=@r1,RandevuKonusu=@r2 where Randevuid=@r3", bgl.baglanti());
                        komut.Parameters.AddWithValue("@r1", tc);
                        komut.Parameters.AddWithValue("@r2", rchkonu.Text);
                        komut.Parameters.AddWithValue("@r3", cmbid.Text);
                        komut.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        MessageBox.Show("Randevu Alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Seçilen randevu  bir daha seçilemesin
                        /*
                        int secilen =Convert.ToInt16(cmbid.Text);
                        for (int i = 0; i < cmbid.Items.Count; i++)
                        {
                            if(Convert.ToInt16(cmbid.Items[i])==secilen)
                            {
                                cmbid.Items.Remove(cmbid.Items[i]);
                            }

                        }*/

                        // RANDEVU ALINDIKTAN SONR TABLOLARIN GÜNCELLENMESİ
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Randevular where VatandasTc=" + tc, bgl.baglanti());
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;

                        DataTable table2 = new DataTable();
                        SqlDataAdapter adapter2 = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBirim='" + cmbbirim.Text + "'" + "and RandevuYönetici='" + cmbgorevli.Text + "' and RandevuDurum=0", bgl.baglanti());
                        adapter2.Fill(table2);
                        dataGridView2.DataSource = table2;

                    }
                    else
                    {
                        MessageBox.Show("Randevu alma İşlemi iptal edildi.", "Bigi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rchkonu.Clear();
                    }

                }
                catch (System.Data.SqlClient.SqlException)
                {

                    MessageBox.Show("Lütfen konu bölümüne 200 karakterden fazla giriş yapmayın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rchkonu.Clear();
                }
            }
            else
            {
                MessageBox.Show("Lütfen randevu almak istedğiniz birim ve yöneticiyi seçniz ve bir randevu konusu giriniz.", "Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Information);
                rchkonu.Clear();
            }
        }
    }
}

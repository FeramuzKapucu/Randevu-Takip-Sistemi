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
    public partial class FrmYoneticiPaneli : Form
    {
        public FrmYoneticiPaneli()
        {
            InitializeComponent();
        }
        void temizle()
        {
            txtisim.Clear();
            txtsifre.Clear();
            txtsoyisim.Clear();
            msktc.Clear();
            txtisim.Focus();
        }
        public string tc4;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay sekreterDetay = new FrmSekreterDetay();
            sekreterDetay.tc3 = tc4;
            sekreterDetay.Show();
            this.Hide();
        }

        private void FrmYoneticiPaneli_Load(object sender, EventArgs e)
        {
            cmbbirim.Items.Clear();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select ÇalışanAd,ÇalışanSoyad,ÇalışanBirim,ÇalışanTc,ÇalışanSifre from Tbl_Yönetici order by ÇalışanBirim", bgl.baglanti());
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            SqlCommand komut = new SqlCommand("Select Birimad from Tbl_Birimler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                cmbbirim.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }

        
        // yÖNETİCİ pANELİNDEKİ HERHNGİ BİR VERİYE ÇİFT TIKLANDIĞI ZAMAN İLGİLİ SAIRDAKİ VERİLER EKRANA SIRALANIR
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtisim.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtsoyisim.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            msktc.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbbirim.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            if(!(txtisim.Text=="")&&!(txtsoyisim.Text=="")&&!(msktc.Text=="")&&!(cmbbirim.Text=="")&&!(txtsifre.Text==""))
            { // gİRİLEN aLNLAR BOŞ dEĞİLSE
                try
                {
                    DialogResult result;
                    result = MessageBox.Show("Yeni Bir Kayıt Oluşturmak İstediğinizden Emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    { // sORUTA CEVPA EVETSE
                        SqlCommand komut = new SqlCommand("insert into Tbl_Yönetici (ÇalışanAd,ÇalışanSoyad,ÇalışanBirim,ÇalışanTc,ÇalışanSifre) values (@y1,@y2,@y3,@y4,@y5)", bgl.baglanti());
                        komut.Parameters.AddWithValue("@y1", txtisim.Text);
                        komut.Parameters.AddWithValue("@y2", txtsoyisim.Text);
                        komut.Parameters.AddWithValue("@y3", cmbbirim.Text);
                        komut.Parameters.AddWithValue("@y4", msktc.Text);
                        komut.Parameters.AddWithValue("@y5", txtsifre.Text);
                        komut.ExecuteNonQuery();
                        temizle();
                        bgl.baglanti().Close();
                        MessageBox.Show("Yeni Yönetici Kaydı Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter("Select ÇalışanAd,ÇalışanSoyad,ÇalışanBirim,ÇalışanTc,ÇalışanSifre from Tbl_Yönetici order by ÇalışanBirim", bgl.baglanti());
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;



                    }
                    else
                    { // sORUYA CEVAP HAYIRSA
                        temizle();
                        MessageBox.Show("Kayıt Oluşturma İşlemi İpral Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                catch(System.Data.SqlClient.SqlException)
                {
                    // sİSTEME KAYITLI BİR TCT GİRİLMİŞSE
                    MessageBox.Show("Girilen TC zaten Sisteme Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    temizle();
                }

                
                

            }

            else
            {  // bOŞ ALANLAR VAR İSE
                MessageBox.Show("Yeni Bir Kayıt Yapmak İçin Tüm Alanları Eksiksiz Bir Biçimde Ve Belirtildiği Gibi Doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                temizle();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {

            if(!(txtisim.Text=="")&&!(txtsoyisim.Text=="")&&!(txtsifre.Text=="")&&!(msktc.Text=="")&&!(cmbbirim.Text==""))
            { // gİRİLEN aLNLAR bOŞ DEĞİLSE
                DialogResult result;
                result = MessageBox.Show("Silme İşlemini Onaylıyor musunuz", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result==DialogResult.Yes)
                { //sORUYA VERİLN CEVPA EVETSE
                    SqlCommand komut = new SqlCommand("delete  from Tbl_Yönetici where ÇalışanTc=@y1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@y1", msktc.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Seçilen Yönetici Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    temizle();

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select ÇalışanAd,ÇalışanSoyad,ÇalışanBirim,ÇalışanTc,ÇalışanSifre from Tbl_Yönetici order by ÇalışanBirim", bgl.baglanti());
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                else
                { // aLANLAR BOŞ DEĞİL AMA CEVAP HAYIRSA
                    MessageBox.Show("Silme İşlemi İptal Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    temizle();
                }

            }
            else
            {  // aLANLAR BOŞ İSE
                MessageBox.Show("Silme İşlemini Yapabilmek Tüm Alanları Doldurmanız Gerklidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (!(txtisim.Text=="")&&!(txtsifre.Text=="")&&!(txtsoyisim.Text=="")&&!(cmbbirim.Text=="")&&!(msktc.Text==""))
            { // aLNLAR bOŞ DEĞİLSE
                DialogResult result;
                result = MessageBox.Show("Seçili Kaydı Güncellemek İstedğinizde Emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result==DialogResult.Yes)
                {  // sORUYA CEVAP EVET İSE
                    SqlCommand komut = new SqlCommand("Update Tbl_Yönetici set ÇalışanAd=@y1,ÇalışanSoyad=@y2,ÇalışanBirim=@y3,ÇalışanSifre=@y4 where ÇalışanTc=@y5", bgl.baglanti());
                    komut.Parameters.AddWithValue("@y1", txtisim.Text);
                    komut.Parameters.AddWithValue("@y2", txtsoyisim.Text);
                    komut.Parameters.AddWithValue("@y3", cmbbirim.Text);
                    komut.Parameters.AddWithValue("@y4", txtsifre.Text);
                    komut.Parameters.AddWithValue("@y5", msktc.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Seçili Kayıt Güncellenmişti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // GÜNCELLEMEEDN SONRA YENİ DEĞERLER EKRNA 
                    SqlCommand komut2=new SqlCommand("Select ÇalışanAd, ÇalışanSoyad, ÇalışanBirim, ÇalışanSifre from Tbl_Yönetici where ÇalışanTc=@ç1", bgl.baglanti());
                    komut2.Parameters.AddWithValue("@ç1", msktc.Text);
                    SqlDataReader dr = komut2.ExecuteReader();
                    if(dr.Read())
                    {
                        txtisim.Text = dr[0].ToString();
                        txtsoyisim.Text = dr[1].ToString();
                        cmbbirim.Text = dr[2].ToString();
                        txtsifre.Text = dr[3].ToString();
                        


                    }
                    

                    bgl.baglanti().Close();

                    // GÜNCELLEMEEDN SONRA YENİ DEĞERLER tABLOYA
                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select ÇalışanAd,ÇalışanSoyad,ÇalışanBirim,ÇalışanTc,ÇalışanSifre from Tbl_Yönetici order by ÇalışanBirim", bgl.baglanti());
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;


                }

                else
                { 
                    // CEVAP HAYIRSA
                    MessageBox.Show("Güncelleme İşlemi iptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    temizle();
                }

            }

            else
            { //aLANLAR BOŞ İSE
                MessageBox.Show("Günceleme İşlemini Yapabilmek İçin Tüm Alanların Uygun Bir Şekilde Dolu Olması Gereklidir. ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }
    }
}

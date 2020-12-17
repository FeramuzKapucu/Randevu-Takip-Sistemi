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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc3;
      

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris sekreterGiris = new FrmSekreterGiris();
            sekreterGiris.Show();
            this.Hide();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            dtptarih.Format = DateTimePickerFormat.Custom;
            dtptarih.CustomFormat = "yyyy-MM-dd";
            dtptarih.MinDate = DateTime.Now;





            // AdSoyad Çekme
            lbltc.Text = tc3;
            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTc=@s1", bgl.baglanti());
            komut.Parameters.AddWithValue("@s1",tc3);
            SqlDataReader reader = komut.ExecuteReader();
            while(reader.Read())
            {
                lblisimsoyisim.Text = reader[0].ToString();
            }
            bgl.baglanti().Close();

            // Birimleri Çekme
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Birimler", bgl.baglanti());
            adapter.Fill(data);
            dataGridView1.DataSource = data;

            // Yöneticileri Çekme

            DataTable data2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter("Select ÇalışanAd,ÇalışanSoyad,ÇalışanBirim from Tbl_Yönetici order by ÇalışanBirim", bgl.baglanti());
            adapter2.Fill(data2);
            dataGridView2.DataSource = data2;

            // Birimleri Combobox a Aktarma
            SqlCommand komut2 = new SqlCommand("Select BirimAd from Tbl_Birimler", bgl.baglanti());
            SqlDataReader rd = komut2.ExecuteReader();
            while(rd.Read())
            {
                cmbbrirm.Items.Add(rd[0]);
            }


        }
        // Sisteme Yeni Bir randevu Tanımlamak İçin
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if(!(msksaat.Text=="")&&!(dtptarih.Text=="")&&!(cmbpersonel.Text=="")&&!(cmbbrirm.Text==""))
            {
                DialogResult result;
               result=MessageBox.Show("Randevuyu oluşturmak istediğinizden emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

              try
                {
                    if (result == DialogResult.Yes)
                    {
                        
                        SqlCommand komut = new SqlCommand("insert into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBirim,RandevuYönetici) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
                        komut.Parameters.AddWithValue("@r1", dtptarih.Text);
                        komut.Parameters.AddWithValue("@r2", msksaat.Text);
                        komut.Parameters.AddWithValue("@r3", cmbbrirm.Text);
                        komut.Parameters.AddWithValue("@r4", cmbpersonel.Text);
                        komut.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        MessageBox.Show("Bir yeni randevu oluşturuldu.");
                       
                        dtptarih.Text = DateTime.Now.ToString();
                        msksaat.Clear();


                    }
                    else
                    {
                       
                        
                        MessageBox.Show("Randevu iptal edildi.");
                        msksaat.Clear();
                    }

                }

                catch(System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Saat  için uygun değerler giriniz.(örn:Saat  23'den büyük, dakika 59 büyük  olamaz.)","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);


                    msksaat.Clear();
                }


            }
            else
            {
                MessageBox.Show("Randevu oluşturmak için Tarih,Saat,Birim ve Yönetici bölümlerini Doldurmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        // Yöneticileri Combobox a Aktarma
        private void cmbbrirm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbpersonel.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ÇalışanAd,ÇalışanSoyad  from Tbl_Yönetici where ÇalışanBirim=@y1", bgl.baglanti());
            komut.Parameters.AddWithValue("@y1", cmbbrirm.Text);
            SqlDataReader adapter = komut.ExecuteReader();
            while(adapter.Read())
            {
                cmbpersonel.Items.Add(adapter[0] + " " + adapter[1]);
            }
            bgl.baglanti().Close();

        }

        private void btnolustur_Click(object sender, EventArgs e)
        {
            if (!(rchduyuru.Text=="")) // DUYURU bOŞ dEĞİLSE
            {
                DialogResult result;
                result =MessageBox.Show("Bir yeni duyuru oluşturulsun mu?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result==DialogResult.Yes) //dUYURU OLUŞTURMA SORUSUNA CEVAP EVET İSE
                {
                    if(rchduyuru.Text.Length>200) //Karakter saysısı 200 den fazla ise uyarı verdir.
                    {
                        MessageBox.Show("Girilen karakter sayısı 200'ü geçmemelidir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rchduyuru.Clear();
                        rchduyuru.Focus();

                    }
                    else // kakater sayısı 200 den fazla değil ve cevap evetse kaydet
                    {
                        SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
                        komut.Parameters.AddWithValue("@d1", rchduyuru.Text);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Duyuru oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bgl.baglanti().Close();

                    }
                    

                }
                else // dUYURU OLUSLTURMA SORUSUNA CEVAP HAYIR İSE

                {
                    MessageBox.Show("Duyuru oluşturma işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rchduyuru.Clear();
                    rchduyuru.Focus();
                }


            }

            else //dUYURU bOŞ iSE
            {
                MessageBox.Show("Duyuru oluşturmak için duyuru panosunun içini doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            
        }

        private void btnbirim_Click(object sender, EventArgs e)
        {
            FrmBirim birim = new FrmBirim();
            birim.tc5 =lbltc.Text;
            birim.Show();
            this.Hide();
        }

        private void btnpersonel_Click(object sender, EventArgs e)
        {
            FrmYoneticiPaneli yoneticiPaneli = new FrmYoneticiPaneli();
            yoneticiPaneli.tc4 = lbltc.Text;
            yoneticiPaneli.Show();
            this.Hide();
        }

        private void btnrndvliste_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi listesi = new FrmRandevuListesi();
            listesi.tc6 = tc3;
            listesi.Show();
            this.Hide();
        }

        private void btnduyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular d1 = new FrmDuyurular();
            d1.tc7 = tc3;
            d1.Show();
            this.Hide();

        }

        private void btnrabdevusil_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class FrmBirim : Form
    {
        public FrmBirim()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc5;

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay sekreterDetay = new FrmSekreterDetay();
            sekreterDetay.tc3 = tc5;
            sekreterDetay.Show();
            this.Hide();
        }

        private void FrmBirim_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Birimler order by Birimid", bgl.baglanti());
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            if(!(txtbirimisim.Text==""))
            { // aLAN BOŞ DEĞİL İSE
                DialogResult result;
                result = MessageBox.Show("Yeni Birim Eklemek İstediğininzden Emin misiniz", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result==DialogResult.Yes)
                { // CEVAP EVET İSE

                    SqlCommand komut = new SqlCommand("insert into Tbl_Birimler (BirimAd) values (@b1)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@b1", txtbirimisim.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Yeni Birim Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // eKLEME YAPILDIKTAN SONRA TABLOYU GÜNCELLEME
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Birimler order by Birimid",bgl.baglanti());
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                     // sON EKLENİLEN BİRİMİ EKRANA YAZMA
                    SqlCommand komut2 = new SqlCommand("Select * from Tbl_Birimler where Birimad=@p1 ", bgl.baglanti());
                    komut2.Parameters.AddWithValue("@p1", txtbirimisim.Text);
                    SqlDataReader dr = komut2.ExecuteReader();
                    if(dr.Read())
                    {
                        txtid.Text = dr[0].ToString();
                        txtbirimisim.Text = dr[1].ToString();
                    }

                    


                }
                else
                { // CEVAP HAYIR İSE
                    MessageBox.Show("Yeni Birim Ekleme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtbirimisim.Clear();
                    txtbirimisim.Focus();
                }

            }
            else
            { // ALAN BOŞ İSE
                MessageBox.Show("Yeni Birim Eklemesi Yapmak İçin Birim Adı Bölümünü Dolduruuz.", "Uyar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbirimisim.Focus();
                
            }
        }

        //  tIKLANILAN sATIRIN eKRANA GELMESİ
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtbirimisim.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if(!(txtid.Text==""))
            {
                DialogResult result;
                result = MessageBox.Show("Birimi Silmek İstediğinizden Emin misiniz.", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result==DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete from Tbl_Birimler where Birimid=@b1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@b1", txtid.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Birim Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable data = new DataTable();
                    SqlDataAdapter reader = new SqlDataAdapter("Select * from Tbl_Birimler order by Birimid", bgl.baglanti());
                    reader.Fill(data);
                    dataGridView1.DataSource = data;
                    txtid.Clear();
                    txtbirimisim.Clear();


                }
                else
                {
                    MessageBox.Show("Silme İşlemi İptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Lütfen Silmek İstediğinin Birimin üstüne Çift Tıklayın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                txtbirimisim.Focus();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (!(txtid.Text==""))
            {
                DialogResult result;
                result = MessageBox.Show("Birimi Güncelleme İşleminden Emin misiniz", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result==DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Update Tbl_Birimler set Birimad=@b1 where Birimid=@b2", bgl.baglanti());
                    komut.Parameters.AddWithValue("@b1", txtbirimisim.Text);
                    komut.Parameters.AddWithValue("@b2", txtid.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Birim İsmi Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Birimler order by Birimid", bgl.baglanti());
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    SqlCommand komut2 = new SqlCommand("Select * from Tbl_Birimler  where Birimid=@p1", bgl.baglanti());
                    komut2.Parameters.AddWithValue("@p1", txtid.Text);
                    SqlDataReader dr = komut2.ExecuteReader();
                    if(dr.Read())
                    {
                        txtid.Text = dr[0].ToString();
                        txtbirimisim.Text = dr[1].ToString();
                    }



                }

                else
                {
                    MessageBox.Show("Güncelleme İşlemi İptal Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Lütfen Güncelleme Yapılcak Birime Çift Tıklayın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

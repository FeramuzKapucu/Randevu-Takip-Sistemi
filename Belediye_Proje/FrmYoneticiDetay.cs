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
    public partial class FrmYoneticiDetay : Form
    {
        public FrmYoneticiDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc;

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmYoneticiGiris gorevlirGiris = new FrmYoneticiGiris();
            gorevlirGiris.Show();
            this.Hide();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            FrmYoneticilBilgiDuzenle bilgiDuzenle = new FrmYoneticilBilgiDuzenle();
            bilgiDuzenle.ytc = tc;
            bilgiDuzenle.Show();
            this.Hide();
        }

        private void FrmYoneticiDetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;
            // Yönetici Ad soyad çekme
            SqlCommand komut = new SqlCommand("Select ÇalışanAd,ÇalışanSoyad from Tbl_Yönetici where ÇalışanTc=@y1", bgl.baglanti());
            komut.Parameters.AddWithValue("@y1", tc);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                lblisimsoyisim.Text = reader[0] + " " + reader[1];
            }
            bgl.baglanti().Close();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuYönetici='" + lblisimsoyisim.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnduyuru_Click(object sender, EventArgs e)
        {
            FrmYönetiCiDuyuru yönetiCiDuyuru = new FrmYönetiCiDuyuru();
            yönetiCiDuyuru.dytc = tc;
            yönetiCiDuyuru.Show();
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchkonu.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

        }
    }
}

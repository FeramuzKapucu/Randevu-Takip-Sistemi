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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }
        public string tc6;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay sekreterDetay = new FrmSekreterDetay();
            sekreterDetay.tc3 = tc6;
            sekreterDetay.Show();
            this.Hide();
        }

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Tbl_Randevular", bgl.baglanti());
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        
    }
}

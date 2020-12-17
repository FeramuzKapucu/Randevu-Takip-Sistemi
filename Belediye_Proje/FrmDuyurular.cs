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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }
        public string tc7;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay sekreterDetay = new FrmSekreterDetay();
            sekreterDetay.tc3 = tc7;
            sekreterDetay.Show();
            this.Hide();
        }

        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Duyurular ", bgl.baglanti());
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}

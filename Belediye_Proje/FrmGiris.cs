using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Belediye_Proje
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnvtandas_Click(object sender, EventArgs e)
        {
            FrmVatandasGiris vatandasGiris = new FrmVatandasGiris();
            vatandasGiris.Show();
            this.Hide();
            
        }

        private void btnyonetici_Click(object sender, EventArgs e)
        {
            FrmYoneticiGiris yoneticiGiris = new FrmYoneticiGiris();
            yoneticiGiris.Show();
            this.Hide();
        }

        private void btnasista_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris sekreterGiris = new FrmSekreterGiris();
            sekreterGiris.Show();
            this.Hide();
        }
    }
}

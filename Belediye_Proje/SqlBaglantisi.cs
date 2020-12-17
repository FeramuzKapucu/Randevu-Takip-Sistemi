using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Belediye_Proje
{
    class SqlBaglantisi
    {
        public SqlConnection baglanti()

        {
            string adres = System.IO.File.ReadAllText(@"C:\belediye_veri _tabanı.txt");
            SqlConnection baglan = new SqlConnection(adres);
            baglan.Open();
            return baglan;


           
        }
    }
}

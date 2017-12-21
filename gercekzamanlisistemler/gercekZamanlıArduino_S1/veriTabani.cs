using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace gercekZamanlıArduino_S1
{
    class veriTabani
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["MySQL"].ToString();
        public MySqlConnection baglan = new MySqlConnection(ConnStr);
        public string baglanti_kontrol()
        {
            try
            {
                baglan.Open();
                return "true";
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace gercekZamanlıArduino_S1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        veriTabani veriBaglan = new veriTabani();
        int i;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = veriBaglan.baglan.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from admin where kadi='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                label1.Text = "Veri Eşleşmedi";
            }
            else
            {
                this.Hide();
                Form2 fm = new Form2();
                fm.Show();
            }
            veriBaglan.baglan.Close();
        }
    }
}

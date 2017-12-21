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
using System.IO;
using System.IO.Ports;

namespace gercekZamanlıArduino_S1
{
    public partial class Form2 : Form
    {
        string[] port = SerialPort.GetPortNames();
        public Form2()
        {
            InitializeComponent();
        }
        veriTabani vb = new veriTabani();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        private void Form2_Load(object sender, EventArgs e)
        {
            vb.baglan.Open();
            serialPort1.Close();
            label7.Text = "Bağlantı Kapalı";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            if (serialPort1.IsOpen == false)
            {
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 9600;
                try
                {
                    serialPort1.Open();
                    label7.Text = "Bağlantı Açık";
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Buton Hata: " + hata.Message);

                }
            }
            else
            {
                label7.Text = "Bağlantı Zaten Kuruldu";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sonuc = serialPort1.ReadLine();
                string[] veriler = sonuc.Split('/');
                textBox1.Text = veriler[0];
                textBox2.Text = veriler[1];
                textBox3.Text = veriler[2];
               MySqlCommand komutEkle = new MySqlCommand("insert into mesafe(id,mesafe,tarih) values (null,'" + veriler[0] + "','" + DateTime.Now.ToString() + "')", vb.baglan);
               komutEkle.ExecuteNonQuery();
               MySqlCommand komutEkle2 = new MySqlCommand("insert into titresim(id,titresim,tarih) values (null,'" + veriler[1] + "','" + DateTime.Now.ToString() + "')", vb.baglan);
               komutEkle2.ExecuteNonQuery();
               MySqlCommand komutEkle3 = new MySqlCommand("insert into ses(id,ses,tarih) values (null,'" + veriler[2] + "','" + DateTime.Now.ToString() + "')", vb.baglan);
               komutEkle3.ExecuteNonQuery();

                string iste1 = "select * from mesafe";
                MySqlDataAdapter adapter = new MySqlDataAdapter(iste1, vb.baglan);
                adapter.Fill(table1);
                dataGridView1.DataSource = table1;
                string iste2 = "select * from titresim";
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(iste2, vb.baglan);
                adapter2.Fill(table2);
                dataGridView2.DataSource = table2;
                string iste3 = "select * from ses";
                MySqlDataAdapter adapter3 = new MySqlDataAdapter(iste3, vb.baglan);
                adapter3.Fill(table3);
                dataGridView3.DataSource = table3;

                serialPort1.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show("Timer " + ex.Message);


            }
         }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
            {
                if (serialPort1.IsOpen == true)
                {
                    serialPort1.Close();
                }
            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    } 

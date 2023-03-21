using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace kurs
{
    public partial class visel : Form
    {
        public visel()
        {
            InitializeComponent();
            uchet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string si1 = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            int i = Convert.ToInt32(si1);
            string sql = $"UPDATE uchet SET [Дата выселения] = '{dateTimePicker1.Value.ToShortDateString()}'   WHERE [Код отчета]={i}";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Успешно");
            comboBox1.Items.Clear();
            int ok = 0;
            sql = $"Select [Номер комнаты] from uchet WHERE [Код отчета]={i}";
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ok = reader.GetInt32(0);
                    }
                }
            }
            sql = $"UPDATE nomer SET [Занята ли комната] = '0'   WHERE [Номер комнаты]={ok}";
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            uchet();

        }
        private void uchet()
        {
            string sql = "Select [Код отчета],[Код клиента],[Номер комнаты],[Дата заселения],[Код работника] from uchet WHERE [Дата выселения]='01.01.1900'";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "    Код клиента:" + reader.GetValue(1).ToString() + "  Номер комнаты:" +
                            reader.GetValue(2).ToString() + "  Дата заселения: " +reader.GetDateTime(3).ToShortDateString() + "  Код работника: " +reader.GetValue(4).ToString();
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }

        private void visel_FormClosing(object sender, FormClosingEventArgs e)
        {
            main MainForm = new main();
            MainForm.Show();
        }
    }
}

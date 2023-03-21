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
    public partial class main : Form
    {
        public static SqlConnection conn;
        public static string connString = "Data Source=.;Initial Catalog=klient;User ID=sa;Password=sasa";
        public main()
        {
            InitializeComponent();
            conn = new SqlConnection(connString);
            try  {   conn.Open(); }
            catch (Exception el)  {MessageBox.Show("Error: " + el.Message);  }
            fullklient(); fullnomer(); fullpersonal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sql = "SELECT MAX([Код отчета]) FROM uchet";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i = Convert.ToInt32(reader.GetValue(0).ToString())+1;
                    }
                }
            }
            string si1 = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            string si2 = comboBox2.Text[0].ToString() + comboBox2.Text[1].ToString() + comboBox2.Text[2].ToString();
            string si3 = comboBox3.Text[0].ToString() + comboBox3.Text[1].ToString() + comboBox3.Text[2].ToString();
            if (comboBox1.Items!=null & comboBox2.Items!=null & comboBox3.Items!=null)
            {
                sql = "INSERT INTO [uchet] VALUES (" + i + ", '" + Convert.ToInt32(si1) + "','" + Convert.ToInt32(si2) 
                    + "','" + dateTimePicker1.Value.ToShortDateString() + "', '" + null + "', '" + Convert.ToInt32(si3) + "')";
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = $"UPDATE nomer SET [Занята ли комната] = '1'   WHERE [Номер комнаты]={Convert.ToInt32(si2)}";
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Учет добавлен");
            }
            else
            {
                MessageBox.Show("Введите во все поля данные");
            }
            comboBox1.Items.Clear();comboBox2.Items.Clear();comboBox3.Items.Clear();
            fullklient(); fullnomer(); fullpersonal();
        } 
        private void fullklient()
        {
            comboBox1.Items.Clear();
            string sql = $"Select [Код клиенты],Фамилия, Имя, Отчество, Телефон from klient";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "       " + reader.GetString(1)+ 
                            reader.GetString(2)+reader.GetString(3)+ reader.GetString(4);
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }
        private void fullnomer()
        {
            comboBox2.Items.Clear();
            string sql = "Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]=0";
            if (numericUpDown1.Value!=0)
            { sql = $"Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]=0 AND [Код категории]='{numericUpDown1.Value}'"; }
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "       Комната    Категория: " + reader.GetValue(1).ToString() +
                          " Стоимость: " + reader.GetValue(2).ToString();
                        comboBox2.Items.Add(sa1);
                    }
                }
            }
        }
        private void fullpersonal()
        {
            comboBox3.Items.Clear();
            string sql = "Select [Код персонала],Фамилия,Имя,Отчетсво from personal";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "       " + reader.GetValue(1).ToString() +
                          "   " + reader.GetValue(2).ToString() + "   " + reader.GetValue(3).ToString();
                        comboBox3.Items.Add(sa1);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            klient klien = new klient();
            klien.Show(); this.Hide();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            { conn.Close(); }
            catch (Exception el)
            { MessageBox.Show("Error: " + el.Message);}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            delete delet = new delete();
            delet.Show(); this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            personal pers = new personal();
            pers.Show(); this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nomer nome = new nomer();
            nome.Show(); this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            visel vise = new visel();
            vise.Show(); this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            kategoria kateg = new kategoria();
            kateg.Show(); this.Hide();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            fullnomer();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            otchet ot = new otchet();
            ot.Show(); this.Hide();
        }
    }
}

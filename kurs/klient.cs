using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace kurs
{
    public partial class klient : Form
    {
        public klient()
        {
            InitializeComponent();
            fullklient();
        }

        private void klient_FormClosing(object sender, FormClosingEventArgs e)
        {
            main MainForm = new main();
            MainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sql = "SELECT MAX([Код клиенты]) FROM klient";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
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
            if (textBox1.Text != null & textBox2.Text != null & textBox3.Text != null & textBox4.Text != null)
            {
                sql = "INSERT INTO [klient] VALUES (" + i + ", '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "', '" + textBox4.Text + "')";
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Клиент добавлен");
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                comboBox1.Items.Clear();
                fullklient();
            }
            else
            {
                MessageBox.Show("Введите во все поля данные");
            }
        }

        private void fullklient()
        {
            string sql = "Select [Код клиенты],Фамилия, Имя, Отчество, Телефон from klient";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "   " + reader.GetString(1) +
                            reader.GetString(2) + reader.GetString(3) + reader.GetString(4);
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string si = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            string sql = $"Select [Код клиенты],Фамилия,Имя,Отчество,Телефон from klient WHERE [Код клиенты]={Convert.ToInt32(si)}";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetValue(1).ToString();
                        textBox2.Text = reader.GetValue(2).ToString();
                        textBox3.Text = reader.GetValue(3).ToString();
                        textBox4.Text = reader.GetValue(4).ToString();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string si = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            string sql = $"UPDATE klient SET Фамилия = '" + textBox1.Text + $"', Имя='" + textBox2.Text + $"', Отчество='"
                + textBox3.Text + $"', Телефон='" + textBox4.Text + $"' WHERE [Код клиенты]={Convert.ToInt32(si)}";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Успешно");
            textBox1.Clear();textBox2.Clear();textBox3.Clear();textBox4.Clear();
            comboBox1.Items.Clear();
            fullklient();
        }
    }
}

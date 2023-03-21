using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace kurs
{
    public partial class nomer : Form
    {
        public nomer()
        {
            InitializeComponent();
            fullnomer();katego();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sql = "SELECT MAX([Номер комнаты]) FROM nomer";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                    }
                }
            }
            if (comboBox2.Text!=null & numericUpDown1.Value!=0)
            {
                string si1 = comboBox2.Text[0].ToString() + comboBox2.Text[1].ToString() + comboBox2.Text[2].ToString();
                sql = "INSERT INTO [nomer] VALUES (" + i + ", '" + Convert.ToInt32(si1) + "','" + Convert.ToInt32(numericUpDown1.Value) + "','" + 0 + "')";
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Номер добавлен");
                comboBox2.Text = "";
                numericUpDown1.Value = 0;
                comboBox1.Items.Clear();
                fullnomer();
            }
            else
            {
                MessageBox.Show("Введите во все поля данные");
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void fullnomer()
        {
            comboBox1.Items.Clear();
            string sql = "Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]=0";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "       Комната,    Категория:  " + reader.GetValue(1).ToString() +
                          ",    Стоимость: " + reader.GetValue(2).ToString();
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }

        private void katego()
        {
            comboBox2.Items.Clear();
            string sql = "Select [Код категории],[Название категории] from kategoria";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "     Название категории:  " + reader.GetValue(1).ToString();
                        comboBox2.Items.Add(sa1);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string si1 = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            string sql = $"Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]='0' AND [Номер комнаты]='{Convert.ToInt32(si1)}'";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox2.Text = reader.GetValue(1).ToString();
                        numericUpDown1.Value = reader.GetInt32(2);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void nomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            main MainForm = new main();
            MainForm.Show();
        }
    }
}

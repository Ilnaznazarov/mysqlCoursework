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
    public partial class personal : Form
    {
        public personal()
        {
            InitializeComponent();
            fullpersonal();
        }
        private void personal_FormClosing(object sender, FormClosingEventArgs e)
        {
            main MainForm = new main();
            MainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sql = "SELECT MAX([Код персонала]) FROM personal";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i = reader.GetInt32(0)+1;
                    }
                }
            }
            i++;
            if (textBox1.Text != null & textBox2.Text != null & textBox3.Text != null & numericUpDown1.Value.ToString() != null)
            {
                sql = "INSERT INTO [personal] VALUES (" + Convert.ToInt32(i) + ", '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "', '" + numericUpDown1.Value + "')";
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Добавил");
            }
            else
            {
                MessageBox.Show("Введите во все поля данные");
            }
            fullpersonal();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string si = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            string sql = $"UPDATE personal SET Фамилия = '"+ textBox1.Text + $"', Имя='" + textBox2.Text + $"', Отчетсво='"
                + textBox3.Text + $"', Зарплата='" + Convert.ToInt32(numericUpDown1.Value) + $"' WHERE [Код персонала]={Convert.ToInt32(si)}";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        private void fullpersonal()
        {
            comboBox1.Items.Clear();
            string sql = "Select [Код персонала],Фамилия,Имя,Отчетсво from personal";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "        " + reader.GetValue(1).ToString() +
                          "   " + reader.GetValue(2).ToString() + "   " + reader.GetValue(3).ToString();
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string si = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            string sql = $"Select [Код персонала],Фамилия,Имя,Отчетсво,Зарплата from personal WHERE [Код персонала]={Convert.ToInt32(si)}";
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
                        numericUpDown1.Value= reader.GetInt32(4);
                    }
                }
            }
        }
    }
}

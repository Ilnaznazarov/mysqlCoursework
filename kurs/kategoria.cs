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
    public partial class kategoria : Form
    {
        public kategoria()
        {
            InitializeComponent();
            katego();
        }

        private void kategoria_FormClosing(object sender, FormClosingEventArgs e)
        {
            main MainForm = new main();
            MainForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void katego()
        {
            comboBox1.Items.Clear();
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
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sql = "SELECT MAX([Код категории]) FROM kategoria";
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
            if (textBox1.Text!=null)
            {
                sql = "INSERT INTO [kategoria] VALUES (" + i + ", '" + textBox1.Text + "')";
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Категория добавлена");
                textBox1.Clear(); 
                comboBox1.Items.Clear();
                katego();
            }
            else
            {
                MessageBox.Show("Введите во все поля данные");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

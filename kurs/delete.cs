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

namespace kurs
{
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
            katego();uchet();personal();nomer();klient();
        }

        private void klient()
        {
            comboBox1.Items.Clear();
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
                        string sa1 = reader.GetValue(0).ToString() + "      " + reader.GetString(1) +
                            reader.GetString(2) + reader.GetString(3) + reader.GetString(4);
                        comboBox1.Items.Add(sa1);
                    }
                }
            }
        }
        private void nomer()
        {
            comboBox4.Items.Clear();
            string sql = "Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]=0";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "      " + reader.GetValue(1).ToString() +
                          "   " + reader.GetValue(2).ToString();
                        comboBox4.Items.Add(sa1);
                    }
                }
            }
        }
        private void personal()
        {
            comboBox2.Items.Clear();
            string sql = "Select [Код персонала],Фамилия,Имя,Отчетсво from personal";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "      " + reader.GetValue(1).ToString() +
                          "   " + reader.GetValue(2).ToString() + "  " + reader.GetValue(3).ToString();
                        comboBox2.Items.Add(sa1);
                    }
                }
            }
        }

        private void katego()
        {
            comboBox5.Items.Clear();
            string sql = "Select [Код категории],[Название категории] from kategoria";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sa1 = reader.GetValue(0).ToString() + "      " + reader.GetValue(1).ToString();
                        comboBox5.Items.Add(sa1);
                    }
                }
            }
        }
        private void uchet()
        {
            comboBox3.Items.Clear();
            string sql = "Select [Код отчета],[Код клиента],[Номер комнаты],[Дата заселения],[Дата выселения],[Код работника] from uchet";
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string sa1 = reader.GetValue(0).ToString() + "      Код клиента:" + reader.GetValue(1).ToString() +
                          " Номер комнаты:" + reader.GetValue(2).ToString() + " Дата заселения" + reader.GetDateTime(3).ToShortDateString() + " Дата выселения" + reader.GetDateTime(4).ToShortDateString() +
                          "   " + reader.GetValue(5).ToString(); ;
                        comboBox3.Items.Add(sa1);
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string si1 = comboBox1.Text[0].ToString() + comboBox1.Text[1].ToString() + comboBox1.Text[2].ToString();
            DialogResult result = MessageBox.Show($"Удалить клиента {comboBox1.Text}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = $"Delete from klient where [Код клиенты]={Convert.ToInt32(si1)}";
                SqlCommand cmd = main.conn.CreateCommand();
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Процесс завершен!", "TsManager"); // Выводим сообщение о звершении.
            }
            klient();
        }

        private void delete_FormClosing(object sender, FormClosingEventArgs e)
        {
            main MainForm = new main();
            MainForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string si1 = comboBox2.Text[0].ToString() + comboBox2.Text[1].ToString() + comboBox2.Text[2].ToString();
            DialogResult result = MessageBox.Show($"Удалить персонал {comboBox2.Text}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = $"Delete from personal where [Код персонала]={Convert.ToInt32(si1)}";
                SqlCommand cmd = main.conn.CreateCommand();
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Процесс завершен!", "TsManager"); // Выводим сообщение о звершении.
            }
            personal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string si1 = comboBox3.Text[0].ToString() + comboBox3.Text[1].ToString() + comboBox3.Text[2].ToString();
            DialogResult result = MessageBox.Show($"Удалить учет {comboBox3.Text}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = $"Delete from uchet where [Код отчета]={Convert.ToInt32(si1)}";
                SqlCommand cmd = main.conn.CreateCommand();
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Процесс завершен!", "TsManager"); // Выводим сообщение о звершении.
            }
            uchet();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string si1 = comboBox4.Text[0].ToString() + comboBox4.Text[1].ToString() + comboBox4.Text[2].ToString();
            DialogResult result = MessageBox.Show($"Удалить номер {comboBox4.Text}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = $"Delete from nomer where [Номер комнаты]={Convert.ToInt32(si1)}";
                SqlCommand cmd = main.conn.CreateCommand();
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Процесс завершен!", "TsManager"); // Выводим сообщение о звершении.
            }
            nomer();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string si1 = comboBox5.Text[0].ToString() + comboBox5.Text[1].ToString() + comboBox5.Text[2].ToString();
            DialogResult result = MessageBox.Show($"Удалить категорию номера {comboBox5.Text}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = $"Delete from kategoria where [Код категории]={Convert.ToInt32(si1)}";
                SqlCommand cmd = main.conn.CreateCommand();
                cmd = main.conn.CreateCommand();
                cmd.Connection = main.conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Процесс завершен!", "TsManager"); // Выводим сообщение о звершении.
            }
            katego();
        }
    }
}

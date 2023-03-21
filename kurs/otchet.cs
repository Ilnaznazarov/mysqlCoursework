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
    public partial class otchet : Form
    {
        public otchet()
        {
            InitializeComponent();
            fullklient(); fullnomer();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void fullklient()
        {
            int i = 0;
            string sql = $"Select [Код клиенты],Фамилия, Имя, Отчество, Телефон from klient";
            SqlCommand cmd =  main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0).ToString();
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetString(1);
                        dataGridView1.Rows[i].Cells[2].Value = reader.GetString(2);
                        dataGridView1.Rows[i].Cells[3].Value = reader.GetString(3);
                        dataGridView1.Rows[i].Cells[4].Value = reader.GetString(4);
                        i++;
                    }
                }
            }
        }
        private void fullnomer()
        {
            dataGridView2.Rows.Clear();
            string sql = "Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]=0"; int i = 0;
            if (radioButton1.Checked)
            { sql = "Select [Номер комнаты],[Код категории],Стоимость from nomer WHERE [Занята ли комната]=1"; }
            SqlCommand cmd = main.conn.CreateCommand();
            cmd.Connection = main.conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = reader.GetValue(0).ToString();
                        dataGridView2.Rows[i].Cells[1].Value = reader.GetValue(1).ToString();
                        dataGridView2.Rows[i].Cells[2].Value = reader.GetValue(2).ToString();
                        i++;
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fullnomer();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fullnomer();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

﻿using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace المخزن
{
    public partial class Indebtedness_Hotel : Form
    {
        public Indebtedness_Hotel()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mostafa\source\repos\المخزن\المخزن\Database1.mdf;Integrated Security=True");

        private void Indebtedness_Hotel_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            Con.Open();
            String query = "insert into HotelTbl values(N'" + dateTimePicker1.Value.ToString() + "','" + daf3.Text + "','" + dayen.Text + "','" + daf23Lbl.Text + "')";
            SqlCommand cmd = new SqlCommand("INSERT INTO HotelTbl (اليوم,الدين,الدفع,اللى_دفع) VALUES " +
                "(@اليوم,@الدين,@الدفع,@اللى_دفع)", Con);
            cmd.Parameters.AddWithValue("@اليوم", dateTimePicker1.Value.ToString());
            cmd.Parameters.AddWithValue("@الدين", dayen.Text);
            cmd.Parameters.AddWithValue("@الدفع", daf3.Text);
            cmd.Parameters.AddWithValue("@اللى_دفع", daf23Lbl.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Item successfully Added");

            Con.Close();
            populate();



        }
        private void populate()
        {
            Con.Open();
            String query = "select * from HotelTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder bulider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DGVPrinter dgvprinter = new DGVPrinter();
            dgvprinter.Title = " جدول مديونيه المطبخ";
            dgvprinter.SubTitle = string.Format(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
            dgvprinter.PorportionalColumns = true;
            dgvprinter.Footer = label15.Text;
            dgvprinter.FooterSpacing = 20;
            dgvprinter.PageNumbers = true;
            dgvprinter.PrintDataGridView(dataGridView1);
            populate();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            double totin = 0.0;
            double totout = 0.0;
            double profit = 0.0;
            string TotalProfit = "";



            if (dataGridView1.Rows[0].Cells[1].Value == "Null")
            {
                label11.Text = totin.ToString();

            }

            else if (dataGridView1.Rows[0].Cells[2].Value == "Null")
            {
                label13.Text = totout.ToString();
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    totin += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    totout += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                }
                label11.Text = totin.ToString();
                label13.Text = totout.ToString();
            }
            profit = Convert.ToDouble(label11.Text) - Convert.ToDouble(label13.Text);
            TotalProfit = profit.ToString();
            label15.Text = TotalProfit.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Indebtedness indebtedness =new Indebtedness();
            indebtedness.Show();
            this.Hide();
        }
    }
}

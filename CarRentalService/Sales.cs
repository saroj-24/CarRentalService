using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarRentalService
{
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable("rent");
                SqlCommand cmd = new SqlCommand("select r.regid,c.custname,r.rentdate,r.returndate,r.fee from rent r inner join customer c on r.custid = c.custid where r.rentdate between @fromdate and @todate", con);
                cmd.Parameters.AddWithValue("@fromdate", fromdateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@todate", todateTimePicker2.Value);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                totalsale();
                

            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void totalsale() // find the total sale of car
        {
            try
            {
                string query = "SELECT SUM(fee) FROM rent";
                SqlCommand cmd = new SqlCommand(query, con);
               // SqlDataReader reader = cmd.ExecuteReader();
                totaltxtlabel.Text = cmd.ExecuteScalar().ToString();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

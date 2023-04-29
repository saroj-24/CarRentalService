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
    public partial class payment : Form
    {
        public payment()
        {
            InitializeComponent();
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");

        private void fillcustomer()
        {
            try
            {
                con.Open();
                string query = "select custid from customer";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader datareader;
                datareader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("custid", typeof(string));
                dt.Load(datareader);
                custcomboBox2.ValueMember = "custid";
                custcomboBox2.DataSource = dt;
                con.Close();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void paybutton1_Click(object sender, EventArgs e)
        {
            try
            {
                if(custcomboBox2.Text=="" || custtextBox2.Text==""||addresstextBox1.Text==""||statuscomboBox1.Text=="")
                {
                    MessageBox.Show("please fill up all the infromation box");
                }
                else if(paytextBox3.Text=="")
                {
                    MessageBox.Show("Please enter payment cash");
                }
                else {
                    con.Open();
                    string query = "insert into payment(custid,custname,address,pay,status)values(@cid,@cusname,@add,@py,@st)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", custcomboBox2.Text);
                    cmd.Parameters.AddWithValue("@cusname", custtextBox2.Text);
                    cmd.Parameters.AddWithValue("@add", addresstextBox1.Text);
                    cmd.Parameters.AddWithValue("@py", paytextBox3.Text);
                    cmd.Parameters.AddWithValue("@st", statuscomboBox1.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Payment Done Successfully thank you");
                }
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void payment_Load(object sender, EventArgs e)
        {
            fillcustomer();
        }
        private void fillname()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM customer WHERE custid = @custid", con);
                cmd.Parameters.AddWithValue("@custid", custcomboBox2.SelectedValue.ToString());
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string name = (string)rd["custname"].ToString();
                    custtextBox2.Text = name;
                }
                con.Close();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }
        private void address()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("select * from customer where custid = @custid", con);
                cmd.Parameters.AddWithValue("@custid", custcomboBox2.SelectedValue.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string name = (string)rd["address"].ToString();
                    addresstextBox1.Text = name;
                }

                con.Close();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }
        private void custcomboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillname();
            address();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customeruserinterfacae c = new customeruserinterfacae();
            c.Show();
            this.Hide();
       }
    }
}

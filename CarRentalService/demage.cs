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
    public partial class demage : Form
    {
        public demage()
        {
            InitializeComponent();
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            customeruserinterfacae c = new customeruserinterfacae();
            c.Show();
            this.Hide();
        }
        private void fillCombo()// this function will only show available car
        {
            con.Open();
            string query = "select Regno from CarTable where available='" + "NO" + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader datareader;
            datareader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Regno", typeof(string));
            dt.Load(datareader);
            regcomboBox1.ValueMember = "Regno";
            regcomboBox1.DataSource = dt;
            con.Close();
        }
        private void fillcustomer()
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
        private void fillname()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from customer where custid = @custid", con);
                cmd.Parameters.AddWithValue("@custid", custcomboBox2.SelectedValue);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string name = rd["custname"].ToString();
                    custtextbox.Text = name;
                }
                con.Close();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }
        private void subitbutton1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void custcomboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillname();
        }

        private void demage_Load(object sender, EventArgs e)
        {
            fillCombo();
            fillcustomer();
        }
        private  void reset()
        {
            custcomboBox2.Text = "";
            custtextbox.Text = "";
            descpboxtextBox1.Text = "";
            finetextBox2.Text = "";
            paymenttextBox3.Text = "";
            regcomboBox1.Text = "";
        }
        private void paybutton2_Click(object sender, EventArgs e)
        {
            try
            {

                if (regcomboBox1.Text == "" || custcomboBox2.Text == "" || custtextbox.Text == "" || descpboxtextBox1.Text == "")
                {
                    MessageBox.Show("Please fill the given information!");
                }
                else if (finetextBox2.Text == "")
                {
                    MessageBox.Show("Please enter Fine");
                }
                else if (paymenttextBox3.Text == "")
                {
                    MessageBox.Show("Please enter the amount!!");
                }
                else if (finetextBox2.Text != paymenttextBox3.Text)
                {
                    MessageBox.Show("Please enter the  Given amount!!");
                }
                else
                {
                    con.Open();
                    string query = "insert into demage(custid,custname,descri,fine,payment,regid)values(@cid,@cname,@des,@fine,@pay,@regi)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", custcomboBox2.Text);
                    cmd.Parameters.AddWithValue("@cname", custtextbox.Text);
                    cmd.Parameters.AddWithValue("@des", descpboxtextBox1.Text);
                    cmd.Parameters.AddWithValue("@fine", finetextBox2.Text);
                    cmd.Parameters.AddWithValue("@pay", paymenttextBox3.Text);
                    cmd.Parameters.AddWithValue("@regi", regcomboBox1.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Your Demage Form submitted successfully");
                    reset();
                }
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
    }
}

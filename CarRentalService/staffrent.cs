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
using System.IO;

namespace CarRentalService
{
    public partial class staffrent : Form
    {
        public staffrent()
        {
            InitializeComponent();
            showcar();

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void fillname()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from staff where staffid = @staffid", con);
                cmd.Parameters.AddWithValue("@staffid", staffcomboBox1.SelectedValue);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string name = rd["staffname"].ToString();
                    custtextbox.Text = name;
                }
                con.Close();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }
       
        private void fillstaff()
        {
           try
            {
                con.Open();
                string query = "select staffid from staff";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader datareader;
                datareader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("staffid", typeof(string));
                dt.Load(datareader);
                staffcomboBox1.ValueMember = "staffid";
                staffcomboBox1.DataSource = dt;
                con.Close();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void fillCombo()// this function will only show available car
        {
            con.Open();
            string query = "select Regno from CarTable where available='" + "YES" + "'";
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
        private void showcar() //function to show carlist in gridview
        {
            con.Open();
            string Query = "select *from CarTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            dataGridView1.DataSource = sd.Tables[0];
            con.Close();
        }
        private void staffcomboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillname();
        }

        private void staffrent_Load(object sender, EventArgs e)
        {
            fillCombo();
            fillstaff();
        }
        private void fillprice() // discount function for staff
        {

            try
            {

                //find date
                DateTime d1 = rentdateTimePicker1.Value.Date;
                DateTime d2 = retrundateTimePicker1.Value.Date;
                TimeSpan t = d2 - d1;
                int numberOfDays = Convert.ToInt32(t.TotalDays);

                string custid = staffcomboBox1.SelectedValue.ToString(); // replace with the actual user ID
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM rent WHERE staffid = @UserId", con);
                cmd.Parameters.AddWithValue("@UserId", custid);
                con.Open();
                int rentalCount = (int)cmd.ExecuteScalar();
                con.Close();

                if (rentalCount > 2) // assuming the discount is applied for 2 or more rentals
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT price FROM CarTable WHERE Regno = @Regno", con);
                    cmd2.Parameters.AddWithValue("@Regno", regcomboBox1.SelectedValue.ToString());
                    con.Open();
                    SqlDataReader rd = cmd2.ExecuteReader();
                    if (rd.Read())
                    {
                        int price = Convert.ToInt32(rd["price"]);
                        int discount = (int)(price * 0.25); // calculate 25% discount
                        int totalFee = price - discount;
                        int totalFee1 = totalFee * numberOfDays;
                        feetextBox1.Text = totalFee1.ToString();
                    }
                    con.Close();
                }
                else // no discount
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT Price FROM CarTable WHERE Regno = @Regno", con);
                    cmd2.Parameters.AddWithValue("@Regno", regcomboBox1.SelectedValue.ToString());
                    con.Open();
                    SqlDataReader rd = cmd2.ExecuteReader();
                    if (rd.Read())
                    {
                        int price = Convert.ToInt32(rd["price"]);
                        int totalFee = price * numberOfDays;
                        feetextBox1.Text = totalFee.ToString();

                    }
                    con.Close();
                }

            }
            catch (Exception messaage)
            {
                MessageBox.Show(messaage.Message);
            }

        }
        private void reset()
        {
            regcomboBox1.Text = " ";
            staffcomboBox1.Text = "";
            custtextbox.Text = "";
            feetextBox1.Text = "";
            rentdateTimePicker1.ResetText();
            retrundateTimePicker1.ResetText();
            pictureBox1.Image = null;
            paytextBox1.Text = "";
        }
        private void regcomboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillprice();
        }
        private void updaterent()
        {
            try
            {
                con.Open();
                string query = "update CarTable set available = @available where Regno = @regno;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@available", "NO");
                cmd.Parameters.AddWithValue("@regno", regcomboBox1.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (regcomboBox1.Text == "" || custtextbox.Text == "" || rentdateTimePicker1.Text == "" || retrundateTimePicker1.Text == "" || feetextBox1.Text == " ")
                {
                    MessageBox.Show("Please fillup the given Information");
                }
                else if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please upload the verification document!!");
                }
                else if (paytextBox1.Text == "")
                {
                    MessageBox.Show("Please payment the rent first");
                }
                else if(feetextBox1.Text != paytextBox1.Text)
                {
                    MessageBox.Show("Please enter the given amount!!");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into staffrent(carid,staffid,staffname,rentdate,returndate,image,payment)values(@regid,@stid,@cname,@rdate,@rndate,@img,@pay)", con);
                    cmd.Parameters.AddWithValue("@regid", regcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@stid", staffcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@cname", custtextbox.Text);
                    cmd.Parameters.AddWithValue("@rdate", rentdateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@rndate", retrundateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@img", getImage());
                    cmd.Parameters.AddWithValue("@pay", paytextBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car rented successfully enjoy your ride!!");
                    con.Close();
                    updaterent();
                    reset();
                }
            }
            catch (Exception message)
            {
                MessageBox.Show(" " + message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        string imagelocation = "";
        private void uploadbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jgp|All files(*.*)|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                imagelocation = openfile.FileName.ToString();
                pictureBox1.ImageLocation = imagelocation;
            }
        }
        private byte[] getImage() // this function for save image in byte format
        {
            MemoryStream steam = new MemoryStream();
            pictureBox1.Image.Save(steam, pictureBox1.Image.RawFormat);

            return steam.GetBuffer();

        }

        private void uploadbutton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jgp|All files(*.*)|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                imagelocation = openfile.FileName.ToString();
                pictureBox1.ImageLocation = imagelocation;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            staffuserinterface s = new staffuserinterface();
            s.Show();
            this.Hide();
        }
    }

    
   
   
}

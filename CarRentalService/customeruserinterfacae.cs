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
using Tulpep.NotificationWindow;

namespace CarRentalService
{
    public partial class customeruserinterfacae : Form
    {
        public customeruserinterfacae()
        {
            InitializeComponent();
            showcar();
          
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void reset()
        {
            regcomboBox1.Text = " ";
            custcomboBox2.Text = "";
            custtextbox.Text = "";
            feetextBox1.Text = "";
            rentdateTimePicker1.ResetText();
            retrundateTimePicker1.ResetText();
            pictureBox1.Image = null;

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
        private void label2_Click(object sender, EventArgs e)
        {
            uerChangePw c = new uerChangePw();
            c.Show();
            this.Hide();
        }
        private void fillprice() // discount function for user
        {

            try
            {

                //find date
                DateTime d1 = rentdateTimePicker1.Value.Date;
                DateTime d2 = retrundateTimePicker1.Value.Date;
                TimeSpan t = d2 - d1;
                int numberOfDays = Convert.ToInt32(t.TotalDays);

                string custid = custcomboBox2.SelectedValue.ToString(); // replace with the actual user ID
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM rent WHERE custid = @UserId", con);
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
                        int discount = (int)(price * 0.15); // calculate 15% discount
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
            catch(Exception messaage)
            {
                MessageBox.Show(messaage.Message);
            }
           
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // request button fucntion
        {
            try
            {
                if(regcomboBox1.Text=="" || custcomboBox2.Text=="" || custtextbox.Text==""|| rentdateTimePicker1.Text=="" || retrundateTimePicker1.Text =="" || feetextBox1.Text==" ")
                {
                    MessageBox.Show("Please fillup the given Information");
                }
                else if(pictureBox1.Image== null)
                {
                    MessageBox.Show("Please upload the verification document!!");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into rent(carregid,custid,staffid,custname,rentdate,returndate,fee,image,request)values(@regid,@cusid,@stid,@cname,@rdate,@rndate,@f,@img,@rq)", con);
                    cmd.Parameters.AddWithValue("@regid", regcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@cusid", custcomboBox2.Text);
                    cmd.Parameters.AddWithValue("@stid", staffcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@cname", custtextbox.Text);
                    cmd.Parameters.AddWithValue("@rdate", rentdateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@rndate", retrundateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@f",   feetextBox1.Text);
                    cmd.Parameters.AddWithValue("@img", getImage());
                    cmd.Parameters.AddWithValue("@rq", requestcomboBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your request has been sent!!");
                    con.Close();
                    updaterent();
                    reset();
                }
            }catch(Exception message)
            {
                MessageBox.Show(" "+message);
            }
        }
        string imagelocation = "";
        private void label13_Click(object sender, EventArgs e)
        {

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
        private void uploadbtn_Click_1(object sender, EventArgs e)
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

        private void label10_Click(object sender, EventArgs e)
        {
            demage f = new demage();
            f.Show();
            this.Hide();

        }
        private void fillstaff()
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
        private void label14_Click(object sender, EventArgs e)
        {
            reqestform form = new reqestform();
            form.Show();
            this.Hide();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void regcomboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillprice();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void customeruserinterfacae_Load(object sender, EventArgs e)
        {
           
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            fillCombo();
            fillcustomer();
            fillstaff();
        }

        private void custcomboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillname();
        }

        private void Paybtn_Click(object sender, EventArgs e)
        {
           
        }
       
        private void requestcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            CarBack cv = new CarBack();
            cv.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = "No Notification";
            popup.ContentText = "no notification ";
            popup.Popup();// show  
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            DashBoard db = new DashBoard();
            db.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void rentdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void retrundateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void custcomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

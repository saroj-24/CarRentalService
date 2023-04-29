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
    public partial class Rent : Form
    {
        public Rent()
        {
            InitializeComponent();
            showrent();
          
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");

        int regid;
        private void fillCombo()// this function will only show available car
        {
            try
            {
                con.Open();
                string query = "select Regno from CarTable where available='" + "YES" + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader datareader;
                datareader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Regno", typeof(string));
                dt.Load(datareader);
                rentcomboBox1.ValueMember = "Regno";
                rentcomboBox1.DataSource = dt;
                con.Close();

            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
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
        private void fillstaff()//this function for fill the staffid on combobox
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
        private void reset()
        {
            rentcomboBox1.Text = " ";
            staffcomboBox1.Text = "";
            custcomboBox2.Text = "";
            custtextbox.Text = "";
            feetextbox.Text = "";
            rentdateTimePicker1.ResetText();
            returndateTimePicker2.ResetText();
            pictureBox1.Image = null;

        }
        private void fillname()//function for fill the name of customer on textbox 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select *from customer where custid = @custid", con);
                cmd.Parameters.AddWithValue("@custid", custcomboBox2.SelectedValue.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string name = (string)rd["custname"].ToString();
                    custtextbox.Text = name;
                }
                con.Close();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }

        } 
        private void fillprice()// function for fill the price on pricetextbox
        {
            if (rentcomboBox1.SelectedValue != null)
            {
              
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from CarTable where Regno=@regno", con);
                    cmd.Parameters.AddWithValue("@regno", rentcomboBox1.SelectedValue.ToString());
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        string price = (string)rd["price"].ToString();
                        feetextbox.Text = price;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void showrent()
        {
            try
            {
                con.Open();
                string Query = "select *from rent";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var sd = new DataSet();
                sda.Fill(sd);
                rentdataGridView1.DataSource = sd.Tables[0];
                con.Close();
            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void updaterent()
        {
            try
            {
                con.Open();
                string query = "update CarTable set available = @available where Regno = @regno";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@available", "NO");
                cmd.Parameters.AddWithValue("@regno", rentcomboBox1.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void updaterentdelete()
        {
          
            try
            {
                con.Open();
                string query = "UPDATE CarTable SET available = @available WHERE Regno = @regno";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@available", "YES"); // assuming that `available` is an integer in the database
                cmd.Parameters.AddWithValue("@regno", rentcomboBox1.SelectedValue.ToString());

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("CarTable updated successfully.");
                }
                else
                {
                    MessageBox.Show("No rows were updated.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }

        }
        private void button2_Click(object sender, EventArgs e)// add button function
        {
           
        }

        private void rentdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e) // update button function
        {
          

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void staffcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void custcomboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillname();

           
        }

        private void panel5_Paint(object sender, PaintEventArgs e) // this function load the value of talbe in combobox field.
        {
            fillCombo();
            fillcustomer();
            fillstaff();
            
        }

        string imagelocation = "";
        private void uploadbtn_Click(object sender, EventArgs e) // function of upload image
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

        private void button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void rentcomboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillprice();
        }

        private void label9_Click(object sender, EventArgs e)
        {

          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            Car c = new Car();
            c.Show();
            this.Hide();
        }
      
        private void button3_Click(object sender, EventArgs e)// delete button
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from rent where regid = @regno", con);
                cmd.Parameters.AddWithValue("@regno", regid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("rent record delete!");
                con.Close();
                updaterentdelete();
                showrent();
                reset();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void rentdataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            regid = Convert.ToInt32(rentdataGridView1.SelectedRows[0].Cells[0].Value);
           rentcomboBox1.Text = rentdataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           custcomboBox2.Text = rentdataGridView1.SelectedRows[0].Cells[2].Value.ToString();
           staffcomboBox1.Text = rentdataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           custtextbox.Text = rentdataGridView1.SelectedRows[0].Cells[4].Value.ToString();
           rentdateTimePicker1.Text = rentdataGridView1.SelectedRows[0].Cells[5].Value.ToString();
           returndateTimePicker2.Text = rentdataGridView1.SelectedRows[0].Cells[6].Value.ToString();
           feetextbox.Text = rentdataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            requestcomboBox1.Text = rentdataGridView1.SelectedRows[0].Cells[9].Value.ToString();

           


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Rent_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            staffuserinterface c = new staffuserinterface();
            c.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updatebutton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (regid > 0)
                {
                    con.Open();
                    ///SqlCommand cmd = new SqlCommand("UPDATE rent SET request=@rq WHERE regid = @id", con);
                    SqlCommand cmd = new SqlCommand("UPDATE rent SET carregid=@cn,custid=@em,staffid=@add,custname=@ph,rentdate=@im ,returndate=@pw,fee=@fee,image=@ig,request=@rq WHERE regid = @id", con);
                    cmd.Parameters.AddWithValue("@cn", rentcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@em", custcomboBox2.Text);
                    cmd.Parameters.AddWithValue("@add", staffcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@ph", custtextbox.Text);
                    cmd.Parameters.AddWithValue("@im", rentdateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@pw", returndateTimePicker2.Value.Date);
                    cmd.Parameters.AddWithValue("@fee", feetextbox.Text);
                    cmd.Parameters.AddWithValue("@ig", getImage());
                    cmd.Parameters.AddWithValue("@rq", requestcomboBox1.Text);
                    cmd.Parameters.AddWithValue("@id", this.regid);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Car rent data updated to customer.");
                    
                    showrent();
                    reset();
                }
                else
                {
                    MessageBox.Show("Something is missing.");
                }
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
                con.Close();
            }

        }
    }
}

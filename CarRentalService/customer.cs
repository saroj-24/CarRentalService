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
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
            showcustomer();
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");

        public int custid;
        private void reset()
        {
            custextbox.Text = " ";
            emailtextBox.Text = "";
            addresstextBox.Text = "";
            phonetextBox.Text = "";
            pictureBox1.Image = null;
            pwtextBox1.Text = " ";
          
        }
        private void showcustomer()
        {
            con.Open();
            string Query = "select *from customer";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            custdataGridView.DataSource = sd.Tables[0];
            con.Close();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
        private  byte[] getImage() // this function for save image in byte format
        {
            MemoryStream steam = new MemoryStream();
            pictureBox1.Image.Save(steam, pictureBox1.Image.RawFormat);

            return steam.GetBuffer();
                 
        }
        string imagelocation = "";
        private void uploadbutton_Click(object sender, EventArgs e) // image upload function  
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jgp|All files(*.*)|*.*";
            if(openfile.ShowDialog() == DialogResult.OK)
            {
                imagelocation = openfile.FileName.ToString();
                pictureBox1.ImageLocation = imagelocation; 
            }
        }

        private void button2_Click(object sender, EventArgs e) // add button function 
        {
            try
            {
                if (custextbox.Text == "" || emailtextBox.Text == "" || phonetextBox.Text == " " || addresstextBox.Text == "" ||pwtextBox1.Text=="")
                {
                    MessageBox.Show("textfield is messing");
                  
                }
                else if(pictureBox1.Image == null)
                {
                    MessageBox.Show("Please upload the verification document");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into customer(custname,emailaddress,address,phone,image,password)values(@cn,@em,@add,@ph,@im,@pw)", con);
                    cmd.Parameters.AddWithValue("@cn", custextbox.Text);
                    cmd.Parameters.AddWithValue("@em", emailtextBox.Text);
                    cmd.Parameters.AddWithValue("@add", addresstextBox.Text);
                    cmd.Parameters.AddWithValue("@ph", phonetextBox.Text);
                    cmd.Parameters.AddWithValue("@im", getImage());
                    cmd.Parameters.AddWithValue("@pw", pwtextBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("customer data saved..");
                    con.Close();
                    showcustomer();
                    reset();
                    

                }
                
            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)// update button function
        {
            try
            {
               if(custid>0)
                {
                    if (custextbox.Text == "" || addresstextBox.Text == " " || phonetextBox.Text == "" || emailtextBox.Text == " ")
                    {
                        MessageBox.Show("select field ");
                    }
                    else
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE customer SET custname=@cn,emailaddress=@em,address=@add,phone=@ph,image=@im ,password=@pw WHERE custid = @id",con);
                        cmd.Parameters.AddWithValue("@cn", custextbox.Text);
                        cmd.Parameters.AddWithValue("@em", emailtextBox.Text);
                        cmd.Parameters.AddWithValue("@add", addresstextBox.Text);
                        cmd.Parameters.AddWithValue("@ph", phonetextBox.Text);
                        cmd.Parameters.AddWithValue("@im", getImage());
                        cmd.Parameters.AddWithValue("@pw", pwtextBox1.Text);
                        cmd.Parameters.AddWithValue("@id", this.custid);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" customer Data Update");
                        con.Close();
                        showcustomer();
                        reset();
                    }
                }
                else
                {
                    MessageBox.Show("Select the information");
                }

            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)// delete button function
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete customer where custid=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", custid);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                showcustomer();
                reset();

            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e) // clear button functipn
        {
            reset();
        }

     

        private void custdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) //gridview function
        {
            custid = Convert.ToInt32(custdataGridView.SelectedRows[0].Cells[0].Value);
            custextbox.Text = custdataGridView.SelectedRows[0].Cells[1].Value.ToString();
            emailtextBox.Text= custdataGridView.SelectedRows[0].Cells[2].Value.ToString();
            addresstextBox.Text= custdataGridView.SelectedRows[0].Cells[3].Value.ToString();
            phonetextBox.Text= custdataGridView.SelectedRows[0].Cells[4].Value.ToString();
          
          //  pictureBox1.Image = custdataGridView.SelectedRows[0].Cells[5].Value.ToString();
            pwtextBox1.Text = custdataGridView.SelectedRows[0].Cells[6].Value.ToString();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

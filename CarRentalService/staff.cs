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
    public partial class staff : Form
    {
        public staff()
        {
            InitializeComponent();
            showstaff(); // this function will fetch data of staff on gridview whenever the panel will open
        }

        //connection develop
       SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");

        int staffid;
        private void reset()//function to clear the textbox
        {
            stafftextbox.Text = " ";
            addresstextBox.Text = " ";
            phonetextBox.Text = " ";
            pwtextBox.Text = " ";


        }
        private void showstaff() //function to show staff
        {
            con.Open();
            string Query = "select *from staff";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            staffdataGridView.DataSource = sd.Tables[0];
            con.Close();
        }
        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // add button function
        {
            try
            {
                if(stafftextbox.Text=="" || addresstextBox.Text=="" || phonetextBox.Text == "" || pwtextBox.Text=="")
                {
                    MessageBox.Show(" Textfieled is empty!!");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into staff(staffname,address,phone,password)values(@sn,@add,@phn,@pw)",con);
                    cmd.Parameters.AddWithValue("@sn",stafftextbox.Text);
                    cmd.Parameters.AddWithValue("@add",addresstextBox.Text);
                    cmd.Parameters.AddWithValue("@phn", phonetextBox.Text);
                    cmd.Parameters.AddWithValue("@pw", pwtextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Staff Data Saved");
                    con.Close();
                    showstaff();
                    reset();
                }
            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // update function
        {

            try
            {
                if(stafftextbox.Text=="" ||addresstextBox.Text == " "|| phonetextBox.Text=="" || addresstextBox.Text ==" " )
                {
                    MessageBox.Show("Textfield is missing!!");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update staff set staffname=@sn,address=@add,phone=@phn,password=@pw where staffid = @id",con);
                    cmd.Parameters.AddWithValue("@sn", stafftextbox.Text);
                    cmd.Parameters.AddWithValue("@add", addresstextBox.Text);
                    cmd.Parameters.AddWithValue("@phn", phonetextBox.Text);
                    cmd.Parameters.AddWithValue("@pw", pwtextBox.Text);
                    cmd.Parameters.AddWithValue("@id", this.staffid);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Staff Data Update");
                    con.Close();
                    showstaff();
                    reset();
                }

            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void staffdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) // this function is to select items from gridview
        {
            staffid = Convert.ToInt32(staffdataGridView.SelectedRows[0].Cells[0].Value);
            stafftextbox.Text = staffdataGridView.SelectedRows[0].Cells[1].Value.ToString();
            addresstextBox.Text = staffdataGridView.SelectedRows[0].Cells[2].Value.ToString();
            phonetextBox.Text = staffdataGridView.SelectedRows[0].Cells[3].Value.ToString();
            pwtextBox.Text = staffdataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void stafftextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) // reset button to clear the field
        {
            reset();
        }

        private void button3_Click(object sender, EventArgs e) // Delete button function
        {
            try
            {

               
                
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" delete from staff where staffid = @staff", con);
                    cmd.Parameters.AddWithValue("@staff", staffid);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Staff Data Delete");
                    con.Close();
                    showstaff();
                    reset();

                

            }catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Car r = new Car();
            r.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

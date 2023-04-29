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
    public partial class CarBack : Form
    {
        public CarBack()
        {
            InitializeComponent();
            showrentcar();
            updaterentdelete();
            returcar();
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void reset()
        {
            registertextbox.Text = "";
            custnametextBox1.Text = "";
            returndateTimePicker1.Text = "";
            delaytextBox2.Text = "";
            finetextBox3.Text = "";
            paytextBox1.Text = "";
        }
        private void showrentcar()//this function show rent date
        {
            try
            {
                string sql = "SELECT regid,carregid,custname,rentdate,returndate from rent";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var sd = new DataSet();
                sda.Fill(sd);
                returndataGridView1.DataSource = sd.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex.Message);
                con.Close();
            }
        }
        int regid;
        private void returndataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            regid = Convert.ToInt32(returndataGridView1.SelectedRows[0].Cells[0].Value);
            registertextbox.Text = returndataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            custnametextBox1.Text = returndataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            returndateTimePicker1.Text = returndataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            DateTime d1 = returndateTimePicker1.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int numberOfDays = Convert.ToInt32(t.TotalDays);
            if (numberOfDays <= 0)
            {
                delaytextBox2.Text = "0";
                finetextBox3.Text = "0";
            }
            else
            {
                delaytextBox2.Text = "" + numberOfDays;
                finetextBox3.Text = "" + (numberOfDays * 250);// fine taken by admin or staff per day 250 
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
                cmd.Parameters.AddWithValue("@regno", registertextbox.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    //MessageBox.Show("CarTable updated successfully.");
                }
                else
                {
                    // MessageBox.Show("No rows were updated.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }
        private void returnbutton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (registertextbox.Text == "" || custnametextBox1.Text == "" || returndateTimePicker1.Text == "" || delaytextBox2.Text == "" || finetextBox3.Text == "")
                {
                    MessageBox.Show("please fillup the information");
                }
                else if (paytextBox1.Text == "")
                {
                    MessageBox.Show("please pay the cash");
                }
                else if(finetextBox3.Text != paytextBox1.Text)
                {
                    MessageBox.Show("Please enter the given amount");
                }
                else
                {

                    string query = "insert into [return] (carreg, custname, returndate, delay, fine, pay) values (@carg, @cname, @rndate, @del, @fne, @py)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@carg", registertextbox.Text);
                    cmd.Parameters.AddWithValue("@cname", custnametextBox1.Text);
                    cmd.Parameters.AddWithValue("@rndate", returndateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@del", delaytextBox2.Text);
                    cmd.Parameters.AddWithValue("@fne", finetextBox3.Text);
                    cmd.Parameters.AddWithValue("@py", paytextBox1.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Car Return and payment done Successfully");
                    updaterentdelete();
                    returcar();
                    deletefromrent();
                    reset();

                }

            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);

            }
        }

        private void retudataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void deletefromrent() // delete the car from rent=sql
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from rent where regid = @regno", con);
                cmd.Parameters.AddWithValue("@regno", regid);
                cmd.ExecuteNonQuery();
               /// MessageBox.Show("rent record delete!");
                con.Close();
               /// updaterentdelete();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        private void returcar()
        {
            try
            {
                string sql = "SELECT regid,carreg,custname,returndate,delay,fine,pay from [return]";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var sd = new DataSet();
                sda.Fill(sd);
                retudataGridView1.DataSource = sd.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex.Message);
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customeruserinterfacae c = new customeruserinterfacae();
            c.Show();
            this.Hide();
        }
    }
}

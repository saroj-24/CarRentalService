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
    public partial class changepw : Form
    {
        public changepw()
        {
            InitializeComponent();
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select staffname, password from staff where  staffname = @username and password = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", userTextBox.Text);
                cmd.Parameters.AddWithValue("@password", currentpw.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count.ToString() == "1")
                {
                    if (newpwfield.Text == repeatpwfield.Text)
                    {
                        con.Open();
                        query = "UPDATE staff SET password = @newpassword where staffname = @username";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@newpassword", newpwfield.Text);
                        cmd.Parameters.AddWithValue("@username", userTextBox.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        messagelabel.Text = "password successfully updated.";
                        messagelabel.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        messagelabel.Text = " new password repeat password not matched!";
                        messagelabel.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    messagelabel.Text = "current password not matched";
                    messagelabel.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
             
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void changepw_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.Show();
            this.Hide();

        }
    }
}

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
    public partial class AdminChangePW : Form
    {
        public AdminChangePW()
        {
            InitializeComponent();
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");

        private void AdminChangePW_Load(object sender, EventArgs e)
        {

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select admin, pw from admin where  admin= @username and pw = @password";
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
                        label7.Text = "password successfully updated.";
                        label7.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        label7.Text = " new password repeat password not matched!";
                        label7.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    label7.Text = "current password not matched";
                    label7.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

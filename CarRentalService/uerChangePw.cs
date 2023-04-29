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
    public partial class uerChangePw : Form
    {
        public uerChangePw()
        {
            InitializeComponent();
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select custname, password from customer where  custname = @username and password = @password";
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
                        query = "UPDATE customer SET password = @newpassword where custname = @username";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@newpassword", newpwfield.Text);
                        cmd.Parameters.AddWithValue("@username", userTextBox.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        messagelebel.Text = "password successfully updated.";
                        messagelebel.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        messagelebel.Text = " new password repeat password not matched!";
                        messagelebel.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    messagelebel.Text = "current password not matched";
                    messagelebel.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            customeruserinterfacae v = new customeruserinterfacae();
            v.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

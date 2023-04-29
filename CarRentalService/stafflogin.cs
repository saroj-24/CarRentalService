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
    public partial class stafflogin : Form
    {
        public stafflogin()
        {
            InitializeComponent();
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void backbutton1_Click(object sender, EventArgs e)
        {
            DashBoard d = new DashBoard();
            d.Show();
            this.Hide();

        }

        private void loginbtn_Click(object sender, EventArgs e)//staff login button
        {
            try
            {
                con.Open();
                string query = "select count(*) from staff where staffname = @username and password = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", usernametxt.Text);
                cmd.Parameters.AddWithValue("@password", pwfield.Text);
                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                   
                    staffuserinterface x = new staffuserinterface();
                    x.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username and password wrong!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}

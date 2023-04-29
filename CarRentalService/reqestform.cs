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
using Tulpep.NotificationWindow;

namespace CarRentalService
{
    public partial class reqestform : Form
    {
        public reqestform()
        {
            InitializeComponent();
            showrent();

        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void button4_Click(object sender, EventArgs e)
        {
            customeruserinterfacae cs = new customeruserinterfacae();
            cs.Show();
            this.Hide();
           
        }
        private void showrent()
        {
            con.Open();
            string Query = "select *from rent";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            dataGridView1.DataSource = sd.Tables[0];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            payment p = new payment();
            p.Show();
            this.Show();
        }
        int regid;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       private void CANCELbutton2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from rent where regid = @regno", con);
                cmd.Parameters.AddWithValue("@regno", regid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("rent record delete!");
                con.Close();
               //updaterentdelete();
                showrent();
               // reset();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void notifiedlabel4_Click(object sender, EventArgs e)
        {
           /* PopupNotifier popup = new PopupNotifier();
            popup.TitleText = "Rent Request";
            popup.ContentText = "Your request not accepted!!";
            popup.Popup();// show */
           
           
        }
    }
}

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
    public partial class ReturnCarList : Form
    {
        public ReturnCarList()
        {
            InitializeComponent();
            returncar();
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.Show();
            this.Hide();
        }

        private void returncardataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void returncar()
        {
            try
            {
                string sql = "SELECT regid,carreg,custname,returndate,delay,fine,pay from [return]";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var sd = new DataSet();
                sda.Fill(sd);
                returncardataGridView1.DataSource = sd.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex.Message);
                con.Close();
            }
        }

        private void ReturnCarList_Load(object sender, EventArgs e)
        {
            
        }
    }
}

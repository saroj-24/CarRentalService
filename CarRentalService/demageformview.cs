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
    public partial class demageformview : Form
    {
        public demageformview()
        {
            InitializeComponent();
            demagecar();
        }
        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        private void demagedataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void demageformview_Load(object sender, EventArgs e)
        {

        }
        public void loadDemageCar()
        {
            demagecar();
        }
        private void demagecar()
        {
            try
            {
                string sql = "SELECT custid,custname,descri,fine,payment,regid from [demage]";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var sd = new DataSet();
                sda.Fill(sd);
                demagedataGridView1.DataSource = sd.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex.Message);
                con.Close();
            }
        }
        private void backbutton3_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.Show();
            this.Hide();
        }
    }
}

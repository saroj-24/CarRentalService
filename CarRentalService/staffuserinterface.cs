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
    public partial class staffuserinterface : Form
    {
        public staffuserinterface()
        {
            InitializeComponent();
            showcar();
        }

        //connection develop
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\OneDrive\Documents\Car.mdf;Integrated Security=True;Connect Timeout=30");
        int carid;
        private void reset()//function to clear the textbox
        {
            regtextBox.Text = " ";
            modeltextBox.Text = "";
            brandcomboBox1.Text = string.Empty;
            colortextBox.Text = "";
            pricetextBox.Text = " ";
            avialcomboBox.Text = string.Empty;

        }
        private void showcar() //function to show carlist in gridview
        {
            con.Open();
            string Query = "select *from CarTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            cardataGridView.DataSource = sd.Tables[0];
            con.Close();
        }
        private void label14_Click(object sender, EventArgs e)
        {
            Sales s = new Sales();
            s.Show();
            this.Hide();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Rent r = new Rent();
            r.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            ReturnCarList c = new ReturnCarList();
            c.Show();
            this.Hide();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            changepw ch = new changepw();
            ch.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutlabel18_Click(object sender, EventArgs e)
        {
            DashBoard db = new DashBoard();
            db.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            staffrent sf = new staffrent();
            sf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (regtextBox.Text == "" || brandcomboBox1.SelectedIndex == -1 || modeltextBox.Text == "" || colortextBox.Text == "" || pricetextBox.Text == "" || avialcomboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Field !!!");
                }
                else
                {
                    
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into CarTable(Regno,brand,model,price,color,available)values(@reg,@brnd,@mdl,@prc,@cr,@avi)", con);
                        cmd.Parameters.AddWithValue("@reg", regtextBox.Text);
                        cmd.Parameters.AddWithValue("@brnd", brandcomboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@mdl", modeltextBox.Text);
                        cmd.Parameters.AddWithValue("prc", pricetextBox.Text);
                        cmd.Parameters.AddWithValue("@cr", colortextBox.Text);
                        cmd.Parameters.AddWithValue("@avi", avialcomboBox.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Car Data Save by staff");
                        con.Close();
                        showcar();
                        reset();

                }
            }
            catch (Exception messege)
            {
                MessageBox.Show(messege.Message);
            }
        }

        private void cardataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            carid = Convert.ToInt32(cardataGridView.SelectedRows[0].Cells[0].Value);
            regtextBox.Text = cardataGridView.SelectedRows[0].Cells[1].Value.ToString();
            brandcomboBox1.SelectedItem = cardataGridView.SelectedRows[0].Cells[2].Value.ToString();
            modeltextBox.Text = cardataGridView.SelectedRows[0].Cells[3].Value.ToString();
            pricetextBox.Text = cardataGridView.SelectedRows[0].Cells[4].Value.ToString();
            colortextBox.Text = cardataGridView.SelectedRows[0].Cells[5].Value.ToString();
            avialcomboBox.SelectedItem = cardataGridView.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)//update btn
        {

            try
            {

                if (carid > 0)
                {
                    if (regtextBox.Text == " " || brandcomboBox1.SelectedIndex == -1 || modeltextBox.Text == "" || colortextBox.Text == "" || pricetextBox.Text == "" || avialcomboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Missing Field !!!");
                    }
                    else
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE  CarTable SET Regno=@reg,brand=@brnd,model=@mdl,price=@prc,color=@cr,available=@avi where carid=@c", con);
                        cmd.Parameters.AddWithValue("@reg", regtextBox.Text);
                        cmd.Parameters.AddWithValue("@brnd", brandcomboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@mdl", modeltextBox.Text);
                        cmd.Parameters.AddWithValue("prc", pricetextBox.Text);
                        cmd.Parameters.AddWithValue("@cr", colortextBox.Text);
                        cmd.Parameters.AddWithValue("@avi", avialcomboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@c", this.carid);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Car Data Update by staff");
                        con.Close();
                        showcar();
                        reset();

                    }
                }
                else
                {
                    MessageBox.Show("Missing Field !!!");
                }


            }
            catch (Exception messege)
            {
                MessageBox.Show(messege.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)//deletevfunction
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from CarTable where carid = @regno", con);
                cmd.Parameters.AddWithValue("@regno", carid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("car record delete  by staff");
                con.Close();
                showcar();
                reset();
            }
            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            track t = new track();
            t.Show();
            this.Show();
        }
    }
}

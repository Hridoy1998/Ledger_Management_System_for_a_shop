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
namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string SellerName = "";
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Clear function
        private void label4_Click(object sender, EventArgs e)
        {
            UserNameTb.Text = "";
            PassTb.Text = "";
        }

        //Login Function
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserNameTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Enter the User Name add Password");
                }
                else
                {
                    if (RoleCb.SelectedIndex > -1)
                    {

                        if (RoleCb.SelectedItem.ToString() == "ADMIN")// Admin method
                        {
                            if (UserNameTb.Text == "Admin" && PassTb.Text == "Admin")
                            {
                                ProductForm prod = new ProductForm();
                                prod.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("If you are Admin, Enter the Correct Id and PassWord");
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Your in The Seller Section");
                            con.Open();
                            SqlDataAdapter sda = new SqlDataAdapter("select count(8) from SellerTb1 where SellerName='" + UserNameTb.Text + "' and SellerPass='" + PassTb.Text + "'", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows[0][0].ToString() == "1")
                            {
                                SellerName = UserNameTb.Text;
                                SellingForm sell = new SellingForm();
                                sell.Show();
                                this.Hide();
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Wrong UserName Or PassWord");
                            }
                            con.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Select A Role");
                    }
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RoleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            CreateAccount crt = new CreateAccount();
            crt.Show();
            this.Hide();
        }
    }
}

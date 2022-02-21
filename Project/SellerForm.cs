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
using SellerManagement.Ent;
using SellerManagement.oparetiom;

namespace Project
{
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");

        private void populate()
        {
            con.Open();
            string query = "select *  from SellerTb1";// display query
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        // Search Function
        private void Search()
        {
            try
            {
                con.Open();
                string query = "select * from SellerTb1 where SellerName Like  '%" + SellerS.Text + "%'";// display query
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
               // DT = new DataTable();
                sda.Fill(ds);
                SellerDGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //link with Product form founction
        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm Prod = new ProductForm();
            Prod.Show();
            this.Hide();
        }

        //link with Category form function
        private void button2_Click(object sender, EventArgs e)
        {
            CATEGORYFORM cat = new CATEGORYFORM();
            cat.Show();
            this.Hide();
        }

        //Seller Delete function
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "")
                {
                    MessageBox.Show("Select The Seller to Delete");
                }
                else
                {
                    try
                    {
                        ESellerManagement ESellerAdd = new ESellerManagement();
                        ESellerAdd.Id = int.Parse(SId.Text);
                        ESellerAdd.Name = SName.Text;
                        ESellerAdd.Age = int.Parse(SAge.Text);
                        ESellerAdd.Phone = SPhone.Text;
                        ESellerAdd.Password = SPass.Text;

                        OSellerManagement OSellerAdd = new OSellerManagement();
                        int Rows = OSellerAdd.DeleteSeller(ESellerAdd);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Seller Delete Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Seller not Delete");
                        }

                        populate();
                        SId.Text = "";
                        SName.Text = "";
                        SAge.Text = "";
                        SPhone.Text = "";
                        SPass.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Seller Add function
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "" || SName.Text == "" || SAge.Text == "" || SPhone.Text == "" || SPass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        ESellerManagement ESellerAdd = new ESellerManagement();
                        ESellerAdd.Id = int.Parse(SId.Text);
                        ESellerAdd.Name = SName.Text;
                        ESellerAdd.Age = int.Parse(SAge.Text);
                        ESellerAdd.Phone = SPhone.Text;
                        ESellerAdd.Password = SPass.Text;

                        OSellerManagement OSellerAdd = new OSellerManagement();
                        int Rows = OSellerAdd.AddSeller(ESellerAdd);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Seller Add Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Seller not Add");
                        }

                        populate();
                        SId.Text = "";
                        SName.Text = "";
                        SAge.Text = "";
                        SPhone.Text = "";
                        SPass.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("This Id Already Added!!!,Please add a new one ");
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
              
        }

        //Seller Edit function
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "" || SName.Text == "" || SAge.Text == ""||SPhone.Text == ""||SPass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        ESellerManagement ESellerAdd = new ESellerManagement();
                        ESellerAdd.Id = int.Parse(SId.Text);
                        ESellerAdd.Name = SName.Text;
                        ESellerAdd.Age = int.Parse(SAge.Text);
                        ESellerAdd.Phone = SPhone.Text;
                        ESellerAdd.Password = SPass.Text;

                        OSellerManagement OSellerAdd = new OSellerManagement();
                        int Rows = OSellerAdd.EditSeller(ESellerAdd);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Seller Update Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Seller not Update");
                        }

                        populate();
                        SId.Text = "";
                        SName.Text = "";
                        SAge.Text = "";
                        SPhone.Text = "";
                        SPass.Text = "";
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("This Id Already Added!!!,Please add a new one ");
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void SellerDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            SId.Text = SellerDGV.SelectedRows[0].Cells[0].Value.ToString();
            SName.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            SAge.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            SPhone.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            SPass.Text = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BillForm Bill = new BillForm();
            Bill.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}

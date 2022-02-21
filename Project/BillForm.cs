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
using BillManagement.BillEnt;
using BillManagement.BillOparetion;

namespace Project
{
    public partial class BillForm : Form
    {
        public BillForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");
        DataTable DT;
        private void populate()
        {
            con.Open();
            string query = "select *  from BillTb1";// display query
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            //DT = new DataTable();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        // Search Function
        private void Search()
        {
            try
            {
                con.Open();
                string query = "select * from BillTb1 where SellName Like  '%" + BillS.Text + "%'";// display query
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                DT = new DataTable();
                sda.Fill(ds);
                BillDGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm Seller = new SellerForm();
            Seller.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CATEGORYFORM cat = new CATEGORYFORM();
            cat.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductForm Prod = new ProductForm();
            Prod.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        //Bill Delete Function
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (BId.Text == "")
                {
                    MessageBox.Show("Select The Bill to Delete");
                }
                else
                {
                    try
                    {
                        EBillManagement EBill = new EBillManagement();
                        EBill.Id = int.Parse(BId.Text);
                        EBill.Name = SName.Text;
                        EBill.Date = BDate.Text;
                        EBill.Total = int.Parse(TAmt.Text);
                        EBill.Due = int.Parse(DBill.Text);

                        OBillManagement OBill = new OBillManagement();
                        int Rows = OBill.DeleteBill(EBill);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Bill Delete Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Bill Not Delete");
                        }

                        populate();
                        BId.Text = "";
                        SName.Text = "";
                        BDate.Text = "";
                        TAmt.Text = "";
                        DBill.Text = "";
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

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BId.Text = BillDGV.SelectedRows[0].Cells[0].Value.ToString();
            SName.Text = BillDGV.SelectedRows[0].Cells[1].Value.ToString();
            BDate.Text = BillDGV.SelectedRows[0].Cells[2].Value.ToString();
            TAmt.Text = BillDGV.SelectedRows[0].Cells[3].Value.ToString();
            DBill.Text = BillDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        //Bill Add Function
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (SName.Text == "" || DBill.Text == "" || TAmt.Text == "" || DBill.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        EBillManagement EBill = new EBillManagement();
                        EBill.Id = int.Parse(BId.Text);
                        EBill.Name = SName.Text;
                        EBill.Date = BDate.Text;
                        EBill.Total = int.Parse(TAmt.Text);
                        EBill.Due = int.Parse(DBill.Text);

                        OBillManagement OBill = new OBillManagement();
                        int Rows = OBill.AddBill(EBill);
                        if(Rows> 0)
                        {
                            MessageBox.Show("Bill Add Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Bill Not Add");
                        }

                        

                        populate();
                        BId.Text = "";
                        SName.Text = "";
                        BDate.Text = "";
                        TAmt.Text = "";
                        DBill.Text = "";
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
            }
        }

        //Bill edit function
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (BId.Text == "" || SName.Text == "" || BDate.Text == "" || TAmt.Text == "" || DBill.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        EBillManagement EBill = new EBillManagement();
                        EBill.Id = int.Parse(BId.Text);
                        EBill.Name = SName.Text;
                        EBill.Date = BDate.Text;
                        EBill.Total = int.Parse(TAmt.Text);
                        EBill.Due = int.Parse(DBill.Text);

                        OBillManagement OBill = new OBillManagement();
                        int Rows = OBill.EditBill(EBill);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Bill Successfully Updated");
                        }
                        else
                        {
                            MessageBox.Show("Bill Not Update");
                        }

                        
                      
                        populate();
                        BId.Text = "";
                        SName.Text = "";
                        BDate.Text = "";
                        TAmt.Text = "";
                        DBill.Text = "";
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
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}

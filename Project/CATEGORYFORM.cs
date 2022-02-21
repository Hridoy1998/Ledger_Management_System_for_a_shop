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
using CategoryManagement.CategoryEnt;
using CategoryManagement.CategoryOparetion;

namespace Project
{
    public partial class CATEGORYFORM : Form
    {
        public CATEGORYFORM()
        {
            InitializeComponent();
        }


        //Sql connection with CategoryTable
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");


        //Add Catrgories method
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        ECategoryManagement ECategory = new ECategoryManagement();
                        ECategory.Id = int.Parse(CatIdTb.Text);
                        ECategory.Name = CatNameTb.Text;
                        ECategory.Description = CatDescTb.Text;

                        OCategoryManagement OCategory = new OCategoryManagement();
                        int Rows = OCategory.AddCategory(ECategory);
                        if (Rows > 0)
                        {
                            MessageBox.Show("category Add Successfully");
                        }
                        else
                        {
                            MessageBox.Show("category Not Add");
                        }
                        
                        populate();
                        CatIdTb.Text = "";
                        CatNameTb.Text = "";
                        CatDescTb.Text = "";
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


            //MessageBox.Show("category Add Successfully");
            //MessageBox.Show("Category Id Already Added");
      

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        //Categories disply method
        private void populate()
        {
            con.Open();
            string query = "select *  from CategoryTb1";// display query
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        // Search Function
        private void Search()
        {
            try
            {
                con.Open();
                string query = "select * from CategoryTb1 where CatName Like  '%" + CatS.Text + "%'";// display query
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                //DT = new DataTable();
                sda.Fill(ds);
                CatDGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void CATEGORYFORM_Load(object sender, EventArgs e)
        {
            populate(); // Categories disply function
        }

        private void CatDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        //Category Delete Function
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatIdTb.Text=="")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {
                    try
                    {
                        ECategoryManagement ECategory = new ECategoryManagement();
                        ECategory.Id = int.Parse(CatIdTb.Text);
                        ECategory.Name = CatNameTb.Text;
                        ECategory.Description = CatDescTb.Text;

                        OCategoryManagement OCategory = new OCategoryManagement();
                        int Rows = OCategory.DeleteCategory(ECategory);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Category Successfully Delete");
                        }
                        else
                        {
                            MessageBox.Show("Category Not Delete");
                        }

                        populate();
                        CatIdTb.Text = "";
                        CatNameTb.Text = "";
                        CatDescTb.Text = "";
                    }
                     //MessageBox.Show("Cetegory Deleted Successfully");
                    catch
                    {
                        MessageBox.Show("ID not found");
                        con.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //Category Update Function
        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        ECategoryManagement ECategory = new ECategoryManagement();
                        ECategory.Id = int.Parse(CatIdTb.Text);
                        ECategory.Name = CatNameTb.Text;
                        ECategory.Description = CatDescTb.Text;

                        OCategoryManagement OCategory = new OCategoryManagement();
                        int Rows = OCategory.EditCategory(ECategory);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Category Successfully Updated");
                        }
                        else
                        {
                            MessageBox.Show("Category Not Updated");
                        }


                        populate();
                        CatIdTb.Text = "";
                        CatNameTb.Text = "";
                        CatDescTb.Text = "";
                    }
                    // MessageBox.Show("Category Successfully Updated");
                    catch (Exception ex)
                    {
                        MessageBox.Show("This Id Already Added!!!,Please add a new one ");
                        con.Close();
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //link with Product form Function
        private void button2_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm ();
            prod.Show();
            this.Hide();
        }

        //link with Seller Form function
        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        //link with Selling Form function
        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            BillForm Bill = new BillForm();
            Bill.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
    }
}

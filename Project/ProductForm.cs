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
using ProductManagement.ProductEnt;
using ProductManagement.ProductOparetion;

namespace Project
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");// database cannection

        //Combobox show method
        private void fillcombo()
        {
            //this method will bind the combobox with the Database
            con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTb1", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCd.ValueMember = "catName";
            CatCd.DataSource = dt;
            SearchCb.ValueMember = "catName";
            SearchCb.DataSource = dt;
            con.Close();
        }

        // Search Function
        private void Search()
        {
            try
            {
                con.Open();
                string query = "select * from ProductTb1 where ProdName Like  '%" + ProdS.Text + "%'";// display query
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
               // DT = new DataTable();
                sda.Fill(ds);
                ProdDGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();// combobox show function
            populate();
        }

        private void populate()
        {
            con.Open();
            string query = "select *  from ProductTb1";// display query
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            con.Close();
        }


        //Add Product Method
        private void button4_Click(object sender, EventArgs e)
        {
            try {
                if (ProdId.Text == "" || ProdNmae.Text == "" || ProdQty.Text == "" || ProdPrice.Text == "" || CatCd.Text == "")
                {
                    MessageBox.Show("Missing Info");
                }
                else {
                    try
                    {
                        EProductManagement EProduct = new EProductManagement();
                        EProduct.Id = int.Parse(ProdId.Text);
                        EProduct.Name = ProdNmae.Text;
                        EProduct.Quantity = int.Parse(ProdQty.Text);
                        EProduct.Price = int.Parse(ProdPrice.Text);
                        EProduct.Category = CatCd.Text;

                        OProductManagement OProduct = new OProductManagement();
                        int Rows = OProduct.AddProduct(EProduct);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Product Add Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Product Not Add");
                        }


                        //MessageBox.Show("Product Add Successfully");

                        populate();
                        ProdId.Text = "";
                        ProdNmae.Text = "";
                        ProdQty.Text = "";
                        ProdPrice.Text = "";
                        //CatCd.Text = "";
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

        private void ProdDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdId.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdNmae.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
            ProdQty.Text = ProdDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProdPrice.Text = ProdDGV.SelectedRows[0].Cells[3].Value.ToString();
            CatCd.SelectedValue = ProdDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Delete Product Method
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(ProdId.Text=="")
                {
                    MessageBox.Show("Select The Product to Delete");
                }
                else
                {
                    try
                    {
                        EProductManagement EProduct = new EProductManagement();
                        EProduct.Id = int.Parse(ProdId.Text);
                        EProduct.Name = ProdNmae.Text;
                        EProduct.Quantity = int.Parse(ProdQty.Text);
                        EProduct.Price = int.Parse(ProdPrice.Text);
                        EProduct.Category = CatCd.Text;

                        OProductManagement OProduct = new OProductManagement();
                        int Rows = OProduct.DeleteProduct(EProduct);
                        if (Rows > 0)
                        {
                            MessageBox.Show("Product Delete Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Product Not Delete");
                        }


                        //MessageBox.Show("Product Delete Successfully");

                        populate();
                        ProdId.Text = "";
                        ProdNmae.Text = "";
                        ProdQty.Text = "";
                        ProdPrice.Text = "";
                        //CatCd.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        con.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //link with Seller form function
        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm Seller = new SellerForm();
            Seller.Show();
            this.Hide();
        }

        //link with Categoryform function
        private void button2_Click(object sender, EventArgs e)
        {
            CATEGORYFORM cat = new CATEGORYFORM();
            cat.Show();
            this.Hide();
        }

        //Product edit Function
        private void button6_Click(object sender, EventArgs e)
        {
            if (ProdId.Text == "" || ProdNmae.Text == "" || ProdQty.Text == "" || ProdPrice.Text == "" || CatCd.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    EProductManagement EProduct = new EProductManagement();
                    EProduct.Id = int.Parse(ProdId.Text);
                    EProduct.Name = ProdNmae.Text;
                    EProduct.Quantity = int.Parse(ProdQty.Text);
                    EProduct.Price = int.Parse(ProdPrice.Text);
                    EProduct.Category = CatCd.Text;

                    OProductManagement OProduct = new OProductManagement();
                    int Rows = OProduct.EditProduct(EProduct);
                    if (Rows > 0)
                    {
                        MessageBox.Show("Product Update Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Product Not Update");
                    }


                    //MessageBox.Show("Product Add Successfully");

                    populate();
                    ProdId.Text = "";
                    ProdNmae.Text = "";
                    ProdQty.Text = "";
                    ProdPrice.Text = "";
                    //CatCd.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("This Id Already Added!!!,Please add a new one ");
                    con.Close();
                }
            }
        }
        private void SearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select * from ProductTb1 where ProdCat ='" + SearchCb.SelectedValue.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void SearchCb_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}

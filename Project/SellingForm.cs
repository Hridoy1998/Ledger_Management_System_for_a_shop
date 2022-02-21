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
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }
         // SQL connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");// database cannection
        
        //its a function for show Product data Grid view value from the SQL server
        private void populate()
        {
            con.Open();
            string query = "select ProdName, ProdPrice from ProductTb1";// display query
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            con.Close();
        }

        // Search Function
        private void Search()
        {
            try
            {
                con.Open();
                string query = "select * from BillTb1 where BillDate Like  '%" + HistoryS.Text + "%'";// display query
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                //DT = new DataTable();
                sda.Fill(ds);
                BillsDGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //its a function for show Bill Data Grid view value from the SQL server
        private void populateBills()
        {
            con.Open();
            string query = "select * from BillTb1 where SellName Like  '%" + SellerNamelb.Text + "%'";// display query
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillsDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        //display function 
        private void SellingForm_Load(object sender, EventArgs e)
        {
            populateBills();
            populate();
            fillcombo();
            SellerNamelb.Text = Form1.SellerName;
        }


        //Select paten for Product Data Grid View
        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNmae.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            ProdPrice.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
           
        }

        //Date Methode
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelbl.Text = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        //Exit button
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //Add Products from seller Given information to History data Grid view function
        int GrdTotal = 0, n=0;

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (BillId.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    try
                    {
                        con.Open();
                        string query = "insert into BillTb1 values(" + BillId.Text + ",'" + SellerNamelb.Text + "','" + Datelbl.Text + "'," + AmtIbl.Text + "," + PAmtBill.Text + ")";//Add qurey
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Add Successfully");
                        con.Close();
                        populateBills();
                        BillId.Text = "";
                        SellerNamelb.Text = "";
                        AmtIbl.Text = "";
                        PAmtBill.Text = "";
                        Payment.Text = "";
                        ORDERDGV.Rows.Clear();
                    }
                    catch(Exception ex)
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        //Print function
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString("Ledger Management System", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
                e.Graphics.DrawString("Bill ID: "+BillsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Green, new Point(100,70));
                e.Graphics.DrawString("Seller Name: " + BillsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Green, new Point(100, 100));
                e.Graphics.DrawString("Bill Date: " + BillsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Green, new Point(100, 130));
                e.Graphics.DrawString("Total Amount: " + BillsDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Green, new Point(100, 160));
                e.Graphics.DrawString("Due Bill: " + BillsDGV.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Green, new Point(100, 190));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

      
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
            //Cat.ValueMember = "catName";
            //CatCd.DataSource = dt;
            SearchCb.ValueMember = "catName";
            SearchCb.DataSource = dt;
            con.Close();
        }

        //show function for Product Data Grid view
        private void SearchCb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "select ProdName, ProdPrice from ProductTb1 where ProdCat ='" + (SearchCb.SelectedValue.ToString()+"'");
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ProdDGV1.DataSource = ds.Tables[0];
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        //Delete orderData grid viwers from given information by user function
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                ORDERDGV.CurrentRow.Selected = false;
                int rowIndex = ORDERDGV.CurrentCell.RowIndex;
                ORDERDGV.Rows.RemoveAt(rowIndex);
                GrdTotal = GrdTotal - total;
                AmtIbl.Text = "" + GrdTotal;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Due calculation function
        private void PAY_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Payment.Text == "")
                {
                    MessageBox.Show("Please Set your Payment Amount");
                }
                else if (GrdTotal < 0)
                {
                    MessageBox.Show("PLease Add products first");
                }
                else if (GrdTotal == 0)
                {
                    MessageBox.Show("No Due");
                }
                else
                {
                    int pay;
                    pay = GrdTotal - Convert.ToInt32(Payment.Text);
                    PAmtBill.Text = "" + pay;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ORDERDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNmae.Text = ORDERDGV.SelectedRows[0].Cells[1].Value.ToString();
            ProdPrice.Text = ORDERDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProdQty.Text = ORDERDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            populateBills();
        }

        private void Datelbl_Click(object sender, EventArgs e)
        {

        }

        private void SellerNamelb_Click(object sender, EventArgs e)
        {

        }

        //Add order Data grid viwers from given information by user function
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (ProdPrice.Text == "" || ProdQty.Text == "" || ProdNmae.Text == "")
                {
                    MessageBox.Show("Samething missing in data");
                }
                else
                {
                    int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(ORDERDGV);// create cell on Order DataGridView
                    newRow.Cells[0].Value = n + 1;
                    newRow.Cells[1].Value = ProdNmae.Text;
                    newRow.Cells[2].Value = ProdPrice.Text;
                    newRow.Cells[3].Value = ProdQty.Text;
                    newRow.Cells[4].Value = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                    ORDERDGV.Rows.Add(newRow);
                    n++;
                    GrdTotal = GrdTotal + total;
                    AmtIbl.Text = "" + GrdTotal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}

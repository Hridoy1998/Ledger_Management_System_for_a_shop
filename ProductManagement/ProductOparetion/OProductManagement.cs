using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ProductManagement.ProductEnt;

namespace ProductManagement.ProductOparetion
{
    public class OProductManagement
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");

        public int AddProduct(EProductManagement Product)
        {

            con.Open();
            string query = "insert into ProductTb1 values(" + Product.Id + ",'" + Product.Name + "'," + Product.Quantity + "," + Product.Price + ",'" + Product.Category + "')"; //Add qurey
            SqlCommand cmd = new SqlCommand(query, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
        public int EditProduct(EProductManagement Product)
        {

            con.Open();
            string query = "Update ProductTb1 set ProdName ='" + Product.Name + "',ProdQty=" + Product.Quantity + ",ProdPrice=" + Product.Price + ",ProdCat='" + Product.Category + "'where Prodid=" + Product.Id + ";";//Update query
            SqlCommand cmd = new SqlCommand(query, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
        public int DeleteProduct(EProductManagement Product)
        {

            con.Open();
            string query = "delete from ProductTb1 where ProdId=" + Product.Id + "";//delete query
            SqlCommand cmd = new SqlCommand(query, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
    }
}

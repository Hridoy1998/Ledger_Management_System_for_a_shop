using SellerManagement.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SellerManagement.oparetiom
{
    public class OSellerManagement
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");
        public int AddSeller(ESellerManagement Seller)
        {
           
                con.Open();
                string query = "insert into SellerTb1 values(" + Seller.Id + ",'" + Seller.Name + "'," + Seller.Age + ",'" + Seller.Phone + "','" + Seller.Password + "')"; //Add qurey
                SqlCommand cmd = new SqlCommand(query, con);
                int Rows = cmd.ExecuteNonQuery();
                con.Close();
            return Rows;
            
        }
        public int EditSeller(ESellerManagement Seller)
        {
            
                con.Open();
                string query = "Update SellerTb1 set SellerName='" + Seller.Name + "',SellerAge=" + Seller.Age + ",SellerPhone='" + Seller.Phone + "',SellerPass='" + Seller.Password + "'where SellerId=" + Seller.Id + ";";//Update query
                SqlCommand cmd = new SqlCommand(query, con);
                int Rows = cmd.ExecuteNonQuery();
                con.Close();
            return Rows;
            
        }
        public int DeleteSeller(ESellerManagement Seller)
        {
            
                con.Open();
                string query = "delete from SellerTb1 where SellerId=" + Seller.Id + "";//delete query
                SqlCommand cmd = new SqlCommand(query, con);
                int Rows = cmd.ExecuteNonQuery();
                con.Close();
            return Rows;
            
        }

    }
}

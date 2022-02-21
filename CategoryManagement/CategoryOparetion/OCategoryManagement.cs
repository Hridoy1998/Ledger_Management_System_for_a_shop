using CategoryManagement.CategoryEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CategoryManagement.CategoryOparetion
{
   public class OCategoryManagement
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");
        public int AddCategory(ECategoryManagement Category)
        {
            con.Open();
            string query = "insert into CategoryTb1 values(" + Category.Id + ",'" + Category.Name + "','" + Category.Description + "')";//Add qurey
            SqlCommand cmd = new SqlCommand(query, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
        public  int EditCategory(ECategoryManagement Category)
        {
            con.Open();
            string query = "Update CategoryTb1 set CatName='" + Category.Name + "',CatDesc='" + Category.Description + "'where CatId=" + Category.Id + ";";//Update query
            SqlCommand cmd = new SqlCommand(query, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
        public int DeleteCategory(ECategoryManagement Category)
        {
            con.Open();
            string query = "delete from CategoryTb1 where CatId=" + Category.Id + "";//delete query
            SqlCommand cmd = new SqlCommand(query, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BillManagement.BillEnt;

namespace BillManagement.BillOparetion
{
    public class OBillManagement
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG04PPF;Integrated Security=True");
        public int AddBill(EBillManagement Bill)
        {
                con.Open();
                string query = "insert into BillTb1 values(" + Bill.Id + ",'" + Bill.Name + "','" + Bill.Date + "',"+Bill.Total+","+Bill.Due+")";//Add qurey
                SqlCommand cmd = new SqlCommand(query, con);
               int Rows = cmd.ExecuteNonQuery();
                con.Close();
                return Rows;
        }
        public int EditBill(EBillManagement Bill)
        {
                con.Open();
                string query = "Update BillTb1 set SellName='" + Bill.Name + "', BillDate='" +Bill.Date+ "',TolAmt="+Bill.Total+ ",DueBill="+Bill.Due+"where BillId="+Bill.Id+"";//Update query
                SqlCommand cmd = new SqlCommand(query, con);
                int Rows = cmd.ExecuteNonQuery();
                con.Close();
                return Rows;
        }
        public int DeleteBill(EBillManagement Bill)
        {
                con.Open();
                string query = "delete from BillTb1 where BillId=" + Bill.Id + "";//delete query
                SqlCommand cmd = new SqlCommand(query, con);
                int Rows = cmd.ExecuteNonQuery();
                con.Close();
                return Rows;
            
        }
    }
}

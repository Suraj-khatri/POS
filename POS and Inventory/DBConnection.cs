using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_and_Inventory
{
    public class DBConnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public string MyConnection()
        {
            string con = @"Data Source=DESKTOP-KVOQSDF\SQLEXPRESS;Initial Catalog=Pos_And_Inventory;Integrated Security=True";
            return con;
        }
        public double GetVal()
        {
            double vat = 0;
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("select * from tblVat", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                vat = double.Parse(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
            return vat;
        }
    }
}

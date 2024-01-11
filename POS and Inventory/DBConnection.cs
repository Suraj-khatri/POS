using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_and_Inventory
{
    public class DBConnection
    {
        public string MyConnection()
        {
            string con = @"Data Source=DESKTOP-KVOQSDF\SQLEXPRESS;Initial Catalog=Pos_And_Inventory;Integrated Security=True";
            return con;
        }
    }
}

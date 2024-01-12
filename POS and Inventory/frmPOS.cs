using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_and_Inventory
{
    public partial class frmPOS : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "Simple POS System";
        [Obsolete]
        public frmPOS()
        {
            InitializeComponent();
            lblDate.Text = DateTime.Now.ToLongDateString();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }
        private void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyymmdd");
                string transno;
                int count;
                cn.Open();
                cm = new SqlCommand("select top 1 transno from tblCart where transno like '"+sdate+"%' order by id desc ",cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows) 
                { 
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                } 
                else
                { 
                    transno = sdate + "1001"; 
                    lblTransno.Text = transno;
                }
                dr.Close();
                cn.Close();

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message,stitle,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            GetTransNo();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == string.Empty) { return; }
                else
                {
                    cn.Open();
                    cm = new SqlCommand("select * from tblProduct where barcode like '" + txtSearch.Text + "' ",cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        frmQty frm = new frmQty(this);
                        frm.ProductDetail(dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lblTransno.Text);
                        dr.Close();
                        cn.Close();
                        frm.ShowDialog();
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadCart()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("select c.id,c.pcode,p.pdesc,c.price,c.qty,c.disc,c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where transno like '"+lblTransno.Text+"' ", cn);
                dr = cm.ExecuteReader();
                while(dr.Read())
                {
                    i++;
                    
                    dataGridView1.Rows.Add(i, dr["id"].ToString(),dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());
                }
                dr.Close ();
                cn.Close();
            }
            catch (Exception ex) 
            {
                cn.Close();
                MessageBox.Show(ex.Message,stitle,MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

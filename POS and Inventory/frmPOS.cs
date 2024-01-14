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
        string id;
        string price;
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

        public void GetCartTotal()
        {
            //double subTol = double.Parse(lblTotal.Text);
            double discount = double.Parse(lblDiscount.Text);
            double sales = double.Parse(lblTotal.Text);

            double vat = sales * dbcon.GetVal();
            double vatable = sales + vat;
            lblTotal.Text = sales.ToString("#,##0.00");
            //lblTotal.Text = (sales- discount).ToString("#,##0.00");
            //lblTotal.Text = (sales- discount).ToString("#,##0.00");
            lblDisplayTotal.Text = (sales- discount).ToString("#,##0.00");
            lblVat.Text = vat.ToString();
            lblVatable.Text = vatable.ToString("#,##0.00");
        }

        public void GetTransNo()
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
                bool hasrecord = false;
                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double discount = 0;
                cn.Open();
                cm = new SqlCommand("select c.id,c.pcode,p.pdesc,c.price,c.qty,c.disc,c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where transno like '"+lblTransno.Text+"' and status like 'Pending' ", cn);
                dr = cm.ExecuteReader();
                while(dr.Read())
                {
                    i++;
                    total += double.Parse(dr["total"].ToString());
                    discount += double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"],dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                    hasrecord = true;
                }
                dr.Close ();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
               // lblDisplayTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");

                GetCartTotal();

                if(hasrecord) { btnSettle.Enabled = true; btnDiscount.Enabled = true; } else { btnSettle.Enabled = false; btnDiscount.Enabled = false; }
            }
            catch (Exception ex) 
            {
                cn.Close();
                MessageBox.Show(ex.Message,stitle,MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
               if(MessageBox.Show("Remove this item?",stitle,MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblCart where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() +"' ",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Removed Successfully.",stitle,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadCart();
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(lblTransno.Text == "00000000000000") { return; }
            frmLookUp frm = new frmLookUp(this);
            frm.LoadProduct(); 
            frm.ShowDialog();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {

        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {

            frmDiscount frm = new frmDiscount(this);
            
            frm.lblID.Text = id;
            frm.txtPrice.Text = price;
            frm.ShowDialog();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[1,i].Value.ToString();
            price = dataGridView1[3,i].Value.ToString();
        }

        //<info>
        //  we have to enable timer first in property    
        //</info>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDateInWord.Text = DateTime.Now.ToLongDateString();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmSettle frm = new frmSettle(this);
            frm.txtCash.Focus();
            frm.txtCash.Select();
            frm.txtSale.Text = lblDisplayTotal.Text;
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

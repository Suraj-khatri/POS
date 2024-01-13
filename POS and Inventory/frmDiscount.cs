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
    public partial class frmDiscount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "Simple POS system";
        frmPOS pos;
        public frmDiscount(frmPOS frmPOS)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            pos = frmPOS;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {

        }

        private void txtPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = double.Parse(txtPrice.Text) * double.Parse(txtPercent.Text);
                txtAmount.Text = discount.ToString("#,##0.00");
            }catch(Exception ex)
            {
                txtAmount.Text = "0.00";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Add Discount? click yes to confirm.",stitle,MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblcart set disc = @disc where id = @id",cn);
                    cm.Parameters.AddWithValue("@disc",Double.Parse(txtAmount.Text));
                    cm.Parameters.AddWithValue("@id",int.Parse(lblID.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();
                    pos.LoadCart();
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

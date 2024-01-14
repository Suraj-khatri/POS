using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_and_Inventory
{
    public partial class frmSettle : Form
    {
        public frmSettle()
        {
            InitializeComponent();
            txtCash.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmSettle_Load(object sender, EventArgs e)
        {

        }

        

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = Convert.ToDouble(txtSale.Text);
                double cash = Convert.ToDouble(txtCash.Text);
                txtChange.Text = (sale -  cash).ToString("#,##0.00");

            }
            catch (Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;

        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;

        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;

        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;

        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;

        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;

        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;

        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;

        }
    }
}

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
    public partial class frmUserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public frmUserAccount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void metroTabControl1_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }

        private void frmUserAccount_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            txtUser.Clear();
            txtPass.Clear();
            txtRetype.Clear();
            txtName.Clear();
            cboRole.Text = "";
            txtUser.Focus();
        }


        

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if((txtUser.Text == string.Empty) || (txtPass.Text == string.Empty) || ((txtName.Text == string.Empty)) || (cboRole.Text == "")) {
                MessageBox.Show("Fields cannot be empty");
            }
            else
            {
                try
                {
                    if (txtRetype.Text != txtPass.Text)
                    {
                        MessageBox.Show("Password donot match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    cn.Open();
                    cm = new SqlCommand("insert into tbluser(username,password,role,name) values(@username,@password,@role,@name) ", cn);
                    cm.Parameters.AddWithValue("@username", txtUser.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    cm.Parameters.AddWithValue("@role", cboRole.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cn.Close();
                    MessageBox.Show("New user saved", "New User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                catch (Exception ex)
                {
                    cn.Close();
                    MessageBox.Show("Please choose Different username. That may be already used");
                }
            }
            
        }
    }
}

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
    public partial class frmSecurity : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _username = "", _role = "", _name = "";
            try
            {
                bool found = false;
                cn.Open();
                cm = new SqlCommand("select * from tbluser where username = @username and password = @pass ",cn);
                cm.Parameters.AddWithValue("@username",txtUser.Text);
                cm.Parameters.AddWithValue("@pass",txtPass.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                }
                dr.Close();
                cn.Close();

                if(found)
                {
                    if (_role == "Cashier")
                    {
                        MessageBox.Show("WelCome!" + _name + "|", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUser.Clear();
                        txtPass.Clear();
                        this.Hide();
                        frmPOS frm = new frmPOS(this);
                        frm.lblUser.Text = _username;
                        frm.ShowDialog();
                    }
                    if (_role == "Administrator")
                    {
                        MessageBox.Show("WelCome!" + _name + "|", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUser.Clear();
                        txtPass.Clear();
                        this.Hide();
                        Form1 frm = new Form1();
                        frm.lblUser.Text = _username;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password.", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                }

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUser.Clear();
            txtPass.Clear();
        }
    }
}

﻿using System;
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
    public partial class frmLookUp : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmPOS pos;
        public frmLookUp(frmPOS pos)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.pos = pos;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
        public void LoadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select p.pcode,p.barcode,p.pdesc,b.brand,c.category,p.price,p.qty from tblProduct as p inner join tblBrand as b on b.id = p.bid inner join tblCategory as c on c.id = p.cid where p.pdesc like '" + txtSearch.Text + "%' order by p.pdesc ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            cn.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                frmQty frm = new frmQty(pos);
                frm.ProductDetail(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()), pos.lblTransno.Text);
                
                frm.ShowDialog();
            }
        }
    }
}

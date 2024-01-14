using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WinForms;
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
    public partial class frmSoldReport : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "Simple POS System";
        frmSoldItems frm = new frmSoldItems();

        public frmSoldReport(frmSoldItems f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frm = f;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmSoldReport_Load(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        public void LoadReport()
        {
            ReportDataSource rptDataSource;
            try
            {

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\soldItemsReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();

                da.SelectCommand = new SqlCommand("select c.id,c.transno,c.pcode,p.pdesc,c.price,c.qty,c.disc,c.total from tblcart as c inner join tblproduct as p on p.pcode = c.pcode where c.status like 'Sold' and sdate between '" + frm.dt1.Value + "' and '" + frm.dt2.Value + "' ", cn);
                da.Fill(ds.Tables["dtSoldItems"]);
                cn.Close();

                ReportParameter pTotal = new ReportParameter("pTotal", frm.lblTotal.Text);

                reportViewer1.LocalReport.SetParameters(pTotal);


                rptDataSource = new ReportDataSource("DataSet1", ds.Tables["dtSoldItems"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 30;


            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}

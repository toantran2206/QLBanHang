using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Bai3._3
{
    public partial class Form1 : Form
    {
        string cnStr;
        SqlConnection cn;
        SqlDataAdapter da;
        DataSet ds;
        DataTable Orders;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(cnStr);
            string sql;
            sql = "SELECT * FROM HoaDon";
            Orders = GetHoaDonDataset(sql).Tables[0];
            cbMaHD.DataSource = Orders;
            cbMaHD.ValueMember = "MaHD";
            cbMaHD.DisplayMember = "MaHD";

            txtMaNV.DataBindings.Add("Text", Orders, "MaNV");
            txtMaKH.DataBindings.Add("Text", Orders, "MaKH");

            dtpOrderDate.DataBindings.Add("Text", Orders, "NgayLapHD");
            dtpShippedDate.DataBindings.Add("Text", Orders, "NgayGiaoHang");


        }
        private void DisConnect()
        {
            if (cn != null && cn.State == ConnectionState.Open)
                cn.Close();
        }
        public DataSet GetHoaDonDataset(string sql)
        {
            try
            {
                ds = new DataSet();
                
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);

                da.Fill(ds);
                dgvHD.DataSource = ds.Tables[0];
                return ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
                //throw;
            }
            finally
            {
                cn.Close();
            }
        }

        private void cbMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM CTHD WHERE MaHD = '" + cbMaHD.Text + "'";
            dgvHD.DataSource = GetHoaDonDataset(sql).Tables[0];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM HoaDon";
            dgvHD.DataSource = GetHoaDonDataset(sql).Tables[0];
        }
        
    }
}

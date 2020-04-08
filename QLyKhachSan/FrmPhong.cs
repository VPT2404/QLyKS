using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLyKhachSan
{
    public partial class FrmPhong : Form
    {
        SqlConnection con = new SqlConnection();
       
        public FrmPhong()
        {
            InitializeComponent();
        }

        private void FrmPhong_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = QuanLyKhachSan; Integrated Security = True";
            con.ConnectionString = connectionstring;
           con.Open();
            string SQL = "select * from tbPhong";
            SqlDataAdapter dataadp = new SqlDataAdapter(SQL, con);
            DataTable tablePhong = new DataTable();
            dataadp.Fill(tablePhong);
            dataGridView1.DataSource = tablePhong;
            LoadDatagriview();
            
        }
        private void LoadDatagriview()
        {
            string SQL = "select * from tbPhong";
            SqlDataAdapter dataadp = new SqlDataAdapter(SQL, con);
            DataTable tablePhong = new DataTable();
            dataadp.Fill(tablePhong);
            dataGridView1.DataSource = tablePhong;
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            string SQL;
            SQL = " Update tblPhong Set TenPhong=N'" + txtTenPhong.Text + "',DonGia=N'" + txtDonGia.Text + "'" +
            "Where MaPhong=N'" + txtMaPhong.Text + "'";
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            LoadDatagriview();
            txtMaPhong.Enabled = false;
            bntHuy.Enabled = false;
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            string SQL = "Delete from tbPhong where MaPhong='" + txtMaPhong.Text + "'";
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            LoadDatagriview();
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string SQL;
            SQL = "select MaPhong from tbPhong WHere MaPhong='" + txtMaPhong.Text + "'";
            SqlDataAdapter adap = new SqlDataAdapter(SQL, con);
            DataTable tablePhong = new DataTable();
            adap.Fill(tablePhong);
            if (tablePhong.Rows.Count > 0)
            {
                MessageBox.Show("mã phòng này  đã tồn tại, nhâp mẫ khác");
                txtMaPhong.Focus();
                return;
            }
            SQL = "insert into tbPhong Values('" + txtMaPhong.Text + "','" + txtTenPhong.Text + "'";
            if (txtDonGia.Text != "")
                SQL = SQL + "," + txtDonGia.Text.Trim();
            SQL = SQL + ")";
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            LoadDatagriview();
        }

        private void bntHuy_Click(object sender, EventArgs e)
        {
            bntHuy.Enabled = false;
            bntThem.Enabled = false;
            bntLuu.Enabled = true;
        }

        private void bntThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13)))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView1.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView1.CurrentRow.Cells["TPhong"].Value.ToString();
            txtDonGia.Text = dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
        }
    }
}

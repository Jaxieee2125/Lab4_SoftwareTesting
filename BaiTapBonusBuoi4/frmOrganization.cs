using System;
using System.Data;
using System.Data.SqlClient; // Thư viện kết nối SQL
using System.Text.RegularExpressions; // Thư viện xử lý Regex (Validate Email, Phone)
using System.Windows.Forms;

namespace BaiTapBonusBuoi4
{
    public partial class frmOrganization : Form
    {
        // 1. Cấu hình chuỗi kết nối (Sửa Server Name cho đúng với máy bạn)
        // Nếu dùng SQL Authentication: "Server=YOUR_SERVER;Database=OrgManagementDB;User Id=sa;Password=your_password;"
        // Nếu dùng Windows Authentication:
        string connectionString = @"Server=localhost;Database=OrgManagementDB;Integrated Security=True";

        // Biến lưu OrgID sau khi lưu thành công để truyền sang form Director
        private int currentOrgID = -1;

        public frmOrganization()
        {
            InitializeComponent();
            // Yêu cầu: Nút Director ban đầu disable
            btnDirector.Enabled = false;
        }

        // ==========================================
        // 5.1 XỬ LÝ NÚT SAVE
        // ==========================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Bước 1: Validate dữ liệu (Mục 3.2 và 4)
            if (!ValidateInput()) return;

            // Bước 2: Kiểm tra trùng tên trong CSDL (Mục 5.1.3)
            if (IsOrgNameExist(txtOrgName.Text.Trim()))
            {
                MessageBox.Show("Organization Name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bước 3: Lưu vào CSDL (Mục 5.1.4)
            if (SaveOrganization())
            {
                MessageBox.Show("Save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Enable nút Director sau khi lưu thành công
                btnDirector.Enabled = true;
            }
        }

        // ==========================================
        // HÀM KIỂM TRA HỢP LỆ (VALIDATION)
        // ==========================================
        private bool ValidateInput()
        {
            // 1. Kiểm tra OrgName
            string name = txtOrgName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Organization Name không được để trống.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrgName.Focus();
                return false;
            }
            if (name.Length < 3 || name.Length > 255)
            {
                MessageBox.Show("Độ dài Organization Name phải từ 3 đến 255 ký tự.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrgName.Focus();
                return false;
            }

            // 2. Kiểm tra Phone (Nếu nhập)
            string phone = txtPhone.Text.Trim();
            if (!string.IsNullOrEmpty(phone))
            {
                // Regex: Chỉ chứa số, độ dài 9-12
                if (!Regex.IsMatch(phone, @"^\d{9,12}$"))
                {
                    MessageBox.Show("Phone chỉ chứa số và độ dài từ 9-12 ký tự.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return false;
                }
            }

            // 3. Kiểm tra Email (Nếu nhập)
            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                // Regex kiểm tra định dạng email cơ bản
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailPattern))
                {
                    MessageBox.Show("Email không đúng định dạng.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        // ==========================================
        // HÀM KIỂM TRA TRÙNG TÊN TRONG DATABASE
        // ==========================================
        private bool IsOrgNameExist(string orgName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM ORGANIZATION WHERE OrgName = @OrgName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrgName", orgName);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
                return true; // Trả về true để chặn lưu nếu lỗi
            }
        }

        // ==========================================
        // HÀM LƯU DỮ LIỆU VÀO DATABASE
        // ==========================================
        private bool SaveOrganization()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Query Insert và lấy về ID vừa tạo (SCOPE_IDENTITY)
                    string query = @"INSERT INTO ORGANIZATION (OrgName, Address, Phone, Email) 
                                     VALUES (@OrgName, @Address, @Phone, @Email);
                                     SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Sử dụng Parameters để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@OrgName", txtOrgName.Text.Trim());

                        // Xử lý giá trị NULL cho các trường không bắt buộc
                        cmd.Parameters.AddWithValue("@Address",
                            string.IsNullOrEmpty(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim());

                        cmd.Parameters.AddWithValue("@Phone",
                            string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim());

                        cmd.Parameters.AddWithValue("@Email",
                            string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());

                        // Thực thi và lấy ID
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            currentOrgID = Convert.ToInt32(result);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
            return false;
        }

        // ==========================================
        // 5.2 XỬ LÝ NÚT DIRECTOR
        // ==========================================
        private void btnDirector_Click(object sender, EventArgs e)
        {
            // Yêu cầu: Mở form Director Management và truyền Organization vừa tạo
            // Giả sử bạn có Form tên là FrmDirectorManagement

            MessageBox.Show($"Đang mở Form Director cho Organization ID: {currentOrgID}", "Info");

            // Code thực tế sẽ như sau (nếu đã tạo Form Director):
            /*
            FrmDirectorManagement frmDirector = new FrmDirectorManagement(currentOrgID);
            this.Hide();
            frmDirector.ShowDialog();
            this.Show();
            */
        }

        // ==========================================
        // 5.3 XỬ LÝ NÚT BACK
        // ==========================================
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại, quay về màn hình trước
            this.Close();
        }
    }
}
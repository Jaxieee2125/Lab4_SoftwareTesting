using System.Text.RegularExpressions;

namespace BaiTapBonusBuoi4
{
    public class OrganizationValidator
    {
        // Hàm kiểm tra hợp lệ chung, trả về chuỗi lỗi (nếu rỗng là không lỗi)
        public string ValidateOrganization(string name, string phone, string email)
        {
            // 1. Kiểm tra OrgName
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Organization Name không được để trống.";
            }
            if (name.Trim().Length < 3 || name.Trim().Length > 255)
            {
                return "Độ dài Organization Name phải từ 3 đến 255 ký tự.";
            }

            // 2. Kiểm tra Phone (Nếu có nhập)
            if (!string.IsNullOrEmpty(phone))
            {
                if (!Regex.IsMatch(phone, @"^\d{9,12}$"))
                {
                    return "Phone chỉ chứa số và độ dài từ 9-12 ký tự.";
                }
            }

            // 3. Kiểm tra Email (Nếu có nhập)
            if (!string.IsNullOrEmpty(email))
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailPattern))
                {
                    return "Email không đúng định dạng.";
                }
            }

            return ""; // Không có lỗi
        }
    }
}
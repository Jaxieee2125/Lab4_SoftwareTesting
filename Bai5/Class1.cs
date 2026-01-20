using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai5
{
    public class HocVien
    {
        // Thuộc tính thông tin cơ bản
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public string QueQuan { get; set; }

        // Điểm 3 môn chính
        public double DiemMon1 { get; set; }
        public double DiemMon2 { get; set; }
        public double DiemMon3 { get; set; }

        // Constructor
        public HocVien(string maSo, string hoTen, string queQuan, double d1, double d2, double d3)
        {
            MaSo = maSo;
            HoTen = hoTen;
            QueQuan = queQuan;

            // Có thể thêm validate điểm 0-10 nếu cần
            if (d1 < 0 || d1 > 10 || d2 < 0 || d2 > 10 || d3 < 0 || d3 > 10)
                throw new ArgumentException("Điểm phải nằm trong khoảng 0 đến 10");

            DiemMon1 = d1;
            DiemMon2 = d2;
            DiemMon3 = d3;
        }

        // Tính điểm trung bình
        public double TinhDiemTrungBinh()
        {
            return Math.Round((DiemMon1 + DiemMon2 + DiemMon3) / 3, 2);
        }

        // Kiểm tra điều kiện nhận học bổng
        // Logic: ĐTB >= 8.0 VÀ Không môn nào < 5.0
        public bool KiemTraHocBong()
        {
            double dtb = TinhDiemTrungBinh();
            bool khongMonNaoDuoi5 = (DiemMon1 >= 5.0) && (DiemMon2 >= 5.0) && (DiemMon3 >= 5.0);

            return (dtb >= 8.0) && khongMonNaoDuoi5;
        }
    }

    public class TrungTam
    {
        // Hàm nhận vào danh sách tất cả học viên và trả về danh sách được học bổng
        public List<HocVien> LayDanhSachNhanHocBong(List<HocVien> danhSachGoc)
        {
            // Sử dụng LINQ để lọc
            return danhSachGoc.Where(hv => hv.KiemTraHocBong()).ToList();
        }
    }
}

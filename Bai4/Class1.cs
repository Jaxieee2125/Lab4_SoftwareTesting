using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai4
{
    public class Diem
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Diem(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    // Lớp Hình Chữ Nhật
    public class HinhChuNhat
    {
        public Diem TopLeft { get; set; }
        public Diem BottomRight { get; set; }

        public HinhChuNhat(Diem topLeft, Diem bottomRight)
        {
            // Có thể thêm validation nếu cần đảm bảo topLeft luôn ở trên/trái
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        // 1. Tính diện tích
        public double TinhDienTich()
        {
            // Dùng Math.Abs để đảm bảo chiều dài/rộng luôn dương 
            // bất kể hệ tọa độ (Y hướng lên hay hướng xuống)
            double width = Math.Abs(BottomRight.X - TopLeft.X);
            double height = Math.Abs(TopLeft.Y - BottomRight.Y);

            return width * height;
        }

        // 2. Kiểm tra giao nhau (Intersection)
        public bool KiemTraGiaoNhau(HinhChuNhat other)
        {
            // Xác định khung bao (Min/Max) của hình chữ nhật hiện tại (This)
            double r1_minX = Math.Min(this.TopLeft.X, this.BottomRight.X);
            double r1_maxX = Math.Max(this.TopLeft.X, this.BottomRight.X);
            double r1_minY = Math.Min(this.TopLeft.Y, this.BottomRight.Y);
            double r1_maxY = Math.Max(this.TopLeft.Y, this.BottomRight.Y);

            // Xác định khung bao của hình chữ nhật kia (Other)
            double r2_minX = Math.Min(other.TopLeft.X, other.BottomRight.X);
            double r2_maxX = Math.Max(other.TopLeft.X, other.BottomRight.X);
            double r2_minY = Math.Min(other.TopLeft.Y, other.BottomRight.Y);
            double r2_maxY = Math.Max(other.TopLeft.Y, other.BottomRight.Y);

            // Điều kiện KHÔNG giao nhau:
            // 1. R1 nằm hoàn toàn bên trái R2
            // 2. R1 nằm hoàn toàn bên phải R2
            // 3. R1 nằm hoàn toàn bên dưới R2
            // 4. R1 nằm hoàn toàn bên trên R2

            bool isSeparated = (r1_maxX < r2_minX) || // R1 bên trái R2
                               (r1_minX > r2_maxX) || // R1 bên phải R2
                               (r1_maxY < r2_minY) || // R1 bên dưới R2
                               (r1_minY > r2_maxY);   // R1 bên trên R2

            // Nếu không bị tách biệt -> Có giao nhau
            return !isSeparated;
        }
    }
}

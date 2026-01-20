using NUnit.Framework;
using Bai4; // Namespace chứa source code trên

namespace GeometryApp.Tests
{
    [TestFixture]
    public class HinhChuNhatTests
    {
        // --------------------------------------------------------
        // TEST TÍNH DIỆN TÍCH
        // --------------------------------------------------------

        [Test]
        public void TinhDienTich_ValidCoordinates_ReturnsCorrectArea()
        {
            // Arrange: Hình chữ nhật từ (0, 5) đến (5, 0) -> Rộng 5, Cao 5
            Diem p1 = new Diem(0, 5);
            Diem p2 = new Diem(5, 0);
            HinhChuNhat hcn = new HinhChuNhat(p1, p2);

            // Act
            double dienTich = hcn.TinhDienTich();

            // Assert
            Assert.AreEqual(25.0, dienTich);
        }

        [Test]
        public void TinhDienTich_NegativeCoordinates_ReturnsPositiveArea()
        {
            // Arrange: HCN ở góc phần tư thứ 3. (-5, -2) đến (-2, -5) -> Rộng 3, Cao 3
            Diem p1 = new Diem(-5, -2);
            Diem p2 = new Diem(-2, -5);
            HinhChuNhat hcn = new HinhChuNhat(p1, p2);

            // Act
            double dienTich = hcn.TinhDienTich();

            // Assert
            Assert.AreEqual(9.0, dienTich);
        }

        // --------------------------------------------------------
        // TEST KIỂM TRA GIAO NHAU
        // --------------------------------------------------------

        // Case 1: Hai hình rời nhau hoàn toàn (Không giao)
        [Test]
        public void KiemTraGiaoNhau_NoIntersection_ReturnsFalse()
        {
            // R1: (0, 10) -> (10, 0)
            HinhChuNhat r1 = new HinhChuNhat(new Diem(0, 10), new Diem(10, 0));
            // R2: (20, 10) -> (30, 0) (Nằm xa về bên phải)
            HinhChuNhat r2 = new HinhChuNhat(new Diem(20, 10), new Diem(30, 0));

            bool result = r1.KiemTraGiaoNhau(r2);

            Assert.IsFalse(result, "Hai hình chữ nhật rời nhau nên phải trả về False");
        }

        // Case 2: Hai hình cắt nhau bình thường (Có giao)
        [Test]
        public void KiemTraGiaoNhau_Intersecting_ReturnsTrue()
        {
            // R1: (0, 10) -> (10, 0)
            HinhChuNhat r1 = new HinhChuNhat(new Diem(0, 10), new Diem(10, 0));
            // R2: (5, 15) -> (15, 5) (Giao một góc phần tư)
            HinhChuNhat r2 = new HinhChuNhat(new Diem(5, 15), new Diem(15, 5));

            bool result = r1.KiemTraGiaoNhau(r2);

            Assert.IsTrue(result, "Hai hình chữ nhật cắt nhau nên phải trả về True");
        }

        // Case 3: Một hình nằm lọt lòng trong hình kia (Có giao)
        [Test]
        public void KiemTraGiaoNhau_OneInsideAnother_ReturnsTrue()
        {
            // R1: (0, 10) -> (10, 0) (Lớn)
            HinhChuNhat r1 = new HinhChuNhat(new Diem(0, 10), new Diem(10, 0));
            // R2: (2, 8) -> (8, 2) (Nhỏ, nằm trong R1)
            HinhChuNhat r2 = new HinhChuNhat(new Diem(2, 8), new Diem(8, 2));

            bool result = r1.KiemTraGiaoNhau(r2);

            Assert.IsTrue(result, "Hình nằm trong hình khác vẫn tính là giao nhau");
        }

        // Case 4: Tiếp xúc cạnh (Touching)
        // Lưu ý: Tùy định nghĩa bài toán mà chạm cạnh tính là True hay False. 
        // Trong toán học tập hợp thì giao != rỗng -> True. Code trên dùng <= >= nên là True.
        [Test]
        public void KiemTraGiaoNhau_TouchingEdges_ReturnsTrue()
        {
            // R1: (0, 10) -> (5, 0)
            HinhChuNhat r1 = new HinhChuNhat(new Diem(0, 10), new Diem(5, 0));
            // R2: (5, 10) -> (10, 0) (Chạm nhau tại cạnh x = 5)
            HinhChuNhat r2 = new HinhChuNhat(new Diem(5, 10), new Diem(10, 0));

            bool result = r1.KiemTraGiaoNhau(r2);

            Assert.IsTrue(result, "Hai hình chạm cạnh nhau (chung biên) thường được tính là giao nhau");
        }
    }
}
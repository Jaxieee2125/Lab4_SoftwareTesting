using NUnit.Framework;
using StudentManagement;
using System.Collections.Generic;
using System.Linq;
using Bai5;

namespace StudentManagement.Tests
{
    [TestFixture]
    public class HocVienTests
    {
        // -----------------------------------------------------------
        // TEST CASE 1: KIỂM TRA TÍNH ĐIỂM TRUNG BÌNH
        // -----------------------------------------------------------
        [Test]
        public void TinhDiemTrungBinh_CorrectCalculation()
        {
            // Input: 8, 9, 10. (Sum = 27, Avg = 9.0)
            HocVien hv = new HocVien("HV01", "Nguyen Van A", "HN", 8.0, 9.0, 10.0);

            double result = hv.TinhDiemTrungBinh();

            Assert.AreEqual(9.0, result);
        }

        // -----------------------------------------------------------
        // TEST CASE 2: LOGIC XÉT HỌC BỔNG (QUAN TRỌNG)
        // -----------------------------------------------------------

        // Trường hợp 1: Đạt chuẩn (ĐTB cao, không môn liệt)
        [Test]
        public void KiemTraHocBong_Qualified_ReturnsTrue()
        {
            // ĐTB = 9.0, Môn thấp nhất = 8.0 (> 5) -> ĐẬU
            HocVien hv = new HocVien("HV01", "A", "HN", 8.0, 9.0, 10.0);
            Assert.IsTrue(hv.KiemTraHocBong());
        }

        // Trường hợp 2: Trượt do ĐTB thấp (< 8.0) dù không có môn liệt
        [Test]
        public void KiemTraHocBong_LowAverage_ReturnsFalse()
        {
            // ĐTB = 7.0, Môn thấp nhất = 7.0 -> TRƯỢT vì ĐTB < 8
            HocVien hv = new HocVien("HV02", "B", "HCM", 7.0, 7.0, 7.0);
            Assert.IsFalse(hv.KiemTraHocBong());
        }

        // Trường hợp 3: Trượt do có 1 môn < 5.0 (Dù ĐTB rất cao)
        // Đây là trường hợp dễ sai logic nhất (quên check điểm liệt)
        [Test]
        public void KiemTraHocBong_OneSubjectBelowFive_ReturnsFalse()
        {
            // Môn 1 = 10, Môn 2 = 10, Môn 3 = 4.
            // ĐTB = 24/3 = 8.0 (Đủ chuẩn ĐTB)
            // Nhưng có môn 4 điểm (< 5) -> TRƯỢT
            HocVien hv = new HocVien("HV03", "C", "DN", 10.0, 10.0, 4.0);

            // Verify ĐTB là 8.0
            Assert.AreEqual(8.0, hv.TinhDiemTrungBinh());
            // Verify Học bổng là False
            Assert.IsFalse(hv.KiemTraHocBong(), "Dù ĐTB >= 8 nhưng có môn < 5 phải trượt");
        }

        // Trường hợp 4: Vùng biên (Boundary Value)
        // Đúng bằng 8.0 và đúng bằng 5.0
        [Test]
        public void KiemTraHocBong_BoundaryValues_ReturnsTrue()
        {
            // Môn 1 = 5, Môn 2 = 9, Môn 3 = 10.
            // ĐTB = 24/3 = 8.0.
            // Môn thấp nhất = 5.0 (Không dưới 5).
            // -> VỪA ĐỦ ĐẬU
            HocVien hv = new HocVien("HV04", "D", "HP", 5.0, 9.0, 10.0);

            Assert.IsTrue(hv.KiemTraHocBong());
        }
    }

    // -----------------------------------------------------------
    // TEST CASE 3: KIỂM TRA CHỨC NĂNG LỌC DANH SÁCH (CÂU A)
    // -----------------------------------------------------------
    [TestFixture]
    public class TrungTamTests
    {
        [Test]
        public void LayDanhSachNhanHocBong_FilterCorrectly()
        {
            // Arrange: Tạo danh sách 3 học viên
            var list = new List<HocVien>
            {
                new HocVien("1", "Pass", "A", 9, 9, 9),      // Đậu
                new HocVien("2", "FailAvg", "B", 6, 6, 6),   // Trượt do điểm TB thấp
                new HocVien("3", "FailSub", "C", 10, 10, 2)  // Trượt do điểm liệt
            };

            TrungTam trungTam = new TrungTam();

            // Act
            var result = trungTam.LayDanhSachNhanHocBong(list);

            // Assert
            Assert.AreEqual(1, result.Count, "Chỉ nên có 1 học viên đậu");
            Assert.AreEqual("Pass", result[0].HoTen, "Học viên đậu phải là người tên 'Pass'");
        }
    }
}
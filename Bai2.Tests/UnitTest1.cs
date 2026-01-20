using NUnit.Framework;
using System;
using System.Collections.Generic;
using Bai2; // Đừng quên using namespace chứa class Polynomial

namespace MathApp.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        // ----------------------------------------------------
        // TEST CASE CHO CONSTRUCTOR (HÀM KHỞI TẠO)
        // ----------------------------------------------------

        [Test]
        public void Constructor_ValidInput_ShouldCreateInstance()
        {
            // Input: Bậc 2, có 3 hệ số -> Hợp lệ
            int n = 2;
            List<int> a = new List<int> { 1, 2, 3 };

            // Kỳ vọng: Không ném ra lỗi nào
            Assert.DoesNotThrow(() => new Polynomial(n, a));
        }

        [Test]
        public void Constructor_NotEnoughCoefficients_ThrowsException()
        {
            // Input: Bậc 2, nhưng chỉ có 2 hệ số (thiếu 1) -> Sai
            int n = 2;
            List<int> a = new List<int> { 1, 2 };

            // Kỳ vọng: Ném lỗi ArgumentException với message "Invalid Data"
            var ex = Assert.Throws<ArgumentException>(() => new Polynomial(n, a));
            Assert.AreEqual("Invalid Data", ex.Message);
        }

        [Test]
        public void Constructor_NegativeN_ThrowsException()
        {
            // Input: Bậc n là số âm -> Sai theo yêu cầu đề bài
            int n = -1;
            List<int> a = new List<int>(); // List rỗng để count = 0, khớp với n+1 = 0

            // Kỳ vọng: Ném lỗi ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Polynomial(n, a));
            Assert.AreEqual("Invalid Data", ex.Message);
        }

        // ----------------------------------------------------
        // TEST CASE CHO HÀM CAL (TÍNH TOÁN)
        // ----------------------------------------------------

        [Test]
        public void Cal_CalculateValue_ReturnsCorrectResult()
        {
            // Đa thức: P(x) = 1 + 2x + 3x^2 (với n=2)
            // Tại x = 2: 1 + 2(2) + 3(4) = 1 + 4 + 12 = 17
            int n = 2;
            List<int> a = new List<int> { 1, 2, 3 };
            Polynomial poly = new Polynomial(n, a);

            int result = poly.Cal(2.0);

            Assert.AreEqual(17, result);
        }

        [Test]
        public void Cal_WithZeroX_ReturnsFirstCoefficient()
        {
            // Tại x = 0, kết quả luôn là hệ số đầu tiên a[0]
            int n = 1;
            List<int> a = new List<int> { 10, 5 }; // 10 + 5x
            Polynomial poly = new Polynomial(n, a);

            int result = poly.Cal(0);

            Assert.AreEqual(10, result);
        }
    }
}
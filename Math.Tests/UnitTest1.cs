using PowerUnitTest; // Gọi thư viện chứa hàm Power
using NUnit.Framework;

namespace MathApp.Tests
{
    public class PowerTests
    {
        [SetUp]
        public void Setup()
        {
            // Code chạy trước mỗi bài test (nếu cần)
        }

        // Test Case 1: n = 0
        [Test]
        public void Test_Power_N_Equals_Zero()
        {
            double result = Calculator.Power(100, 0);
            Assert.AreEqual(1.0, result);
        }

        // Test Case 2: n > 0 (Test case này sẽ FAIL do lỗi trong đề bài)
        [Test]
        public void Test_Power_N_Positive()
        {
            // Thử tính 2^3. Kết quả đúng phải là 8.
            // Nhưng code đề bài: 3 * 2 * 1 = 6.
            double result = Calculator.Power(2.0, 3);

            // Tham số thứ 3 (delta) là sai số cho phép khi so sánh số thực
            Assert.AreEqual(8.0, result, 0.0001, "Lỗi: Hàm tính sai lũy thừa dương!");
        }

        // Test Case 3: n < 0
        [Test]
        public void Test_Power_N_Negative()
        {
            // Thử tính 2^-1 = 0.5
            double result = Calculator.Power(2.0, -1);
            Assert.AreEqual(0.5, result, 0.0001);
        }
    }
}
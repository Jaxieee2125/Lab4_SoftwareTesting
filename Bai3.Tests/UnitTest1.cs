using NUnit.Framework;
using System;
using Bai3; // Namespace chứa class Radix

namespace MathApp.Tests
{
    [TestFixture]
    public class RadixTests
    {
        // ---------------------------------------------------------
        // NHÓM 1: KIỂM THỬ CONSTRUCTOR (INPUT VALIDATION)
        // ---------------------------------------------------------

        [Test]
        public void Constructor_NegativeNumber_ThrowsException()
        {
            // Input: -5 (Số âm)
            // Kỳ vọng: Ném lỗi ArgumentException("Incorrect Value")
            var ex = Assert.Throws<ArgumentException>(() => new Radix(-5));
            Assert.AreEqual("Incorrect Value", ex.Message);
        }

        [Test]
        public void Constructor_PositiveNumber_CreatesInstance()
        {
            // Input: 10 (Hợp lệ)
            // Kỳ vọng: Không có lỗi
            Assert.DoesNotThrow(() => new Radix(10));
        }

        // ---------------------------------------------------------
        // NHÓM 2: KIỂM THỬ VALIDATE RADIX (CƠ SỐ)
        // ---------------------------------------------------------

        [Test]
        public void Convert_RadixLessThanTwo_ThrowsException()
        {
            Radix r = new Radix(10);
            // Input: Cơ số 1 (Nhỏ hơn 2) -> Lỗi
            var ex = Assert.Throws<ArgumentException>(() => r.ConvertDecimalToAnother(1));
            Assert.AreEqual("Invalid Radix", ex.Message);
        }

        [Test]
        public void Convert_RadixGreaterThanSixteen_ThrowsException()
        {
            Radix r = new Radix(10);
            // Input: Cơ số 17 (Lớn hơn 16) -> Lỗi
            var ex = Assert.Throws<ArgumentException>(() => r.ConvertDecimalToAnother(17));
            Assert.AreEqual("Invalid Radix", ex.Message);
        }

        // ---------------------------------------------------------
        // NHÓM 3: KIỂM THỬ LOGIC CHUYỂN ĐỔI (FUNCTIONAL TEST)
        // ---------------------------------------------------------

        // Test Case: Chuyển sang hệ Nhị phân (Binary - Base 2)
        [Test]
        public void Convert_ToBinary_ReturnsCorrectString()
        {
            // Input: 10 (Thập phân)
            // Kỳ vọng Binary: 1010 (8 + 2)
            Radix r = new Radix(10);
            string result = r.ConvertDecimalToAnother(2);

            Assert.AreEqual("1010", result);
        }

        // Test Case: Chuyển sang hệ Bát phân (Octal - Base 8)
        [Test]
        public void Convert_ToOctal_ReturnsCorrectString()
        {
            // Input: 8 (Thập phân)
            // Kỳ vọng Octal: 10
            Radix r = new Radix(8);
            string result = r.ConvertDecimalToAnother(8);

            Assert.AreEqual("10", result);
        }

        // Test Case: Chuyển sang hệ Thập lục phân (Hex - Base 16)
        // Trường hợp đơn giản (0-9)
        [Test]
        public void Convert_ToHex_SimpleNumber()
        {
            // Input: 255
            // Kỳ vọng Hex: FF
            Radix r = new Radix(255);
            string result = r.ConvertDecimalToAnother(16);

            Assert.AreEqual("FF", result);
        }

        // Test Case: Chuyển sang hệ Thập lục phân (Hex - Base 16)
        // Trường hợp có ký tự chữ (A-F)
        [Test]
        public void Convert_ToHex_WithLetters()
        {
            // Input: 31
            // Giải thích: 31 / 16 = 1 dư 15 (F) -> Kết quả 1F
            Radix r = new Radix(31);
            string result = r.ConvertDecimalToAnother(16);

            Assert.AreEqual("1F", result);
        }

        // Test Case: Giá trị mặc định (Default Radix = 2)
        [Test]
        public void Convert_DefaultRadix_IsBinary()
        {
            // Input: 5 (Binary là 101)
            Radix r = new Radix(5);
            // Gọi hàm không truyền tham số
            string result = r.ConvertDecimalToAnother();

            Assert.AreEqual("101", result);
        }
    }
}
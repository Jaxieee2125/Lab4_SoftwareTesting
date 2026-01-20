using NUnit.Framework;
using BaiTapBonusBuoi4; // Nhớ using namespace của project chính

namespace BaiTapBonusBuoi4.Tests
{
    [TestFixture]
    public class OrganizationTests
    {
        private OrganizationValidator _validator;

        [SetUp] // Chạy trước mỗi test case
        public void Setup()
        {
            _validator = new OrganizationValidator();
        }

        // --- TEST CHO ORG NAME ---

        [Test]
        public void Validate_NameIsEmpty_ReturnsError()
        {
            // Arrange (Chuẩn bị)
            string name = "";
            string phone = "0901234567";
            string email = "test@email.com";

            // Act (Thực hiện)
            string result = _validator.ValidateOrganization(name, phone, email);

            // Assert (Kiểm tra kết quả)
            Assert.AreEqual("Organization Name không được để trống.", result);
        }

        [Test]
        public void Validate_NameTooShort_ReturnsError()
        {
            string result = _validator.ValidateOrganization("AB", "", "");
            Assert.AreEqual("Độ dài Organization Name phải từ 3 đến 255 ký tự.", result);
        }

        [Test]
        public void Validate_NameValid_ReturnsEmptyString()
        {
            string result = _validator.ValidateOrganization("FPT Software", "", "");
            Assert.IsEmpty(result); // Rỗng nghĩa là không có lỗi -> Pass
        }

        // --- TEST CHO PHONE ---

        [Test]
        public void Validate_PhoneContainsLetters_ReturnsError()
        {
            string result = _validator.ValidateOrganization("Valid Name", "0909abc", "");
            Assert.AreEqual("Phone chỉ chứa số và độ dài từ 9-12 ký tự.", result);
        }

        [Test]
        public void Validate_PhoneTooShort_ReturnsError()
        {
            string result = _validator.ValidateOrganization("Valid Name", "12345", "");
            Assert.AreEqual("Phone chỉ chứa số và độ dài từ 9-12 ký tự.", result);
        }

        [Test]
        public void Validate_PhoneBoundary9_ReturnsSuccess()
        {
            string result = _validator.ValidateOrganization("Valid Name", "123456789", "");
            Assert.IsEmpty(result);
        }

        [Test]
        public void Validate_PhoneBoundary12_ReturnsSuccess()
        {
            string result = _validator.ValidateOrganization("Valid Name", "123456789012", "");
            Assert.IsEmpty(result);
        }

        // --- TEST CHO EMAIL ---

        [Test]
        public void Validate_EmailInvalidFormat_ReturnsError()
        {
            string result = _validator.ValidateOrganization("Valid Name", "", "invalid-email");
            Assert.AreEqual("Email không đúng định dạng.", result);
        }

        [Test]
        public void Validate_EmailValid_ReturnsSuccess()
        {
            string result = _validator.ValidateOrganization("Valid Name", "", "student@fpt.edu.vn");
            Assert.IsEmpty(result);
        }

        // --- TEST TỔNG HỢP ---

        [Test]
        public void Validate_AllFieldsCorrect_ReturnsSuccess()
        {
            string result = _validator.ValidateOrganization("Company A", "0987654321", "contact@companya.com");
            Assert.IsEmpty(result);
        }
    }
}
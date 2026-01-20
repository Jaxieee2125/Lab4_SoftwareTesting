using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai2
{
    public class Polynomial
    {
        private int n;
        private List<int> a;

        // Constructor
        public Polynomial(int n, List<int> a)
        {
            // Yêu cầu đề bài: Nếu n âm HOẶC nhập không đủ n + 1 hệ số -> Lỗi
            // Code trong ảnh chỉ check count, ở đây tôi bổ sung check n < 0 để đúng đề bài
            if (n < 0 || a.Count != n + 1)
            {
                throw new ArgumentException("Invalid Data");
            }

            this.n = n;
            this.a = a;
        }

        // Hàm tính giá trị đa thức
        public int Cal(double x)
        {
            int result = 0;
            for (int i = 0; i <= this.n; i++)
            {
                // Công thức: result += a[i] * x^i
                // Lưu ý: Code trong ảnh ép kiểu (int) ngay tại từng hạng tử
                result += (int)(a[i] * Math.Pow(x, i));
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai3
{
    public class Radix
    {
        private int number;

        // Constructor: Nhận vào số nguyên dương
        public Radix(int number)
        {
            if (number < 0)
                throw new ArgumentException("Incorrect Value");

            this.number = number;
        }

        // Phương thức chuyển đổi sang cơ số khác (mặc định là binary - cơ số 2)
        public string ConvertDecimalToAnother(int radix = 2)
        {
            int n = this.number;

            // Kiểm tra điều kiện cơ số (2 <= k <= 16)
            if (radix < 2 || radix > 16)
                throw new ArgumentException("Invalid Radix");

            List<string> result = new List<string>();

            while (n > 0)
            {
                int value = n % radix; // Lấy phần dư

                if (value < 10)
                {
                    result.Add(value.ToString());
                }
                else
                {
                    // Chuyển đổi các số dư từ 10-15 thành A-F
                    switch (value)
                    {
                        case 10: result.Add("A"); break;
                        case 11: result.Add("B"); break;
                        case 12: result.Add("C"); break;
                        case 13: result.Add("D"); break;
                        case 14: result.Add("E"); break;
                        case 15: result.Add("F"); break;
                    }
                }
                n /= radix; // Chia lấy phần nguyên để tiếp tục vòng lặp
            }

            result.Reverse(); // Đảo ngược danh sách số dư
            return string.Join("", result.ToArray());
        }
    }
}

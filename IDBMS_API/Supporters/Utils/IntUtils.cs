using System.Globalization;

namespace IDBMS_API.Supporters.Utils
{
    public class IntUtils
    {
        public static string ConvertStringToMoney(decimal Amount)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string a = Amount.ToString("#,###", cul.NumberFormat);
            return a;
        }
        public static string IntToRoman(int num)
        {
            if (num < 1 || num > 3999)
                throw new ArgumentOutOfRangeException("num", "Value must be between 1 and 3999");

            string[] romanSymbols = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            int[] values = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };

            List<string> result = new List<string>();

            for (int i = values.Length - 1; i >= 0; i--)
            {
                while (num >= values[i])
                {
                    num -= values[i];
                    result.Add(romanSymbols[i]);
                }
            }

            return string.Join("", result);
        } 
        static public string ConvertNumberToVietnamese(int number)
        {
            if (number == 0)
            {
                return "không";
            }

            string[] unit = { "", "nghìn", "triệu", "tỷ" };

            string result = "";

            int i = 0;

            while (number > 0)
            {
                int chunk = number % 1000;
                if (chunk > 0)
                {
                    result = $"{ConvertChunkToVietnamese(chunk)} {unit[i]} {result}";
                }

                number /= 1000;
                i++;
            }

            return result.Trim();
        }

        public static string ConvertChunkToVietnamese(int chunk)
        {
            string[] digitNames = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            int hundreds = chunk / 100;
            int tens = (chunk % 100) / 10;
            int ones = chunk % 10;

            string result = "";

            if (hundreds > 0)
            {
                result += $"{digitNames[hundreds]} trăm ";
            }

            if (tens > 1)
            {
                result += $"{digitNames[tens]} mươi ";
            }
            else if (tens == 1)
            {
                result += "mười ";
            }

            if (ones > 0)
            {
                result += digitNames[ones];
            }

            return result.Trim();
        }
    }
}

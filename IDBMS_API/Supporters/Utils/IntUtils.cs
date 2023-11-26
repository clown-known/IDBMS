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

            if (ones > 0 && tens != 1)
            {
                result += digitNames[ones];
            }

            return result.Trim();
        }
    }
}

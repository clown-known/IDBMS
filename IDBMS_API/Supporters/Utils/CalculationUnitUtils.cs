using BusinessObject.Enums;

namespace IDBMS_API.Supporters.Utils
{
    public class CalculationUnitUtils
    {
        public static string ConvertVietnamese(CalculationUnit unit)
        {
            switch (unit)
            {
                case CalculationUnit.CubicMeter:
                    return "m3";
                case CalculationUnit.SquareMeter:
                    return "m2";
                case CalculationUnit.Meter:
                    return "Mét";
                case CalculationUnit.SquareCentimeter:
                    return "cm2";
                case CalculationUnit.Centimeter:
                    return "cm";
                case CalculationUnit.Set:
                    return "Bộ";
                case CalculationUnit.Unit:
                    return "Cái";
                case CalculationUnit.Part:
                    return "Phần";
                case CalculationUnit.Bag:
                    return "Túi";
                case CalculationUnit.Kilogram:
                    return "Kg";
                case CalculationUnit.Liter:
                    return "Lít";
                case CalculationUnit.Barrel:
                    return "Thùng";
                case CalculationUnit.Frame:
                    return "Khung";
                case CalculationUnit.Roll:
                    return "Cuộn";
                case CalculationUnit.Thread:
                    return "Dây";
                case CalculationUnit.Label:
                    return "Nhãn";
                case CalculationUnit.Node:
                    return "Nút";
                case CalculationUnit.Bar:
                    return "Thanh";
                case CalculationUnit.Box:
                    return "Hộp";
                case CalculationUnit.Sheet:
                    return "Tấm";
                case CalculationUnit.TransportationUnit:
                    return "Đơn Vị Vận Chuyển";
                case CalculationUnit.Worker:
                    return "Người";
                case CalculationUnit.Kilometer:
                    return "Km";
                case CalculationUnit.Modun:
                    return "Mô Đun";
                case CalculationUnit.Unclassified:
                    return "Không Xác Định";
                case CalculationUnit.Shelf:
                    return "Kệ";
                default:
                    return "Không Xác Định";
            }
        }
    }
}

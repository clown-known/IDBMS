using BusinessObject.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using IDBMS_API.Constants;
using IDBMS_API.Supporters.Utils;

namespace IDBMS_API.DTOs.Request
{
    public class ContractRequest
    {
        public string ACompanyName { get; set; }
        public string ACompanyAddress { get; set; }
        public string AOwnerName { get; set; }
        public string APhone { get; set; }
        public string AEmail { get; set; }
         
        public string BCompanyName { get; set; }
        public string BCompanyAddress { get; set; }
        public string BSwiftCode { get; set; }
        public string BRepresentedBy { get; set; }
        public string BCompanyPhone { get; set; }
        public string BPosition { get; set; }
        public string BEmail { get; set; }

        public int NumOfCopie { get; set; }
        public int NumOfA { get; set; }
        public int NumOfB { get; set; }
        public int EstimateDays { get; set; }

        public string ProjectName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Value { get; set; }
    }
}

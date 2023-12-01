namespace IDBMS_API.DTOs.Response
{
    public class ContractForCompanyResponse
    {
        public string ACompanyName { get; set; }
        public string ACompanyAddress { get; set; }
        public string AOwnerName { get; set; }
        public string Aposition { get; set; }
        public string APhone { get; set; }
        public string AEmail { get; set; }

        public string BCompanyName { get; set; }
        public string BCompanyAddress { get; set; }
        public string BSwiftCode { get; set; }
        public string BRepresentedBy { get; set; }
        public string BCompanyPhone { get; set; }
        public string BPosition { get; set; }
        public string BEmail { get; set; }

        public int EstimateDays { get; set; }

        public string ProjectName { get; set; }
        public decimal Value { get; set; }
    }
}

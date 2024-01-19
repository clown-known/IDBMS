namespace IDBMS_API.DTOs.Response
{
    public class ContractForCustomerResponse
    {
        public string? CustomerName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }


        public string? BCompanyName { get; set; }
        public string? BCompanyAddress { get; set; }
        public string? BSwiftCode { get; set; }
        public string? BRepresentedBy { get; set; }
        public string? BCompanyPhone { get; set; }
        public string? BPosition { get; set; }
        public string? BEmail { get; set; }

        public int? EstimateDays { get; set; }

        public string? ProjectName { get; set; }
        public decimal? Value { get; set; }
    }
}

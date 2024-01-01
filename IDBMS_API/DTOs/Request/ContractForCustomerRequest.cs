using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class ContractForCustomerRequest
    {
        public string CustomerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdentityCode { get; set; }
        public DateTime CodeCreatedDate { get; set; }
        public string IssuedBy { get; set; }
        public string RegisteredPlaceOfPermanentResidence { get; set; }

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

using BusinessObject.Models;

namespace IDBMS_API.DTOs.Response
{
    public class PaymentStageResponse
    {
        public PaymentStage Stage { get; set; }

        public bool OpenAllowed { get; set; } = false;
        public bool ReopenAllowed { get; set; } = false;
        public bool CloseAllowed { get; set; } = false;
        public bool SuspendAllowed { get; set; } = false;
    }
}

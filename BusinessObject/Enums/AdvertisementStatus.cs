using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Enums
{
    public enum AdvertisementStatus
    {
        None = 0,
        PendingRequest = 1,
        NotAllowed = 2,
        Allowed = 3,
        Public = 4,
    }
}

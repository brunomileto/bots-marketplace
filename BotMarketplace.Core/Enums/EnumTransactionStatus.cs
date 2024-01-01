using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMarketplace.Core.Enums
{
    public enum EnumTransactionStatus
    {
        Success,
        Created,
        Deleted,
        Cancelled_by_seller,
        Cancelled_by_buyer,
        Waiting_payment,
        Waiting_liberation,
        Cancelled
    }
}

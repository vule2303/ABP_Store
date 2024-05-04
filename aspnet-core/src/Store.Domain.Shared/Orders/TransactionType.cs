using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Orders
{
    public enum TransactionType
    {
        ConfirmOrder,
        StartProcessing,
        FinishOrder,
        CancelOrder
    }
}

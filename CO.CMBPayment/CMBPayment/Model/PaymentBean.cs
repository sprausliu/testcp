using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMBPayment
{
    public class PaymentBean : BasePagingBean
    {
        public string BatchId { get; set; }
    }

    public class PaymentItemBean : BaseInfoBean
    {


    }
}
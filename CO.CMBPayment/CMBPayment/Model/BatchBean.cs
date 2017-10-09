using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMBPayment
{
    public class BatchBean : BasePagingBean
    {
        public string BatchId { get; set; }
        public string StatusId { get; set; }
    }

    public class BatchItemBean : BaseInfoBean
    {
        public string BatchId { get; set; }
        public string StatusId { get; set; }
        public string PayCnt { get; set; }
        public string TtlAmt { get; set; }
        public string Appr1Id { get; set; }
        public string Appr2Id { get; set; }
        public string RejcId { get; set; }

    }
}
using System;
using System.Collections.Generic;

#nullable disable

namespace wematest.Data
{
    public partial class Customer
    {
        public int CustId { get; set; }
        public string CustEmail { get; set; }
        public string CustPhoneNumber { get; set; }
        public int? CustSor { get; set; }
        public int? CustLga { get; set; }
        public int CustRegStatus { get; set; }

        public virtual Stateofresidence CustSorNavigation { get; set; }
    }
}

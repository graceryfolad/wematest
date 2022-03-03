using System;
using System.Collections.Generic;

#nullable disable

namespace wematest.Data
{
    public partial class Otp
    {
        public int OtpId { get; set; }
        public int? OtpCustomer { get; set; }
        public string OptValue { get; set; }
        public DateTime? OptCreated { get; set; }
        public int? OptStatus { get; set; }
    }
}

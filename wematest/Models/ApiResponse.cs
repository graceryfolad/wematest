using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wematest.Models
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }

        public Object Error { get; set; }
    }

    public class BankResult
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }


    

    public class BankAPIResponse
    {
        public List<BankResult> result { get; set; }
        public object errorMessage { get; set; }
        public object errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }

}

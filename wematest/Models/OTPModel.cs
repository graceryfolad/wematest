using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wematest.Data;

namespace wematest.Models
{
    public class OTPModel
    {
        public async Task<bool> SendOTP(string phonenumber, int custid)
        {
            Random rd = new Random();
            int rand_num = rd.Next(100000, 99999);

            // store otp in db

            using(var context = new wematestContext())
            {
                Otp otp = new Otp() { 
                    OptCreated = DateTime.Now,
                    OtpCustomer = custid,
                    OptStatus = MyConstants.Customer_Register_Pending,
                    OptValue = rand_num.ToString()
                };

                context.Otps.Add(otp);

                if(await context.SaveChangesAsync()  > 0)
                {
                    // send the otp to customer



                    return true;
                }
            }

            
            return false;
        }

        public async Task<bool> VerifyOTP(VerifyOTP verifyOTP)
        {
            using (var context = new wematestContext())
            {
                Otp findotp = context.Otps.Where(o => o.OptValue == verifyOTP.otpvalue && o.OtpCustomer == verifyOTP.customerid).SingleOrDefault();
                DateTime expiretime = (DateTime)findotp.OptCreated.Value.AddMinutes(5);
                if(DateTime.Now  < expiretime)
                {
                    //update otp table 
                    findotp.OptStatus = MyConstants.Otp_Verified;
                    context.Otps.Update(findotp);
                    return true;
                }
            }

            return false;
        }
    }

    public class VerifyOTP
    {
        public string otpvalue { get; set; }
        public int customerid { get; set; }
    }

}

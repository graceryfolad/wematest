using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wematest.Models;
using wematest.Services;

namespace wematest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApiResponse apiResponse = new ApiResponse();
        BankService service;
        public CustomerController(BankService bankService)
        {
            apiResponse.Status = MyConstants.HTTP_RESPONSE_STATUS_FAILED;
            apiResponse.Message = "Failed";
            service = bankService;
        }

        [HttpPost("Onboard")]
        public async Task<ActionResult> NewCustomer(OnboardCustomer onboard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerModel customerModel = new CustomerModel();
                   var result = await customerModel.Onboard(onboard);
                    // send otp 
                    OTPModel otp = new OTPModel();
                    await otp.SendOTP(result.PhoneNumber, result.CustId);

                    apiResponse.Result = result;
                    apiResponse.Status = MyConstants.HTTP_RESPONSE_STATUS_SUCCESS;
                }
                else
                {
                    apiResponse.Message = "Validaation Error";
                    apiResponse.Error = ValidationModel.getErrors(ModelState);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok(apiResponse);
        }

        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP(VerifyOTP onboard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OTPModel model = new OTPModel();
                   if(await model.VerifyOTP(onboard))
                    {
                        // upldate customer to completed

                        CustomerModel customer = new CustomerModel();
                       if(await customer.CompleteOnboarding(onboard.customerid))
                        {
                            apiResponse.Status = MyConstants.HTTP_RESPONSE_STATUS_SUCCESS;
                            apiResponse.Message = "OTP Verified";
                        }
                        //customer.
                    }
                   
                    
                }
                else
                {
                    apiResponse.Message = "Validaation Error";
                    apiResponse.Error = ValidationModel.getErrors(ModelState);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                apiResponse.Error = ex.StackTrace;
              //  throw;
            }

            return Ok(apiResponse);
        }

        [HttpGet("AllCustomers")]
        public async Task<ActionResult> AllCustomers()
        {
            CustomerModel customerModel = new CustomerModel();
            var result = await customerModel.AllCustomers();

            apiResponse.Result = result;
            apiResponse.Status = MyConstants.HTTP_RESPONSE_STATUS_SUCCESS;
            apiResponse.Message = "Success";
            return Ok(apiResponse);

        }

        [HttpGet("Customer/{customerid}")]
        public async Task<ActionResult> Customer(int customerid)
        {
            CustomerModel customerModel = new CustomerModel();
            var result = customerModel.aCustomer(customerid);

            apiResponse.Result = result;
            apiResponse.Status = MyConstants.HTTP_RESPONSE_STATUS_SUCCESS;
            apiResponse.Message = "Success";
            return Ok(apiResponse);

        }

        [HttpGet("AllBanks")]
        public async Task<ActionResult> AllBanks()
        {
          var response =   service.GetBanks();
            apiResponse.Result = response;
            apiResponse.Status = MyConstants.HTTP_RESPONSE_STATUS_SUCCESS;
            apiResponse.Message = "Success";
            return Ok(apiResponse);

        }


    }
}

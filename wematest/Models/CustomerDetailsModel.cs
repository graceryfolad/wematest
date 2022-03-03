using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using wematest.Data;

namespace wematest.Models
{
    public class CustomerDetailsModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StateOfResidence { get; set; }
        public string LGA { get; set; }
        public int CustId { get; set; }
    }

    public class OnboardCustomer
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int StateOfResidence { get; set; }
        [Required]
        public int LGA { get; set; }
    }

    public class CustomerModel
    {
        public async Task<CustomerDetailsModel> Onboard(OnboardCustomer onboard)
        {
            using(var context = new wematestContext())
            {
                Customer customer = new Customer()
                {
                    CustEmail = onboard.Email,
                    CustLga = onboard.LGA,
                    CustPhoneNumber = onboard.PhoneNumber,
                    CustSor = onboard.StateOfResidence,
                    CustRegStatus = MyConstants.Customer_Register_Pending
                };
                context.Customers.Add(customer);
                try
                {
                    if (await context.SaveChangesAsync() > 0)
                    {
                        // send otp

                        OTPModel model = new OTPModel();

                        await model.SendOTP(customer.CustPhoneNumber, customer.CustId);
                        return aCustomer(customer.CustId);
                    }
                }
                catch (Exception ex)
                {
                    
                    throw;
                }

                
            }
            return null;
        }

        public async Task<List<CustomerDetailsModel>> AllCustomers()
        {
            using (var context = new wematestContext())
            {
                var customers =  context.Customers.Include("CustSorNavigation").ToList();

                return this.FornatCustomers(customers);
            }
            return null;
        }


        public CustomerDetailsModel aCustomer(int customerid)
        {
            using (var context = new wematestContext())
            {
                var customer = context.Customers.Where(cust=>cust.CustId == customerid).Include("CustSorNavigation").SingleOrDefault();

                return this.FornatCustomer(customer);
            }
            return null;
        }



        public async Task<bool> CompleteOnboarding(int custid)
        {
            using (var context = new wematestContext())
            {
                Customer customer = context.Customers.SingleOrDefault(c => c.CustId == custid);
                customer.CustRegStatus = MyConstants.Customer_Register_Completed;
                context.Customers.Update(customer);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public CustomerDetailsModel FornatCustomer(Customer customer)
        {
            if(customer != null)
            {
                return new CustomerDetailsModel()
                {
                    CustId = customer.CustId,
                    Email = customer.CustEmail,
                    PhoneNumber = customer.CustPhoneNumber,
                    LGA = "",                                                                                       
                    StateOfResidence = customer.CustSorNavigation.StaName
                };
            }

            return null;
        }

        public List<CustomerDetailsModel> FornatCustomers(List<Customer> customers)
        {
            if (customers != null)
            {

                List<CustomerDetailsModel> list = new List<CustomerDetailsModel>();
                foreach (Customer cust in customers)
                {
                    list.Add(new CustomerDetailsModel()
                    {
                        CustId = cust.CustId,
                        Email = cust.CustEmail,
                        PhoneNumber = cust.CustPhoneNumber,
                        LGA = "",
                        StateOfResidence = cust.CustSorNavigation.StaName
                    });
                }
                return  list;
            }

            return null;
        }
    }
}

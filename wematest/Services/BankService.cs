using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using wematest.Models;

namespace wematest.Services
{
    public class BankService
    {
        protected readonly IConfiguration configuration;
        public string APIURL = String.Empty;
        public object returnobj;

        public BankService(IConfiguration iconfig)
        {

            configuration = iconfig;
            APIURL = configuration["APIURL"];


        }

        public BankAPIResponse GetBanks()
        {
            string endpoint = "Shared/GetAllBanks";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "f4dcf54002d34742accd29120ddef3bb");
                var response = client.GetAsync(endpoint);
                response.Wait();

                if (response.IsCompleted)
                {

                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<BankAPIResponse>();
                        readTask.Wait();



                        var rawResponse = readTask.Result;


                        return rawResponse;

                        


                    }
                    else
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string xx = readTask.Result;
                        var err = JsonConvert.DeserializeObject(xx);
                    }

                }

            }

            return null;
        }
    }
}

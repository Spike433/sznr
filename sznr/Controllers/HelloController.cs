using Itenso.TimePeriod;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using sznr;

namespace HelloWebAPI.Controller
{
    public class HelloController : ApiController
    {
        //https://localhost:44325/api/hello?param1=20190614T041236&param2=car&api_key=M6N015C8W6ALJ

        static List<Customer> customers = new List<Customer>();

        static int delay = 30;

               
        public HttpResponseMessage Post([FromUri] Customer customer)
        {
            if (customer.api_key == "M6N015C8W6ALJ")
            {

                String formatFromAndroid = "yyyyMMddTHHmmss";
               
                DateTime dateTime = DateTime.ParseExact(customer.param1, formatFromAndroid, null);

                ITimePeriod timePeriod = new TimeBlock(dateTime);
                                
                TimePeriodCollection timePeriods = new TimePeriodCollection();


                
                foreach (Customer customer1 in customers)
                {
                    string line = customer1.param1;

                    DateTime parsedLine = DateTime.ParseExact(line, formatFromAndroid, null);

                    timePeriods.Add(new TimeBlock(parsedLine, Duration.Minutes(delay)));

                    if (timePeriods.Last().OverlapsWith(timePeriod))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created,customers);

                    }

                    
                }                
                customers.Add(customer);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            
            else
            {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }

        }

        
    }
}
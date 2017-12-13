using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Client;


namespace Console17Odata
{
    class Program
    {
        static void Main(string[] args)
        {
            NAV.NAV nav=new NAV.NAV(new Uri(@"http://ahasan-pc:7048/DynamicsNAV100/ODataV4/Company('CRONUS%20International%20Ltd.')"));
            nav.Credentials=new NetworkCredential(@"ahasan-pc\ahasan","shobuz");
            var dd = nav.CustomerService.Take(100);
            //DataServiceQuery<NAV.CustomerService> q = nav.CreateQuery<NAV.CustomerService>("CustomerService");

            //List<NAV.CustomerService> ccList = q.Execute().ToList();
        }
    }
}

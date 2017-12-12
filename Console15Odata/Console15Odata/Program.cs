using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Console15Odata.OdataService;

namespace Console15Odata
{
    class Program
    {
        static void Main(string[] args)
        {
            NAV nav=new NAV(new Uri(@"http://ahasan-pc:7048/DynamicsNAV100/OData/Company('CRONUS%20International%20Ltd.')"));
            nav.Credentials=new NetworkCredential(@"ahasan-pc\ahasan","shobuz");
            var data = (nav.CustomerService.Take(100)).ToList();
        }
    }
}

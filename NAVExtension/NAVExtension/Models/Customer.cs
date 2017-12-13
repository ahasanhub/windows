using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVExtension.Models
{
    public class Customer
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string LocationCode { get; set; }
        public string Contact { get; set; }
        public decimal Balance { get; set; }
        public decimal BalanceDue { get; set; }
        public decimal Sales { get; set; }        
    }
}

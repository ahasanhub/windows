using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVExtension.Models
{
    public class GeneralJournal
    {
        public DateTime PostingDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public string GeneralPostingType { get; set; }
        public decimal Amount { get; set; }
        public string BalAccountNo { get; set; }
        public decimal Balance { get; set; }

    }
}

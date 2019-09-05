using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Transaction> Transactions { get; set; }
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

    }
}
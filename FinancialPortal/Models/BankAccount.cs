using FinancialPortal.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public int HouseholdId { get; set; }
        public int AccountTypeId { get; set; }
        public Double StartingBalance {get;set;}
        public Double CurrentBalance { get; set; }
        public Double LowBalance { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual Household Household { get; set; }
        public virtual AccountType AccountType { get; set; }
       
        public virtual ICollection<Transaction> Transactions { get; set; }
       

        public BankAccount()
        {
           
            Transactions = new HashSet<Transaction>();
        }



    }
}
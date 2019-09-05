using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Greeting { get; set; }
        public DateTime Established { get; set; }


        
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }

        public Household()
        {
            BankAccounts = new HashSet<BankAccount>();
            Invitations = new HashSet<Invitation>();
            Budgets = new HashSet<Budget>();
            Notifications = new HashSet<Notification>();
            Members = new HashSet<ApplicationUser>();
        }
    }
}
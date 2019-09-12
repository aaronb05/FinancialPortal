using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class LobbyVM
    {
        public Household Household = new Household();
        public BankAccount BankAccount = new BankAccount();
        public Budget Budget = new Budget();
        public BudgetItem BudgetItem = new BudgetItem();
        public AccountType AccountType = new AccountType();

    }
}
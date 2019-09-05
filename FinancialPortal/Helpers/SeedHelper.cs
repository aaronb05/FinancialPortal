using FinancialPortal.Enumerations;
using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FinancialPortal.Helpers
{

    public class SeedHelper
    {
       
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private ApplicationDbContext db { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }
        private readonly string DefaultAvatar = Setting.DefaultAvatar.ToString();
        private readonly string DefaultPassword = Setting.DefaultAvatar.ToString();
    
        private readonly string HeadOfHouse = Setting.HeadOfHouse.ToString();
        private readonly string SeededHouseId = Setting.SeededHouseId.ToString();


        public void SeedRoles()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(dbContext));

            if (!dbContext.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!dbContext.Roles.Any(r => r.Name == "HeadOfHouse"))
            {
                roleManager.Create(new IdentityRole { Name = "HeadOfHouse" });
            }
            if (!dbContext.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }
            if (!dbContext.Roles.Any(r => r.Name == "Lobbyist"))
            {
                roleManager.Create(new IdentityRole { Name = "Lobbyist" });
            }

            dbContext.SaveChanges();
        }

      

        #region Users

        public void SeedUsers()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(dbContext));
            
            if (!dbContext.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@mailinator.com",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Aaron",
                    LastName = "Boyles",
                    DisplayName = "AaronBoyles"

                }, "Abc123!");

            }
            if (!dbContext.Users.Any(u => u.Email == "DemoHoH@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoHoH@mailinator.com",
                    Email = "DemoHoH@mailinator.com",
                    FirstName = "Tori",
                    LastName = "Faircloth",
                    DisplayName = "ToriFaircloth"

                }, "Abc123!");
            }
            if (!dbContext.Users.Any(u => u.Email == "DemoMember@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoMember@mailinator.com",
                    Email = "DemoMember@mailinator.com",
                    FirstName = "Bentley",
                    LastName = "Boyles",
                    DisplayName = "BentleyBoyles"

                }, "Abc123!");
            }
            if (!dbContext.Users.Any(u => u.Email == "DemoLobbyist@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoLobbyist@mailinator.com",
                    Email = "DemoLobbyist@mailinator.com",
                    FirstName = "Josh",
                    LastName = "Broach",
                    DisplayName = "JoshBroach"

                }, "Abc123!");
            }
            dbContext.SaveChanges();
        }

        #endregion

        public void AssignRoles()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(dbContext));

            var adminUser = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(adminUser, "Admin");

            var hohUser = userManager.FindByEmail("DemoHoH@mailinator.com").Id;
            userManager.AddToRole(hohUser, "HeadOfHouse");

            var member = userManager.FindByEmail("DemoMember@mailinator.com").Id;
            userManager.AddToRole(member, "Member");

            var lobbyist= userManager.FindByEmail("DemoLobbyist@mailinator.com").Id;
            userManager.AddToRole(lobbyist, "Lobbyist");

            dbContext.SaveChanges();
        }

        public void SeedHouse()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(
              new UserStore<ApplicationUser>(dbContext));

           
                dbContext.Households.AddOrUpdate(h => h.Established,
                    new Household
                    {
                        OwnerId = userManager.FindByEmail("DemoHoh@mailinator.com").Id,
                        Name = "Seeded House",
                        Greeting = "Welcome to your new house!",
                        Established = DateTime.Now
                    });


            dbContext.SaveChanges();
        }

        public void SeedAccountTypes()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            dbContext.AccountTypes.AddOrUpdate(
                t => t.Type,
                new AccountType { Type = "Checking", Description = "This account is a checking account"},
                new AccountType { Type = "Savings", Description = "This account in a savings account"}
                );

            dbContext.SaveChanges();
        }

        public void SeedTransactionTypes()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            dbContext.TransactionTypes.AddOrUpdate(
                t => t.Name,
                new TransactionType { Name ="Withdrawl", Description = "This transaction is a withdrawl" },
                new TransactionType { Name = "Deposit", Description = "This transaction is a Deposit"}
                );

            dbContext.SaveChanges();
        }

        public void SeedBudget()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var houseId = dbContext.Households.Where(h => h.Name == "Seeded House").FirstOrDefault().Id;

            dbContext.Budgets.AddOrUpdate(
                b =>b.Name,
                new Budget { HouseHoldId = houseId, Name = "Utilities", Target = 800, Actual = 750, Created = DateTime.Now, },
                new Budget { HouseHoldId = houseId, Name = "Entertainment", Target = 150, Actual = 30, Created = DateTime.Now, },
                new Budget { HouseHoldId = houseId, Name = "Groceries", Target = 200, Actual = 225, Created = DateTime.Now, },
                new Budget { HouseHoldId = houseId, Name = "House", Target = 1200, Actual = 1000, Created = DateTime.Now, },
                new Budget { HouseHoldId = houseId, Name = "Miscellaneous", Target = 800, Actual = 750, Created = DateTime.Now, },
                new Budget { HouseHoldId = houseId, Name = "VehicleMaintenance", Target = 200, Actual = 110, Created = DateTime.Now, });

            dbContext.SaveChanges();

        }

        public void SeedBudgetItems()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var utilitiesBudgetId = dbContext.Budgets.AsNoTracking().FirstOrDefault(b => b.Name == "Utilities").Id;
            var groceriesBudegtId = dbContext.Budgets.AsNoTracking().FirstOrDefault(b => b.Name == "Groceries").Id;
            var enterainmentBudgetId = dbContext.Budgets.AsNoTracking().FirstOrDefault(b => b.Name == "Entertainment").Id;
            var houseBudgetId = dbContext.Budgets.AsNoTracking().FirstOrDefault(b => b.Name == "House").Id;
            var vehicleBudgetId = dbContext.Budgets.AsNoTracking().FirstOrDefault(b => b.Name == "VehicleMaintenance").Id;


            dbContext.BudgetItems.AddOrUpdate(
                new BudgetItem { Name = "Power", BudgetId = utilitiesBudgetId, Target = 120, Actual = 110, Created = DateTime.Now, },
                new BudgetItem { Name = "Water", BudgetId = utilitiesBudgetId, Target = 35, Actual = 32, Created = DateTime.Now, },
                new BudgetItem { Name = "TV/Internet", BudgetId = utilitiesBudgetId, Target = 150, Actual = 150, Created = DateTime.Now, },
                new BudgetItem { Name = "Food", BudgetId = groceriesBudegtId, Target = 50, Actual = 45, Created = DateTime.Now, },
                new BudgetItem { Name = "HousePayment", BudgetId = houseBudgetId, Target = 1000, Actual = 1000, Created = DateTime.Now, },
                new BudgetItem { Name = "Movies", BudgetId = enterainmentBudgetId, Target = 50, Actual = 30, Created = DateTime.Now, },
                new BudgetItem { Name = "Gas", BudgetId = vehicleBudgetId, Target = 100, Actual = 110, Created = DateTime.Now, });

            dbContext.SaveChanges();
        }

        public void SeedBankAccounts()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(
              new UserStore<ApplicationUser>(dbContext));

            var houseId = dbContext.Households.Where(h => h.Name == "Seeded House").FirstOrDefault().Id;
            var checking = dbContext.AccountTypes.AsNoTracking().FirstOrDefault(a => a.Type == "Checking").Id;
            var savings = dbContext.AccountTypes.AsNoTracking().FirstOrDefault(a => a.Type == "Savings").Id;


            dbContext.BankAccounts.AddOrUpdate(
                b => b.Name,
                new BankAccount
                {
                   
                    HouseholdId = houseId,
                    AccountTypeId = checking,
                    OwnerId = userManager.FindByEmail("DemoHoH@mailinator.com").Id,
                    StartingBalance = 5000,
                    CurrentBalance = 4000,
                    LowBalance = 1000,
                    Name = "Bank of America Checking",
                    Description = "Seeded Checking Account",
                    Created = DateTime.Now,
                    Address1 = "3105 Meadow Lark Ln",
                    City = "Sophia",
                    State = State.NC,
                    ZipCode = "27350",
                    Phone ="336.688.4615"

                },
                new BankAccount
                {
                   
                    HouseholdId = houseId,
                    AccountTypeId = savings,
                    OwnerId = userManager.FindByEmail("DemoHoH@mailinator.com").Id,
                    StartingBalance = 20000,
                    CurrentBalance = 15000,
                    LowBalance = 10000,
                    Name = "Bank of America Savings",
                    Description = "Seeded Savings Account",
                    Created = DateTime.Now,
                    Address1 = "3105 Meadow Lark Ln",
                    City = "Sophia",
                    State = State.NC,
                    ZipCode = "27350",
                    Phone = "336.688.4615"

                });

            dbContext.SaveChanges();
        }

        public void SeedTransactions()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(
              new UserStore<ApplicationUser>(dbContext));

            var savingsAccount = dbContext.BankAccounts.AsNoTracking().FirstOrDefault(b => b.Name == "Bank of America Savings").Id;
            var checkingAccount = dbContext.BankAccounts.AsNoTracking().FirstOrDefault(b => b.Name == "Bank of America Checking").Id;
            var withdrawl = dbContext.TransactionTypes.AsNoTracking().FirstOrDefault(t => t.Name == "Withdrawl").Id;
            var deposit = dbContext.TransactionTypes.AsNoTracking().FirstOrDefault(t => t.Name == "Deposit").Id;
            var housePaymentBudgetId = dbContext.BudgetItems.AsNoTracking().FirstOrDefault(t => t.Name == "HousePayment").Id;
            var powerPaymentBudgetId = dbContext.BudgetItems.AsNoTracking().FirstOrDefault(t => t.Name == "Power").Id;

            dbContext.Transactions.AddOrUpdate(
                b => b.Memo,
                new Transaction
                {
                    BankAccountId = checkingAccount,
                    TransactionTypeId = withdrawl,
                    BudgetItemId = housePaymentBudgetId,
                    OwnerId = userManager.FindByEmail("DemoHoH@mailinator.com").Id,
                    Description = "House Payment",
                    Amount = 1000,
                    Memo = "Payment to mortgage company or my house",
                    Created = DateTime.Now

                },
                 new Transaction
                 {
                     BankAccountId = checkingAccount,
                     TransactionTypeId = withdrawl,
                     BudgetItemId = powerPaymentBudgetId,
                     OwnerId = userManager.FindByEmail("DemoHoH@mailinator.com").Id,
                     Description = "Power",
                     Amount = 110,
                     Memo = "Payment for the power bill",
                     Created = DateTime.Now

                 },
                 new Transaction
                 {
                     BankAccountId = savingsAccount,
                     TransactionTypeId = deposit,
                     OwnerId = userManager.FindByEmail("DemoHoH@mailinator.com").Id,
                     Description = "Deposit",
                     Amount = 1500,
                     Memo = "Paycheck",
                     Created = DateTime.Now

                 });

            dbContext.SaveChanges();
        }

    }
}
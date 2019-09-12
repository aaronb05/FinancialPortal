namespace FinancialPortal.Migrations
{
    using FinancialPortal.Helpers;
    using FinancialPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<FinancialPortal.Models.ApplicationDbContext>
    {
        public SeedHelper seedHelper = new SeedHelper();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinancialPortal.Models.ApplicationDbContext context)
        {
            //This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //if (!Debugger.IsAttached) Debugger.Launch();

            seedHelper.SeedRoles();
            seedHelper.SeedUsers();
            seedHelper.AssignRoles();
            seedHelper.SeedHouse();
            seedHelper.SeedTransactionTypes();
            seedHelper.SeedAccountTypes();
            seedHelper.SeedBudget();
            seedHelper.SeedBudgetItems();
            seedHelper.SeedBankAccounts();
            seedHelper.SeedTransactions();


        }
    }
}

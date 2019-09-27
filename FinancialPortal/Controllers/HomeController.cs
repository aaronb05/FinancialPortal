using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.ViewModels;
using FinancialPortal.Helpers;


namespace FinancialPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LandingPage()
        {

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult Lobby()
        {
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Wizard(Household household, BankAccount account, Budget budget, BudgetItem budgetItem)
        {
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                household.OwnerId = userId;
                household.Established = DateTime.Now;
                db.Households.Add(household);

                var user = db.Users.Find(userId);
                user.HouseholdId = household.Id;
                roleHelper.RemoveUserFromRole(userId, "Lobbyist");
                roleHelper.AddUserToRole(userId, "HeadOfHouse");
                db.SaveChanges();

                account.HouseholdId = household.Id;
                account.OwnerId = userId;
                account.Created = DateTime.Now;
                account.AccountTypeId = account.AccountType.Id;
                db.BankAccounts.Add(account);

                budget.HouseholdId = household.Id;
                budget.Created = DateTime.Now;
                db.Budgets.Add(budget);
                db.SaveChanges();

                budgetItem.BudgetId = budget.Id;
                budgetItem.Created = DateTime.Now;
                db.BudgetItems.Add(budgetItem);
                db.SaveChanges();

                return RedirectToAction("Dashboard", "Households");
            }

            return View("Lobby");
        }
       
    }
}
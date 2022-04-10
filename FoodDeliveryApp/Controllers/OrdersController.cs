using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryApp.Models;
using PagedList;

namespace FoodDeliveryApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string currentOrder, string currentFillter, string SearchString, int? page)
        {
            ViewBag.CurentOrder = currentOrder;
            ViewBag.DateSortParameter = String.IsNullOrEmpty(currentOrder) ? "Order_desc" : "";
            IQueryable<Order> orders = db.orders;
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFillter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                orders = orders.Where(s => s.Customer.Name.Contains(SearchString)
                                          || s.Id.ToString().Contains(SearchString));

            }
            switch (currentOrder)
            {
                case "Order_desc":
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(s => s.OrderDate);
                    break;
            }
            ViewBag.CurrentFillter = SearchString;
            int PageSize = 10;
            int PageNumber = (page ?? 1);
            return View(orders.ToPagedList(PageNumber, PageSize));
        }

        // GET: Orders1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerName,TotalPrice")] CreateOrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer() { Name = orderVM.CustomerName };
                db.customers.Add(customer);
                Order order = new Order() { OrderDate = DateTime.Now, TotalPrice = orderVM.TotalPrice };
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderVM);
        }

        // GET: Orders1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

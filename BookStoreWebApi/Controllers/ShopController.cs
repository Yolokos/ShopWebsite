using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStoreWebApi.Models;
using BookStoreWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreWebApi.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<Customer> userManager;

        public ShopController(ApplicationDbContext db, UserManager<Customer> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult Store() => View(db.Books.ToList());

        
        

        public async Task<IActionResult> BuyBook(string ISBN)
        {
            if (!string.IsNullOrEmpty(ISBN))
            {
                var book = await db.Books.FirstOrDefaultAsync(p => p.ISBN == ISBN);
                if (book != null)
                {
                    return View(book);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Такой книги не существует");
                }
            }
            return NotFound();
        }
        

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyBook(string ISBN, TypeOfDeliverViewModel typeOfDeliver, int countOfBooks = 1)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUser();
                var book = await db.Books.FirstOrDefaultAsync(p => p.ISBN == ISBN);
                var courier = await db.Couriers.FirstAsync();
                DateTime today = DateTime.Now;
                var order = new Order
                {
                    CustomersId = new List<Customer> { user },
                    FormOfPayment = typeOfDeliver.Payment.ToString(),
                    DateOrder = today,
                    DateDeliver = today.AddDays(3),
                    DateOfExecute = today.AddDays(1),
                    TypeOfDeliver = typeOfDeliver.Type.ToString(),
                    DeliverPrice = typeOfDeliver.Price,
                    CouriersId = new List<Courier> { courier },
                    DeliverAdress = typeOfDeliver.Adress
                };

                await db.Orders.AddAsync(order);

                await db.Shoppings.AddAsync(new ShoppingCart
                {
                    OrdersId = new List<Order> { order },
                    BooksId = new List<Book> { book },
                    CountCopy = countOfBooks
                });

                await db.SaveChangesAsync();

                return View(order);
            }
            return View(typeOfDeliver);
        }

        private async Task<Customer> GetCurrentUser()
        {
            return await userManager.GetUserAsync(HttpContext.User);
        }

    }
}
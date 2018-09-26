using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStoreWebApi.Models;
using BookStoreWebApi.ViewModel;

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

        public async Task<IActionResult> InfoBook(string ISBN)
        {
            if(!string.IsNullOrEmpty(ISBN))
            {
                var book = await db.Books.FirstOrDefaultAsync(p => p.ISBN == ISBN);
                if(book != null)
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

        
        public async Task<IActionResult> BuyBook(string ISBN, TypeOfDeliverViewModel typeOfDeliver)
        {
            var user = await GetCurrentUser();
            DateTime today = DateTime.Now;

            await db.Orders.AddAsync(new Order
            {
                CustomersId = new List<Customer> { user },
                FormOfPayment = "Yandex.Money",
                DateOrder = today,
                DateDeliver = today.AddDays(3),
                DateOfExecute = today.AddDays(1),
                TypeOfDeliver = typeOfDeliver.Type,
                DeliverPrice = typeOfDeliver.Price
            });

            throw new Exception();
        }

        private async Task<Customer> GetCurrentUser()
        {
            return await userManager.GetUserAsync(HttpContext.User);
        }

    }
}
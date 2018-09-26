using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreWebApi.Models;

namespace BookStoreWebApi.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext db;

        public ShopController(ApplicationDbContext db)
        {
            this.db = db;
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



        public async void AddToCart(string ISBN)
        {
            var book = await db.Books.FirstOrDefaultAsync(p => p.ISBN == ISBN);

        }
    }
}
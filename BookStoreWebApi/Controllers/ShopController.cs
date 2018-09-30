﻿using System;
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
           // InitializeBooks();
        }

        public async Task<IActionResult> Store() {

            return View(await db.Books.ToListAsync());
        }

        
        

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

        private async Task InitializeBooks()
        {
            var books = new List<Book> {new Book{
                ISBN = "978-5-496-01649-0",
                Authors = "Сергей Тепляков",
                NameOfBook = "Паттерны проектирования на платформе .NET",
                SectionOfLiterature = "Компьютерная литература",
                Publishing = "Питер",
                Description = "Паттерны проектирования остаются важным инструментом в арсенале разработчика, поскольку они опираются на фундаментальные принципы проектирования. " +
                "Тем не менее, появление новых конструкций в современных языках программирования делает одни паттерны более важными, а значимость других сводит к минимуму. " +
                "Цель данной книги — показать, как изменились паттерны проектирования за это время, как на них повлияло современное увлечение функциональным программированием, и объяснить, каким образом они используются в современных .NET-приложениях. " +
                "В издании вы найдете подробное описание классических паттернов проектирования с особенностями их реализации на платформе .NET, а также примеры их использования в .NET Framework. " +
                "Вы также изучите принципы проектирования, известные под аббревиатурой SOLID, и научитесь применять их при разработке собственных приложений. " +
                "Книга предназначена для профессиональных программистов, которые хотят изучить особенности классических принципов и паттернов программирования с примерами на языке C# и понять их роль в разработке современных приложений на платформе .NET",
                BookPrice = 999.99,
                YearOfPublishing = new DateTime(2009, 8, 18)
            },
            new Book
            {
                ISBN = "978-5-6040043-7-1",
                Authors = "Джозеф Албахари, Бен Албахари",
                NameOfBook = "C# 7.0. Справочник. Полное описание языка",
                SectionOfLiterature = "Компьютерная литература",
                Publishing = "Диалектика, Вильямс", 
                Description = "Когда у вас возникают вопросы по языку C# 7.0 или среде CLR и основным сборкам .NET Framework, это ставшее бестселлером руководство предложит все необходимые ответы. " +
                "С момента представления в 2000 году C# стал языком с замечательной гибкостью и широким размахом, но такое непрекращающееся развитие означает, что по-прежнему есть многие вещи, которые предстоит изучить.",
                BookPrice = 988.88,
                YearOfPublishing = new DateTime(2011, 1, 29)
            }
            };

            await db.Books.AddAsync(books[0]);
            await db.SaveChangesAsync();           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebApi.Models;

namespace BookStoreWebApi.ViewModel
{
    public class CurrentOrdersViewModel
    {
        public List<Book> Books { get; set; }
        public List<Order> Orders { get; set; }
    }
}

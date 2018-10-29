using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWebApi.Models
{

    public class Order
    {
        public int OrderId { get; set; }
        public string FormOfPayment { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOrder { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateDeliver { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfExecute { get; set; }
        public string TypeOfDeliver { get; set; }
        public double DeliverPrice { get; set; }
        public string DeliverAdress { get; set; }

        public List<OrderBook> OrderBooks { get; set; }

        public Order()
        {
            OrderBooks = new List<OrderBook>();
        }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

    }

    public class OrderBook
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }

        public int CountCopy { get; set; }
    }

    public class Book
    {
        [Key]
        public string ISBN { get; set; }
        public string SectionOfLiterature { get; set; }
        public string NameOfBook { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string Publishing { get; set; }
        [Column(TypeName = "Date")]
        public DateTime YearOfPublishing { get; set; }
        public double BookPrice { get; set; }

        public List<OrderBook> OrderBooks { get; set; }
        public Book()
        {
            OrderBooks = new List<OrderBook>();
        }
    }

}

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
        public List<Customer> Customers { get; set; }

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
        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }

    public class ShoppingCart
    {
  
        public int Id { get; set; }
        public List<Order> Orders { get; set; }
        public List<Book> Books { get; set; }
        public int CountCopy { get; set; }
    }

}

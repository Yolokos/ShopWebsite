using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWebApi.Models
{

    public class Courier
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfBorn { get; set; }
        [Column(TypeName = "Date")]
        public DateTime EmploymentDate { get; set; }
        public DateTime WorkingShift { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public List<Customer> CustomersId { get; set; }
        public string FormOfPayment { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOrder { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateDeliver { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfExecute { get; set; }
        public string TypeOfDeliver { get; set; }
        public double DeliverPrice { get; set; }
        public List<Courier> CouriersId { get; set; }
        public string DeliverAdress { get; set; }


        public Order()
        {
            CustomersId = new List<Customer>();
            CouriersId = new List<Courier>();
        }
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
    }

    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public List<Order> OrdersId { get; set; }
        public List<Book> BooksId { get; set; }
        public int CountCopy { get; set; }

        public ShoppingCart()
        {
            OrdersId = new List<Order>();
            BooksId = new List<Book>();
        }
    }

}

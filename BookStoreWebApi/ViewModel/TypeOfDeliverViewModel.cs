using System.ComponentModel.DataAnnotations;
using BookStoreWebApi.Enums;
using BookStoreWebApi.Models;

namespace BookStoreWebApi.ViewModel
{
    public class TypeOfDeliverViewModel
    {
        [Required]
        public EnumTypeOfDeliver Type { get; set; }
        [Required]
        public EnumOfPayment TypePayment { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public int CountOfBooks { get; set; }

        public Book Book { get; set; }
    }
}

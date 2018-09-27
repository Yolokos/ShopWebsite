using System.ComponentModel.DataAnnotations;
using BookStoreWebApi.Enums;

namespace BookStoreWebApi.ViewModel
{
    public class TypeOfDeliverViewModel
    {
        [Required]
        public EnumTypeOfDeliver Type { get; set; }
        [Required]
        public EmumOfPayment Payment { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}

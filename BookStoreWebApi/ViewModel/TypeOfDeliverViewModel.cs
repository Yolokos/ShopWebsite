using System.ComponentModel.DataAnnotations;

namespace BookStoreWebApi.ViewModel
{
    public class TypeOfDeliverViewModel
    {
       [Required]
       public string Type { get; set; }
       [Required]
       public double Price { get; set; }
    }
}

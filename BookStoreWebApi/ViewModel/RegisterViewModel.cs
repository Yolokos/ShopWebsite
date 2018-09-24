using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebApi.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Введите Вашу почту")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Compare("Email", ErrorMessage = "Почты не совпадает")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Введите номер Вашего мобильного телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}

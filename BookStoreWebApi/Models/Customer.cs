using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookStoreWebApi.Models
{
    public class Customer : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}

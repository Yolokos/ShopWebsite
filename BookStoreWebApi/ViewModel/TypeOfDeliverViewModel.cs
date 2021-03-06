﻿using System.ComponentModel.DataAnnotations;
using BookStoreWebApi.Enums;
using BookStoreWebApi.Models;

namespace BookStoreWebApi.ViewModel
{
    //When creating order 
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
   
        public string GetBook { get; set; }
    }
}

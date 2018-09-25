using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BookStoreWebApi.Models;

namespace BookStoreWebApi.Validate
{
    public class EmailValidatior : IUserValidator<Customer>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<Customer> manager, Customer user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (!IsValid(user.Email))
            {
                errors.Add(new IdentityError
                {
                    Description = "Некорректная почта, выберите другую."
                });
            }
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Ник пользователя не должен содержать слово 'admin'"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

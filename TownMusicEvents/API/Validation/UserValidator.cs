using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace API.Validation
{
    public class UserValidator : IValidator<User>
    {
        public List<string> Check(User entity)
        {
            List<string> errors = new List<string>();
            if (entity.Email == null || entity.Email == "")
            {
                errors.Add("• Email-ul nu poate fi vid" + Environment.NewLine);
            }
            try
            {
                MailAddress m = new MailAddress(entity.Email);
            }
            catch
            {
                errors.Add("• Formatul email-ului este incorect");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            if (entity.Password==null || entity.Password.Length <= 5 || !hasNumber.IsMatch(entity.Password) || 
                (!hasLowerChar.IsMatch(entity.Password) && !hasUpperChar.IsMatch(entity.Password)))
            {
                errors.Add("• Parola trebuie aiba minim 6 caractere si sa contina cel putin o litera si o cifra" + Environment.NewLine);
            }
            if(entity.Name ==null || entity.Name.Length < 2)
            {
                errors.Add("• Numele este invalid" + Environment.NewLine);
            }
            return errors;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace API.Validation
{
    public class PasswordValidator : IValidator<string>
    {
        public List<string> Check(string password)
        {
            List<string> errors = new List<string>();
            var hasNumber = new Regex(@"[0-9]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            if (password.Length <= 5 || !hasNumber.IsMatch(password) ||
                (!hasLowerChar.IsMatch(password) && !hasNumber.IsMatch(password)))
            {
                errors.Add("Parola trebuie aiba minim 6 caractere si sa contina cel putin o litera si o cifra" + Environment.NewLine);
            }
            return errors;
        }
    }
}
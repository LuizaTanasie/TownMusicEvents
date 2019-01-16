using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Validation
{
    public class RatingValidator : IValidator<int>
    {
        public List<string> Check(int score)
        {
            List<string> errors = new List<string>();
            if (score<1 || score > 5)
            {
                errors.Add("Rating invalid");
            }
            return errors;
        }
    }
}
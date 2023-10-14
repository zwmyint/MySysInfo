using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySysInfo.ConsoleApp
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage("Name shouldn't be empty.");

            RuleFor(person => person.Age)
                .InclusiveBetween(18, 99)
                .WithMessage("The age must between 18 & 99 years.");
        }

        //
    }
}

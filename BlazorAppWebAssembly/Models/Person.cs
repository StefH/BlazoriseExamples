using System;
using System.Threading.Tasks;
using FluentValidation;

namespace BlazorAppWebAssembly.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; } = new Address();

        public DateTime? Date { get; set; }
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            //RuleFor(p => p.Name).NotEmpty().WithMessage("You must enter a name");
            //RuleFor(p => p.Name).MaximumLength(50).WithMessage("Name cannot be longer than 50 characters");
            RuleFor(p => p.Age).GreaterThan(0).WithMessage("Age must be greater than 0");
            RuleFor(p => p.Age).LessThanOrEqualTo(99).WithMessage("Age cannot be greater than 99");
            //RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("You must enter a email address");
            //RuleFor(p => p.EmailAddress).EmailAddress().WithMessage("You must provide a valid email address");

            //RuleFor(x => x.Name).MustAsync(async (name, cancellationToken) => await IsUniqueAsync(name))
            //    .WithMessage("Name must be unique")
            //    .When(person => !string.IsNullOrEmpty(person.Name));

            //RuleFor(p => p.Address).SetValidator(new AddressValidator());
        }

        private async Task<bool> IsUniqueAsync(string name)
        {
            await Task.Delay(300);
            return name.ToLower() != "test";
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;

namespace TechSouq.Application.Validators
{
    public class CartValidator : AbstractValidator<CartDto>
    {
        public CartValidator ()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().NotNull().WithMessage("Must Have UserID");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid Cart Status");

        }


    }
}

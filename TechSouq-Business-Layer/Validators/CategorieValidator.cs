using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;

namespace TechSouq.Application.Validators
{
    public class CategorieValidator:AbstractValidator<CategorieDto>
    {
        public CategorieValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("inValid data Name Must be Not Null Or Empty");
        }


    }
}

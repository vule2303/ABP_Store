using FluentValidation;
using Store.Admin.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.ProductAttributes
{
    public class CreateUpdateProductAttributeDtoValidator : AbstractValidator<CreateUpdateProductAttributeDto>
    {
        public CreateUpdateProductAttributeDtoValidator()
        {
            RuleFor(x => x.Label).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DataType).NotNull();
        }
    }
}
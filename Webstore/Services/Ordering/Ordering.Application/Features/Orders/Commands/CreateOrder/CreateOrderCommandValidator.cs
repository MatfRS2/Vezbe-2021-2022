using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(order => order.BuyerUsername)
                .NotEmpty().WithMessage("{Username} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Username} must not exceed 50 characters.");

            RuleFor(order => order.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress] is required.");

            RuleForEach(order => order.OrderItems)
                .Must(item => item.Price > 0).WithMessage("{Price} should be greater than zero");
        }
    }
}

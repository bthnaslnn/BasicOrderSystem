﻿
using Business.Handlers.Orders.Commands;
using FluentValidation;

namespace Business.Handlers.Orders.ValidationRules
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Piece).NotEmpty();

        }
    }
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Piece).NotEmpty();

        }
    }
}
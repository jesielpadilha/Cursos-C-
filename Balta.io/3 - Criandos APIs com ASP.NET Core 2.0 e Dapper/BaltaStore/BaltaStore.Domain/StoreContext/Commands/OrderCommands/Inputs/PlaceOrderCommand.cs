using System;
using System.Collections.Generic;
using BaltaStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;

namespace BaltaStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class PlaceOrderCommand : Notifiable, ICommand
    {
        public PlaceOrderCommand()
        {
            OrderItems = new List<OrderItemCommand>();
        }

        public Guid Customer { get; set; }
        public List<OrderItemCommand> OrderItems { get; set; }

        public bool IsValid()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasLen(Customer.ToString(), 36, "Customer", "Identificador do Cliente inválido")
                .IsGreaterThan(OrderItems.Count, 0, "OrderItems", "Nenhum item do pedido foi encontrado")
            );
            return Valid;
        }
    }
}
using System;

namespace BaltaStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class OrderItemCommand
    {
        public Guid Product { get; set; }
        public decimal Quantity { get; set; }
    }
}
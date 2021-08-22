using System;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;

namespace BaltaStore.Domain.StoreContext.Entites
{
    public class Delivery : Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Shipped;
        }

        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            //se data estimada for no passado, não entregar
            Status = EDeliveryStatus.Shipped;
        }

        public void Cancel()
        {
            //Se o status já estiver entregue, não pode cancelar
            Status = EDeliveryStatus.Canceled;
        }
    }
}
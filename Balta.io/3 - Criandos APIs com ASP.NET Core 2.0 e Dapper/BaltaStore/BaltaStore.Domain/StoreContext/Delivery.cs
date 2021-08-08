using System;

namespace BaltaStore.Domain.StoreContext
{
    public class Delivery
    {
      public DateTime CreateDate { get; set; }
      public DateTime EstimatedDeliveryDate { get; set; }
      public string Status { get; set; }
    }
}
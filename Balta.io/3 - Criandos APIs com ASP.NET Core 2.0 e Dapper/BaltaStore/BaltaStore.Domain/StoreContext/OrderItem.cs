namespace BaltaStore.Domain.StoreContext
{
    public class OrderItem
    {
      public Product Product { get; set; }
      public int Quantity { get; set; }
      public decimal Price { get; set; }
    }
}
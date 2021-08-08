namespace BaltaStore.Domain.StoreContext
{
    public class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int QuantityOnHand { get; set; }
    }
}
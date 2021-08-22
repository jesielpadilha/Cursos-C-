using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.VelueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var customer = new Customer(
                new Name("Jesiel", "Padilha"),
                new Document("323131323131"),
                new Email("jesiel@gmail.com"),
                "(11) 99998-8988"
            );

            var order = new Order(customer);
            var mouse = new Product("Mouse", "mouse hyper-x gammer", "image.png", 249.99m, 10);
            var keyboard = new Product("Keyboard", "teclado mecânico hyper-x", "image.png", 450m, 150);
            // var orderItem1 = new OrderItem(mouse, 1);
            // var orderItem2 = new OrderItem(keyboard, 1);
            // order.AddItem(orderItem1);
            // order.AddItem(orderItem2);

            //Realizar pedido
            order.Place();

            //Verifdicar se o pedido é valid
            var valid = order.Valid;

            //Simular pagamento
            order.Pay();

            //Simular envio
            order.Ship();

            //Simular cancelamento
            order.Cancel();
        }
    }
}

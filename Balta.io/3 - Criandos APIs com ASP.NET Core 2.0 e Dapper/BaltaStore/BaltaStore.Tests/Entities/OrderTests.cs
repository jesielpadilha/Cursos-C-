using System.Linq;
using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Domain.StoreContext.VelueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Customer _customer;
        private Order _order;
        private Product _mouse;
        private Product _keyboard;
        private Product _motherboard;
        private Product _processor;
        private Product _memory;
        private Product _ssd;
        private Product _powerSupply;

        public OrderTests()
        {
            _customer = new Customer(
                          new Name("Jesiel", "Padilha"),
                          new Document("323131323131"),
                          new Email("jesiel@gmail.com"),
                          "(11) 99998-8988"
                      );
            _order = new Order(_customer);

            _mouse = new Product("Mouse", "product description...", "image.png", 1000m, 10);
            _keyboard = new Product("Keyboard", "product description...", "image.png", 1000m, 10);
            _motherboard = new Product("Motherboard", "product description...", "image.png", 1000m, 10);
            _processor = new Product("Processor", "product description...", "image.png", 1000m, 10);
            _memory = new Product("Memory", "product description...", "image.png", 1000m, 10);
            _ssd = new Product("SSD", "product description...", "image.png", 1000m, 10);
            _powerSupply = new Product("PowerSupply", "product description...", "image.png", 450m, 10);
        }
        // Consigo criar um novo pedido
        [TestMethod]
        public void ShouldCreateOrerWhenValid()
        {
            Assert.AreEqual(true, _order.Valid);
        }

        //Ao criar o pedido, o status deve ser "created"
        [TestMethod]
        public void StatusShouldBeCreatedWhenOrderCreated()
        {
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
        }

        //Ao add um novo item, a quantidade deve mudar
        [TestMethod]
        public void ShouldReturnTwoWhenAddedTwoItems()
        {
            _order.AddItem(_mouse, 5);
            _order.AddItem(_keyboard, 5);
            Assert.AreEqual(2, _order.Items.Count);
        }

        //Ao add um novo item, deve subtrair a quantidade do produto 
        [TestMethod]
        public void ShouldReturnFiveWhenAddedPurchasedFiveItem()
        {
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(_mouse.QuantityOnHand, 5);
        }

        //Ao confirmar o pedido, deve gerar um número
        [TestMethod]
        public void ShouldReturnANumberWhenOrerPlaced()
        {
            _order.Place();
            Assert.AreNotEqual("", _order.Number);
        }

        //Ao pagar um pedido, o status de ver "paid"
        [TestMethod]
        public void ShouldReturnPaidWhenOrderPaid()
        {
            _order.Pay();
            Assert.AreEqual(EOrderStatus.Paid, _order.Status);
        }

        //Dados mais 10 produtos, deve haver duas entregas
        [TestMethod]
        public void ShouldReturnTwoWhenPurchasedTenProducts()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();

            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        //Ao cancelar o pedido, o status deve ser "canceled"
        [TestMethod]
        public void StatusShouldBeCanceledWhenOrderCanceled()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        //Ao cancelar o pedido, deve cancelar as entregas
        [TestMethod]
        public void ShouldCancelShippingWhenOrderCanceled()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();
            _order.Cancel();

            _order.Deliveries.ToList().ForEach(x => Assert.AreEqual(EDeliveryStatus.Canceled, x.Status));
        }

        public void CreateCustomer()
        {
            //Verificar se o CPF já existe
            //Verificar se o Email já existe
            //Criar os VOs
            //Criar a entidade
            //Validar as entidades e VO
            //Inserir o cliente no banco
            //Enviar convite do Slack
            //Email um email de boas vindas
        }
    }
}
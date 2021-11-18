using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = Builder<CreateBoletoSubscriptionCommand>.CreateNew()
                .With(x => x.Document = "99999999999")
                .Build();

            handler.Handle(command);

            Assert.IsFalse(handler.IsValid);            
        }
    }
}
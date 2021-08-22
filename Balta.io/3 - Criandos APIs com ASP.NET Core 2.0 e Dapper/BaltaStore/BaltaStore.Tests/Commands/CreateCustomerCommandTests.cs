using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void SouldValidateWhenCommandIsValida()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Jesiel";
            command.LastName = "Padilha";
            command.Document = "75087170072";
            command.Email = "jesiel@gmail.com";
            command.Phone = "(11) 99999-9999";

            Assert.AreEqual(true, command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }
    }
}
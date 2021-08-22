using BaltaStore.Domain.StoreContext.VelueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests.ValueObjects
{
    [TestClass]
    public class NameTests
    {
        private Name validName;
        private Name invalidName;

        public NameTests()
        {
            validName = new Name("test", "test");
            invalidName = new Name("t", "tesssssssssssssssssssssssssssssssssssssssssssst");
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsNotValid()
        {
            Assert.AreEqual(false, invalidName.Valid);
            Assert.AreEqual(2, invalidName.Notifications.Count);
        }

        [TestMethod]
        public void ShouldNotReturnNotificationWhenNameIsValid()
        {
            Assert.AreEqual(true, validName.Valid);
            Assert.AreEqual(0, validName.Notifications.Count);
        }
    }
}
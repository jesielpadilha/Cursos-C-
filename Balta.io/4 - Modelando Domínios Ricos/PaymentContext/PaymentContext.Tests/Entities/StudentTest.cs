using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTest
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Payment _payment;

        public StudentTest()
        {
            var name = new Name("Bruce", "Wayne");
            var doc = new Document("12345678911", EDocumentType.CPF);
            var email = new Email("batman@dc.com");
            var address = new Address("Steet 1", "1234", "Hell Kitchen", "Gotham", "NY", "US", "12345008");
            _student = new Student(name, doc, email);
            _subscription = new Subscription(null);
            _payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", doc, address, email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            _subscription.AddPayment(_payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHadNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            _subscription.AddPayment(_payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.IsValid);
        }
    }
}
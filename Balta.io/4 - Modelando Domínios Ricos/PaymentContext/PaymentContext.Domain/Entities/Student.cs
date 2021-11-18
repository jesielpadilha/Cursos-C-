using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get => _subscriptions.ToArray(); }
        private IList<Subscription> _subscriptions;

        public void AddSubscription(Subscription subscription)
        {
            _subscriptions.ToList().ForEach(s => s.Activate());
            var hadSubscriptionActive = _subscriptions.Any(s => s.Active);

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hadSubscriptionActive, "Student.Subscriptions", "Você já possui uma assinatura ativa")
                .AreNotEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos.")
            );

            if (!hadSubscriptionActive && IsValid)
                _subscriptions.Add(subscription);
        }
    }
}
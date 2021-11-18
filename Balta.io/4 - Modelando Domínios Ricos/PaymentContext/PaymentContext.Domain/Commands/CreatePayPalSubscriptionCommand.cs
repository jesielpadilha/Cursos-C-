using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand  : Notifiable<Notification>, ICommand
    {
        //student data
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        //payment data
        public string TransactionCode { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Decimal Total { get; set; }
        public Decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public string PayerEmail { get; set; }
        public EDocumentType PayerType { get; set; }
        //address data
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
           
            AddNotifications(new Contract<CreatePayPalSubscriptionCommand>()
               .Requires()
               .IsNotNullOrEmpty(FirstName, "FirstName", "Nome obrigatório")
               .IsNotNullOrEmpty(LastName, "LastName", "Sobrenome obrigatório")
            );
            if (IsValid)
            {
                AddNotifications(new Contract<CreatePayPalSubscriptionCommand>()
                   .IsLowerThan(3, FirstName.Length, "FirstName", "Nome deve conter pelo menos 3 caracteres")
                   .IsLowerThan(3, LastName.Length, "LastName", "Sobrenome deve conter pelo menos 3 caracteres")
                   .IsGreaterThan(40, FirstName.Length, "FirstName", "Nome deve conter até 40 caracteres")
                   .IsGreaterThan(40, LastName.Length, "LastName", "Nome deve conter até 40 caracteres")
                );
            }
        }
    }
}
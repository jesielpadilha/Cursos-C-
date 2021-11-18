using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "Name.FirstName", "Nome obrigatório")
                .IsNotNullOrEmpty(LastName, "Name.LastName", "Sobrenome obrigatório")
            );
            if (IsValid)
            {
                AddNotifications(new Contract<Name>()
                   .IsLowerThan(3, FirstName.Length, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                    .IsLowerThan(3, LastName.Length, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
                    .IsGreaterThan(40, FirstName.Length, "Name.FirstName", "Nome deve conter até 40 caracteres")
                    .IsGreaterThan(40, LastName.Length, "Name.LastName", "Nome deve conter até 40 caracteres")
                );
            }
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
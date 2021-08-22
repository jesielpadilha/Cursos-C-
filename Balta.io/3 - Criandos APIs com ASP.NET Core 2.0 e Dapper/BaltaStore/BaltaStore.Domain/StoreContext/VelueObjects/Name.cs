using FluentValidator;
using FluentValidator.Validation;

namespace BaltaStore.Domain.StoreContext.VelueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 40, "FirstName", "O nome deve conter no máximo 40 caracteres")
                .HasMinLen(LastName, 3, "LasName", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "LastName", "O sobrenome deve conter no máximo 40 caracteres")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
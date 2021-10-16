using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            //if (string.IsNullOrEmpty(FirstName))
            //    AddNotification("Name.FirstName", "Nome inválido.");

            //if (string.IsNullOrEmpty(LastName))
            //    AddNotification("Name.LastName", "Sobrenome inválido.");

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres.")
                .IsLowerOrEqualsThan(FirstName, 30, "Name.FirstName", "Nome deve conter no máximo 30 caracteres.")
                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres.")
                .IsLowerOrEqualsThan(LastName, 30, "Name.LastName", "Sobrenome deve conter no máximo 30 caracteres."));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

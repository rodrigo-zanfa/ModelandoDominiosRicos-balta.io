using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Address>()
                .Requires()
                .IsGreaterOrEqualsThan(Street, 5, "Address.Street", "Rua deve conter pelo menos 5 caracteres.")
                .IsLowerOrEqualsThan(Street, 50, "Address.Street", "Rua deve conter no máximo 50 caracteres.")
                .IsGreaterOrEqualsThan(Number, 1, "Address.Number", "Número deve conter pelo menos 1 caracter.")
                .IsLowerOrEqualsThan(Number, 5, "Address.Number", "Número deve conter no máximo 5 caracteres.")
                .IsGreaterOrEqualsThan(Neighborhood, 5, "Address.Neighborhood", "Bairro deve conter pelo menos 5 caracteres.")
                .IsLowerOrEqualsThan(Neighborhood, 40, "Address.Neighborhood", "Bairro deve conter no máximo 40 caracteres.")
                .IsGreaterOrEqualsThan(City, 5, "Address.City", "Cidade deve conter pelo menos 5 caracteres.")
                .IsLowerOrEqualsThan(City, 40, "Address.City", "Cidade deve conter no máximo 40 caracteres.")
                .IsGreaterOrEqualsThan(State, 2, "Address.State", "Estado deve conter 2 caracteres.")
                .IsLowerOrEqualsThan(State, 2, "Address.State", "Estado deve conter 2 caracteres.")
                .IsGreaterOrEqualsThan(Country, 3, "Address.Country", "País deve conter pelo menos 3 caracteres.")
                .IsLowerOrEqualsThan(Country, 40, "Address.Country", "País deve conter no máximo 40 caracteres.")
                .IsGreaterOrEqualsThan(ZipCode, 8, "Address.ZipCode", "CEP deve conter 8 caracteres.")
                .IsLowerOrEqualsThan(ZipCode, 8, "Address.ZipCode", "CEP deve conter 8 caracteres."));
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}

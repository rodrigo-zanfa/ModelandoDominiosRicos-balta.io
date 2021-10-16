using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Commands
{
    public class CreateCreditCardSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        #region Atributos do Student.Name
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion

        #region Atributos do Student.Document
        public string Document { get; set; }
        //public EDocumentType DocumentType { get; set; }
        #endregion

        #region Atributos do Student.Email
        public string Email { get; set; }
        #endregion

        #region Atributos do Student.Subscriptions.Payments
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }

        #region Atributos do Student.Subscriptions.Payments.Document
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        #endregion

        #region Atributos do Student.Subscriptions.Payments.Address
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        #endregion

        #region Atributos do Student.Subscriptions.Payments.Email
        public string PayerEmail { get; set; }
        #endregion
        #endregion

        public void Validate()
        {
            AddNotifications(new Contract<CreateCreditCardSubscriptionCommand>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres.")
                .IsLowerOrEqualsThan(FirstName, 30, "Name.FirstName", "Nome deve conter no máximo 30 caracteres.")
                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres.")
                .IsLowerOrEqualsThan(LastName, 30, "Name.LastName", "Sobrenome deve conter no máximo 30 caracteres."));
        }
    }
}

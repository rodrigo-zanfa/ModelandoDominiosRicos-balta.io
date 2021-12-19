using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand();

            #region Atributos do Student.Name
            command.FirstName = "Rodrigo";
            command.LastName = "Zanferrari";
            #endregion

            #region Atributos do Student.Document
            command.Document = "99999999999";  // documento que já exista no FakeStudentRepository
            //command.DocumentType = EDocumentType.CPF;
            #endregion

            #region Atributos do Student.Email
            command.Email = "hello2@world.com";  // e-mail que não exista no FakeStudentRepository
            #endregion

            #region Atributos do Student.Subscriptions.Payments
            command.BarCode = "123456789123456789123456789123456789123456789";
            command.BoletoNumber = "123456789";
            command.PaymentNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddDays(5);
            command.Total = 200;
            command.TotalPaid = 200;
            command.Payer = "RZ2 Fotografia";

            #region Atributos do Student.Subscriptions.Payments.Document
            command.PayerDocument = "32516143800";
            command.PayerDocumentType = EDocumentType.CPF;
            #endregion

            #region Atributos do Student.Subscriptions.Payments.Address
            command.Street = "Rua 1";
            command.Number = "2";
            command.Neighborhood = "Vila Augusta";
            command.City = "Guarulhos";
            command.State = "SP";
            command.Country = "Brasil";
            command.ZipCode = "07023022";
            #endregion

            #region Atributos do Student.Subscriptions.Payments.Email
            command.PayerEmail = "rodrigo.zanferrari@gmail.com";
            #endregion
            #endregion

            handler.Handle(command);

            Assert.AreEqual(handler.IsValid, false);
        }
    }
}

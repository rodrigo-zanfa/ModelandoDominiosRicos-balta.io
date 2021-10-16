using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var document = new Document("12345", EDocumentType.CNPJ);

            Assert.IsTrue(!document.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var document = new Document("34110468000150", EDocumentType.CNPJ);

            Assert.IsTrue(document.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var document = new Document("12345", EDocumentType.CPF);

            Assert.IsTrue(!document.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("32516143800")]
        [DataRow("33713557802")]
        [DataRow("12345678901")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            var document = new Document(cpf, EDocumentType.CPF);

            Assert.IsTrue(document.IsValid);
        }
    }
}

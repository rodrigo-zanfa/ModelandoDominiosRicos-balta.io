using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();

            for (int i = 1; i <= 10; i++)
            {
                _students.Add(new Student(
                    new Name($"Nome{i}", $"Sobrenome{i}"),
                    new Document($"1111111111{i}", EDocumentType.CPF),
                    new Email($"nome{i}@world.com")));
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudent("32516143800");

            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(student, null);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudent("11111111111");

            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(student, null);
        }
    }
}

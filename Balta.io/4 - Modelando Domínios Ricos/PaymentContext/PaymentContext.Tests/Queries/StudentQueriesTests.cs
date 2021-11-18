using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;
using FizzWare.NBuilder;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var students = Builder<Student>.CreateListOfSize(4)
                .All()
                .WithFactory(x => new Student(
                    new Name("firstname", "lastname"),
                    new Document("", EDocumentType.CPF),
                    new Email("")
                ))
                .Build();

            var exp = StudentQueries.GetStudentInfo("12345678911");
            var student = students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNull(student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var students = Builder<Student>.CreateListOfSize(4)
                .All()
                .WithFactory(x => new Student(
                    new Name("firstname", "lastname"),
                    new Document("", EDocumentType.CPF),
                    new Email("")
                ))
                .TheFirst(1)
                .WithFactory(x => new Student(
                    new Name("firstname", "lastname"),
                    new Document("12345678911", EDocumentType.CPF),
                    new Email("")
                ))
                .Build();

            var exp = StudentQueries.GetStudentInfo("12345678911");
            var student = students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNotNull(student);
        }
    }
}
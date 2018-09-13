using System;
using System.Collections.Generic;
using Xunit;

namespace CTable.Tests
{
    public class ConsoleTableTests
    {
        [Fact]
        public void Should_Test_List_To_Console_Table()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person
                {
                },
                new Person
                {
                    FirstName = "Baris",
                    MiddleName = "Savas",
                    LastName = "Ceviz",
                    CreatedOn = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow,
                    Age = 21
                }
            };

            //Act
            var actual = persons.ToStringTable(
                p => p.FirstName,
                p => p.MiddleName,
                p => p.LastName,
                p => p.CreatedOn,
                p => p.LastModifiedOn,
                p => p.Age
            );

            Console.WriteLine(actual);
        }

        [Fact]
        public void Should_Test_Empty_List_To_Console_Table()
        {
            //Arrange
            var persons = new List<Person>();

            //Act
            var table = persons.ToStringTable(
                p => p.FirstName,
                p => p.MiddleName,
                p => p.LastName,
                p => p.CreatedOn,
                p => p.LastModifiedOn,
                p => p.Age
            );

            //Assert
            Console.WriteLine(table);
        }

        private class Person
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime CreatedOn { get; set; } = DateTime.Now;
            public DateTime? LastModifiedOn { get; set; }
            public int Age { get; set; }
        }
    }
}
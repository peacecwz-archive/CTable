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
            var users = new List<Person>
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

            var actual = users.ToStringTable(
                u => u.FirstName,
                u => u.MiddleName,
                u => u.LastName,
                u => u.CreatedOn,
                u => u.LastModifiedOn,
                u => u.Age
            );

            Console.WriteLine(actual);

            var guids = new List<Guid>
            {
                Guid.NewGuid(), 
                Guid.NewGuid()
            };

            var guidTable = guids.ToStringTable(new[] { "UserId" }, g => g);
            Console.WriteLine(guidTable);
        }

        [Fact]
        public void Should_Test_Empty_List_To_Console_Table()
        {
            var users = new List<Person>();

            var table = users.ToStringTable(
                u => u.FirstName,
                u=>u.MiddleName,
                u => u.LastName,
                u => u.CreatedOn,
                u => u.LastModifiedOn,
                u => u.Age
            );

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
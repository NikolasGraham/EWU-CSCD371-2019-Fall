using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mailbox.Tests
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow("firstName", null)]
        [DataRow(null, "lastName")]
        public void Person_Constructor_NullNames(string firstName, string lastName)
        {
            Person person = new Person(firstName, lastName);
        }

        [TestMethod]
        public void Person_Constructor_ValidName_CreatesPersonObject()
        {
            Person person = new Person("firstName", "lastName");

            Assert.IsNotNull(person);
        }

        [TestMethod]
        [DataRow("firstName", "lastName")]
        [DataRow("firstName2", "lastName2")]
        [DataRow("firstName3", "lastName3")]
        public void Person_EqualsOperator_PeopleAreEqual(string firstName, string lastName)
        {
            Person person1 = new Person(firstName, lastName);
            Person person2 = new Person(firstName, lastName);

            bool worked = person1.Equals(person2);

            Assert.IsTrue(worked);
        }

        [TestMethod]
        [DataRow("firstName", "lastName")]
        [DataRow("firstName2", "lastName2")]
        [DataRow("firstName3", "lastName3")]
        public void Person_EqualsOperator_PeopleAreNotEqual(string firstName, string lastName)
        {
            Person person1 = new Person(firstName, lastName);
            Person person2 = new Person(firstName + "2", lastName + "2");

            bool worked = person1.Equals(person2);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("firstName", "lastName")]
        [DataRow("firstName2", "lastName2")]
        [DataRow("firstName3", "lastName3")]
        public void Person_EqualsSign_PeopleAreEqual(string firstName, string lastName)
        {
            Person person1 = new Person(firstName, lastName);
            Person person2 = person1;

            bool worked = person1 == person2;

            Assert.IsTrue(worked);
        }

        [TestMethod]
        [DataRow("firstName", "lastName")]
        [DataRow("firstName2", "lastName2")]
        [DataRow("firstName3", "lastName3")]
        public void Person_EqualsSign_PeopleAreNotEqual(string firstName, string lastName)
        {
            Person person1 = new Person(firstName, lastName);
            Person person2 = new Person(firstName + "2", lastName + "2");

            bool worked = person1 == person2;

            Assert.IsFalse(worked);
        }

        [TestMethod]
        public void Person_EqualsOperator_NullCheck()
        {
            Person person1 = new Person("firstName", "lastName");
            Object obj = null;

            bool worked = person1.Equals(obj);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("firstName", "lastName")]
        public void Person_EqualsSign_NullCheck(string firstName, string lastName)
        {
            Person person1 = new Person(firstName, lastName);

            bool worked = person1 == null;

            Assert.IsFalse(worked);
        }
    }
}

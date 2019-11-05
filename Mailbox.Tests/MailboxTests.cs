using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mailbox.Tests
{
    [TestClass]
    public class MailboxTests
    {
        [TestMethod]
        public void Mailbox_toString_CorrectValuesReturned_Sucess()
        {
            Mailbox mB = new Mailbox(Sizes.Medium, (2, 2), new Person("first", "last"));

            Assert.AreEqual("first last owns mailbox 2,2 with size 'Medium'", mB.ToString());
        }

        [TestMethod]
        public void Mailbox_toString_ChangingOwnershipOfMailbox_Sucess()
        {
            Mailbox mB = new Mailbox(Sizes.Medium, (2, 2), new Person("first", "last"));
            mB.Owner = (new Person("newFirst", "newLast"));

            Assert.AreEqual("newFirst newLast owns mailbox 2,2 with size 'Medium'", mB.ToString());
        }

        [TestMethod]
        [DataRow(Sizes.Default)]
        [DataRow(Sizes.Small)]
        [DataRow(Sizes.Medium)]
        [DataRow(Sizes.Large)]
        public void Mailbox_toString_CreatingMailBoxWithAllNonPremiumOptions_Sucess(Sizes sizes)
        {
            string mailSize = sizes + "";
            if(sizes == Sizes.Default) { mailSize = ""; }
            Mailbox mB = new Mailbox(sizes, (5,6), new Person("John", "Doe"));

            Assert.AreEqual($"John Doe owns mailbox 5,6 with size '{mailSize}'", mB.ToString());
        }

        [TestMethod]
        [DataRow(Sizes.SmallPremium)]
        [DataRow(Sizes.MediumPremium)]
        [DataRow(Sizes.LargePremium)]
        public void Mailbox_toString_CreatingMailBoxWithAllValidPremiumOptions_Sucess(Sizes sizes)
        {
            Mailbox mB = new Mailbox(sizes, (5, 6), new Person("John", "Doe"));

            Assert.AreEqual($"John Doe owns mailbox 5,6 with size '{sizes}'", mB.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Mailbox_toString_CreatingMailBoxWithPremiumOption_Exception()
        {
            Mailbox mB = new Mailbox(Sizes.Premium, (5, 6), new Person("John", "Doe"));
        }
    }
}

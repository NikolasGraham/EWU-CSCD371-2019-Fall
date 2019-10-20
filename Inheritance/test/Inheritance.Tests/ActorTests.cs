using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Inheritance.Tests
{
    [TestClass]
    public class ActorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Actor_NullActor_ThrowsException()
        {
            ActorExtension.Speak(null);
        }

        [TestMethod]
        public void Actor_UnknownActor()
        {
            Actor actor = new Actor();

            Assert.AreEqual("Unknown Actor Detected", actor.Speak());
        }

        [TestMethod]
        public void Penny_SpeaksUsingExtension()
        {
            Actor penny = new Penny();

            Assert.AreEqual("HOLY CRAP ON A CRACKER!", penny.Speak());
        }

        [TestMethod]
        public void Sheldon_SpeaksUsingExtension()
        {
            Actor sheldon = new Sheldon();

            Assert.AreEqual("You can’t make a half sandwich. " +
            "If it’s not half of a whole sandwich it’s just a small sandwich", sheldon.Speak());
        }

        [TestMethod]
        public void Raj_SpeaksUsingExtensionWithoutWomen()
        {
            Actor raj = new Raj() { WomenArePresent = false };

            Assert.AreEqual("My religion teaches that if we suffer in this life " +
            "we are rewarded in the next.Three months at the North Pole with Sheldon " +
            "and I’m reborn as a well-hung billionaire with wings.", raj.Speak());
        }

        [TestMethod]
        public void Raj_SpeaksUsingExtensionWithWomen()
        {
            Actor raj = new Raj() { WomenArePresent = true };

            Assert.AreEqual("mmm....*whispers*", raj.Speak());
        }
    }
}
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

            Assert.Equals("Unknown Actor Detected", actor.Speak());
        }

        [TestMethod]
        public void Penny_SpeaksUsingExtension()
        {

        }
    }
}
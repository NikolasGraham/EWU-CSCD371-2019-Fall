using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    public static class ActorExtension
    {

        public static string Speak(this Actor actor) =>
            actor switch
            {
            Penny p => p.getSpeak(),
            Sheldon s => s.getSpeak(),
            Raj { WomenArePresent: true } r => r.getSpeakWithWomen(),
            Raj { WomenArePresent: false } r => r.getSpeakWithoutWomen(),
            Actor a => "Unknown Actor Detected",
            _ => throw new ArgumentNullException(nameof(actor))
            };
    }
}

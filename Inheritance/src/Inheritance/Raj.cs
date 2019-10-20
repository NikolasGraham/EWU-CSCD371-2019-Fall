using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    public class Raj : Actor
    {
        public bool WomenArePresent { get; set; }
        public string getSpeakWithoutWomen() => "My religion teaches that if we suffer in this life " +
            "we are rewarded in the next.Three months at the North Pole with Sheldon " +
            "and I’m reborn as a well-hung billionaire with wings.";

        public string getSpeakWithWomen() => "mmm....*whispers*";
    }
}

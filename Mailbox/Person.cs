using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Mailbox
{
    [DataContract]
    public struct Person : IEquatable<Person>
    {
        [DataMember]
        public string FirstName {get; set;}
        [DataMember]
        public string LastName { get; set; }

        public Person(string fName, string lName)
        {
            FirstName = fName ?? throw new ArgumentNullException(nameof(fName));
            LastName = lName ?? throw new ArgumentNullException(nameof(lName));
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return Equals(obj as Person?);
        }

        public bool Equals([AllowNull] Person other)
        {
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public static bool operator ==(Person a, Person b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Person a, Person b) => !(a == b);

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
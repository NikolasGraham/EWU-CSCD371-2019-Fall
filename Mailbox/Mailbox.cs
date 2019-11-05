using System;

namespace Mailbox
{
    public class Mailbox
    {
        public Sizes MailSize { get; set; }
        public (int x, int y) Location { get; set; }
        public Person Owner { get; set; }

        public Mailbox(Sizes mailSize, (int,int) location, Person owner)
        {
            if (mailSize == Sizes.Premium)
            {
                throw new ArgumentException("MailSize must have a premium size, not premium!");
            }
            MailSize = mailSize;
            Location = location;
            Owner = owner;
        }

        public override bool Equals(object obj)
        {
            if (obj is Mailbox) return this.Equals((Mailbox)obj);
            return false;
        }

        public bool Equals(Mailbox other)
        {
            return MailSize == MailSize &&
                Location == other.Location &&
                Owner == Owner;
        }
        public override string ToString()
        {
            string mailSize = $"{MailSize}";
            if(MailSize == Sizes.Default) { mailSize = ""; }
            return $"{Owner.ToString()} owns mailbox {Location.x},{Location.y} with size '{mailSize}'";
        }
    }
}

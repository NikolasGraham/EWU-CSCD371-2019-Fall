using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mailbox
{
    public class DataLoader : IDisposable
    {

        private Stream Source { get; }
        public DataLoader(Stream source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public List<Mailbox> Load()
        {
            List<Mailbox> mailboxes = new List<Mailbox>();
            Source.Position = 0;

            try
            {
                using(StreamReader reader = new StreamReader(Source, leaveOpen:true))
                {
                    string line;
                    while((line = reader.ReadLine()) != null){
                        Mailbox mailBox = JsonConvert.DeserializeObject<Mailbox>(line);
                        mailboxes.Add(mailBox);
                    }
                }
            }
            catch (JsonReaderException)
            {
                return null;
            }

            return mailboxes;
        }

        public void Save(List<Mailbox> mailboxes) //TODO
        {
            Source.Position = 0;

            using(StreamWriter writer = new StreamWriter(Source, leaveOpen:true))
            {
                foreach (Mailbox mB in mailboxes)
                {
                    string json = JsonConvert.SerializeObject(mB);
                    writer.WriteLine(json);
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Source?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DataLoader()
        {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

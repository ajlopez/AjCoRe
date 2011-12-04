namespace AjCoRe.Transactions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Transaction : IDisposable
    {
        private Session session;

        public Transaction(Session session)
        {
            this.session = session;
        }

        public Session Session { get { return this.session; } }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.session.CloseTransaction();
        }
    }
}

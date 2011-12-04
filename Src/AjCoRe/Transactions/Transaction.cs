namespace AjCoRe.Transactions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Transaction : IDisposable
    {
        private Session session;
        private IList<Operation> operations = new List<Operation>();

        public Transaction(Session session)
        {
            this.session = session;
        }

        public Session Session { get { return this.session; } }

        public void Complete()
        {
            foreach (Operation operation in this.operations)
                operation.Commit();

            this.operations.Clear();
        }

        public void Dispose()
        {
            for (int k = this.operations.Count; k-- > 0; )
                this.operations[k].Rollback();

            this.operations.Clear();

            this.session.CloseTransaction();
        }

        internal void RegisterSetPropertyValue(INode node, string name, object oldvalue, object newvalue)
        {
            this.operations.Add(new SetPropertyValueOperation(node, name, oldvalue, newvalue));
        }

        internal void RegisterCreateNode(INode parent, INode newnode)
        {
            this.operations.Add(new CreateNodeOperation(parent, newnode));
        }

        internal void RegisterRemoveNode(INode parent, INode node)
        {
            this.operations.Add(new RemoveNodeOperation(parent, node));
        }
    }
}

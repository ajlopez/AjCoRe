using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjCoRe.Transactions;

namespace AjCoRe
{
    public class Session
    {
        private IWorkspace workspace;
        private Transaction transaction;

        public Session(IWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public IWorkspace Workspace { get { return this.workspace; } }

        public void SetPropertyValue(INode node, string propname, object value)
        {
            if (this.transaction == null)
                throw new InvalidOperationException("No Transaction in Session");

            node.Properties.SetPropertyValue(propname, value);
        }

        public INode CreateNode(INode parent, string name, IEnumerable<Property> properties)
        {
            if (this.transaction == null)
                throw new InvalidOperationException("No Transaction in Session");

            return ((INodeCreator)this.workspace).CreateNode(parent, name, properties);
        }

        public void RemoveNode(INode node)
        {
            if (this.transaction == null)
                throw new InvalidOperationException("No Transaction in Session");

            ((IUpdatableNode)node).SetParent(null);
        }

        public Transaction OpenTransaction()
        {
            this.transaction = new Transaction(this);

            return this.transaction;
        }

        internal void CloseTransaction()
        {
            this.transaction = null;
        }
    }
}


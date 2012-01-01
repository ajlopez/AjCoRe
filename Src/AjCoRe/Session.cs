﻿using System;
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

            if (propname == null)
                throw new InvalidOperationException("No Name provided");

            if (propname.StartsWith("_"))
                throw new InvalidOperationException("Provided Name is reserverd");

            Property property = node.Properties[propname];
            object original = null;

            if (property != null)
                original = property.Value;

            node.Properties.SetPropertyValue(propname, value);
            this.transaction.RegisterSetPropertyValue(node, propname, original, value);
        }

        public INode CreateNode(INode parent, string name, IEnumerable<Property> properties)
        {
            if (this.transaction == null)
                throw new InvalidOperationException("No Transaction in Session");

            INode node = ((INodeCreator)this.workspace).CreateNode(parent, name, properties);

            this.transaction.RegisterCreateNode(parent, node);

            return node;
        }

        public void RemoveNode(INode node)
        {
            if (this.transaction == null)
                throw new InvalidOperationException("No Transaction in Session");

            INode parent = node.Parent;

            ((IUpdatableNode)node).SetParent(null);

            this.transaction.RegisterRemoveNode(parent, node);
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


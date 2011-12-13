using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCoRe.Transactions
{
    class CreateNodeOperation : Operation
    {
        private INode parent;
        private INode node;

        internal CreateNodeOperation(INode parent, INode node)
        {
            this.parent = parent;
            this.node = node;
        }

        internal override INode Node { get { return this.node; } }

        override internal void Commit()
        {
        }

        override internal void Rollback()
        {
            ((IUpdatableNode)node).SetParent(null);
        }
    }
}

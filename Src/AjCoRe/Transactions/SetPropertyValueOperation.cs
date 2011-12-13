using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCoRe.Transactions
{
    class SetPropertyValueOperation : Operation
    {
        private INode node;
        private string name;
        private object oldvalue;
        private object newvalue;

        internal SetPropertyValueOperation(INode node, string name, object oldvalue, object newvalue)
        {
            this.node = node;
            this.name = name;
            this.oldvalue = oldvalue;
            this.newvalue = newvalue;
        }

        internal override INode Node { get { return this.node; } }

        internal string PropertyName { get { return this.name; } }

        override internal void Commit()
        {
        }

        override internal void Rollback()
        {
            node.Properties.SetPropertyValue(name, oldvalue);
        }
    }
}

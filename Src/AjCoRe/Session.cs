using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCoRe
{
    public class Session
    {
        private IWorkspace workspace;

        public Session(IWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public IWorkspace Workspace { get { return this.workspace; } }

        public void SetPropertyValue(INode node, string propname, object value)
        {
            node.Properties.SetPropertyValue(propname, value);
        }

        public INode CreateNode(INode parent, string name, IEnumerable<Property> properties)
        {
            return ((INodeCreator)this.workspace).CreateNode(parent, name, properties);
        }

        public void RemoveNode(INode node)
        {
            ((IUpdatableNode)node).SetParent(null);
        }
    }
}


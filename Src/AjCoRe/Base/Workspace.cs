namespace AjCoRe.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjCoRe.Stores;

    public class Workspace : IWorkspace, INodeCreator, IStorable
    {
        private string name;
        private INode root;
        private IStore store;

        public Workspace(string name, IEnumerable<Property> rootproperties)
        {
            this.name = name;
            this.root = ((INodeCreator) this).CreateNode(null, string.Empty, rootproperties);
        }

        public Workspace(IStore store, string name)
        {
            this.name = name;
            this.store = store;
            this.root = ((INodeCreator)this).CreateNode(null, string.Empty, store.LoadProperties("/"));
        }

        public string Name { get { return this.name; } }

        public INode RootNode { get { return this.root; } }

        IStore IStorable.Store { get { return this.store; } }

        INode INodeCreator.CreateNode(INode parent, string name, IEnumerable<Property> properties)
        {
            if (parent != null && parent.ChildNodes[name] != null)
                throw new InvalidOperationException("Duplicated Child Node Name");

            return new Node(parent, name, properties, this.store);
        }
    }
}

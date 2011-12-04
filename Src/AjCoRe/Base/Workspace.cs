namespace AjCoRe.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Workspace : IWorkspace, INodeCreator
    {
        private string name;
        private INode root;

        public Workspace(string name, IEnumerable<Property> rootproperties)
        {
            this.name = name;
            this.root = ((INodeCreator) this).CreateNode(null, string.Empty, rootproperties);
        }

        public string Name { get { return this.name; } }

        public INode RootNode { get { return this.root; } }

        INode INodeCreator.CreateNode(INode parent, string name, IEnumerable<Property> properties)
        {
            return new Node(parent, name, properties);
        }
    }
}

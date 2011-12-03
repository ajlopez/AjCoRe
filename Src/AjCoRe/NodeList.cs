namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NodeList : IEnumerable<INode>
    {
        private List<INode> nodes = null;

        public NodeList()
        {
        }

        public INode this[string name]
        {
            get
            {
                return this.nodes.Where(n => n.Name == name).SingleOrDefault();
            }
        }

        public IEnumerator<INode> GetEnumerator()
        {
            if (this.nodes == null)
                this.nodes = new List<INode>();

            return this.nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (this.nodes == null)
                this.nodes = new List<INode>();

            return this.nodes.GetEnumerator();
        }

        internal void AddNode(INode node)
        {
            if (this.nodes == null)
                this.nodes = new List<INode>();

            this.nodes.Add(node);
        }
    }
}

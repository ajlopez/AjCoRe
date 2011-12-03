namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NodeList : IEnumerable<Node>
    {
        private List<Node> nodes = null;

        public NodeList()
        {
        }

        public Node this[string name]
        {
            get
            {
                return this.nodes.Where(n => n.Name == name).SingleOrDefault();
            }
        }

        public IEnumerator<Node> GetEnumerator()
        {
            if (this.nodes == null)
                this.nodes = new List<Node>();

            return this.nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (this.nodes == null)
                this.nodes = new List<Node>();

            return this.nodes.GetEnumerator();
        }

        internal void AddNode(Node node)
        {
            if (this.nodes == null)
                this.nodes = new List<Node>();

            this.nodes.Add(node);
        }
    }
}

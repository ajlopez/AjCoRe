namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Workspace
    {
        private string name;
        private Node root;

        public Workspace(string name, Node root)
        {
            this.name = name;
            this.root = root;
        }

        public string Name { get { return this.name; } }

        public Node RootNode { get { return this.root; } }
    }
}

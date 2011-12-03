namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Node
    {
        string name;
        PropertyList properties;
        Node parent;

        public string Name { get { return this.name; } }

        public Node Parent { get { return this.parent; } }

        public PropertyList Properties { get { return this.properties; } }

        public Node(IEnumerable<Property> properties)
            : this(null, string.Empty, properties)
        {
        }

        public Node(Node parent, string name, IEnumerable<Property> properties)
        {
            this.parent = parent;
            this.name = name;

            this.properties = new PropertyList(properties);
        }
    }
}


﻿namespace AjCoRe.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Node : INode
    {
        string name;
        PropertyList properties;
        NodeList nodes;
        INode parent;

        public string Name { get { return this.name; } }

        public INode Parent { get { return this.parent; } }

        public PropertyList Properties { get { return this.properties; } }

        public NodeList ChildNodes { get { return this.nodes; } }

        public Node(IEnumerable<Property> properties)
            : this(null, string.Empty, properties)
        {
        }

        public Node(Node parent, string name, IEnumerable<Property> properties)
        {
            this.parent = parent;
            this.name = name;

            this.properties = new PropertyList(properties);
            this.nodes = new NodeList();

            if (this.parent != null)
                this.parent.ChildNodes.AddNode(this);
        }
    }
}

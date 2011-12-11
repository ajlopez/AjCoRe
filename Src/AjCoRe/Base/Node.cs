﻿namespace AjCoRe.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjCoRe.Stores;

    public class Node : INode, IUpdatableNode
    {
        private string name;
        private PropertyList properties;
        private NodeList nodes;
        private INode parent;
        private IStore store;

        internal Node(IEnumerable<Property> properties)
            : this(null, string.Empty, properties, null)
        {
        }

        internal Node(INode parent, string name, IEnumerable<Property> properties, IStore store)
        {
            this.parent = parent;
            this.name = name;
            this.store = store;

            this.properties = new PropertyList(properties);

            if (store == null)
                this.nodes = new NodeList();

            if (this.parent != null)
                this.parent.ChildNodes.AddNode(this);
        }

        public string Name { get { return this.name; } }

        public INode Parent { get { return this.parent; } }

        public PropertyList Properties { get { return this.properties; } }

        public NodeList ChildNodes
        {
            get
            {
                if (this.nodes == null && this.store != null)
                {
                    this.nodes = new NodeList();
                    string path = this.Path;

                    foreach (var name in this.store.GetChildNames(this.Path))
                        new Node(this, name, this.store.LoadProperties(path + "/" + name), this.store);
                }

                return this.nodes;
            }
        }

        public string Path
        {
            get
            {
                if (this.parent == null)
                    return "/";
                if (this.parent.Parent == null)
                    return "/" + this.name;
                return this.parent.Path + "/" + this.name;
            }
        }

        void IUpdatableNode.SetParent(INode parent)
        {
            if (this.parent != null)
                this.parent.ChildNodes.RemoveNode(this);

            this.parent = parent;

            if (this.parent != null)
                this.parent.ChildNodes.AddNode(this);
        }
    }
}


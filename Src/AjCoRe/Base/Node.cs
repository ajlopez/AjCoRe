﻿namespace AjCoRe.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjCoRe.Stores;

    public class Node : INode, IUpdatableNode
    {
        private Guid? id;
        private string name;
        private PropertyList properties;
        private NodeList nodes;
        private INode parent;
        private IStore store;
        private Session session;

        internal Node(Session session, IEnumerable<Property> properties)
            : this(session, null, string.Empty, properties, null)
        {
        }

        internal Node(Session session, INode parent, string name, IEnumerable<Property> properties, IStore store)
        {
            this.session = session;

            if (parent != null && name == String.Empty)
                throw new InvalidOperationException("Child Node Name cannot be Empty");

            if (name == null)
                throw new InvalidOperationException("Node Name cannot be Null");

            this.parent = parent;
            this.name = name;
            this.store = store;

            if (properties != null)
            {
                this.properties = new PropertyList(properties.Where(p => !p.Name.StartsWith("_")));
                Property propertyId = properties.Where(p => p.Name == "_Id").SingleOrDefault();

                if (propertyId != null)
                    this.id = (Guid)propertyId.Value;
                else
                    this.id = new Guid?();
            }
            else
            {
                this.properties = new PropertyList(null);
                this.id = new Guid?();
            }

            if (store == null)
                this.nodes = new NodeList();

            if (this.parent != null)
                this.parent.ChildNodes.AddNode(this);
        }

        public Guid? Id { get { return this.id; } }

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

                    if (path == "/")
                        path = string.Empty;

                    foreach (var name in this.store.GetChildNames(this.Path))
                        new Node(this.session, this, name, this.store.LoadProperties(path + "/" + name), this.store);
                }

                return this.nodes;
            }
        }

        public Session Session { get { return this.session; } }

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

        // TODO apply DRY this implementation is repeated
        public object this[string name]
        {
            get
            {
                Property property = this.properties[name];

                if (property == null)
                    return null;

                return this.properties[name].Value;
            }
            set
            {
                if (this.session != null)
                    this.session.SetPropertyValue(this, name, value);
                else
                {
                    Session.CurrentTransaction.Session.SetPropertyValue(this, name, value);
                }
            }
        }
    }
}


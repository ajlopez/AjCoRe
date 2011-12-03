namespace AjCoRe.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class DirectoryNode : INode
    {
        private string name;
        private DirectoryInfo info;
        private INode parent;
        private PropertyList properties;

        public DirectoryNode(DirectoryInfo info)
            : this(null, string.Empty, info)
        {
        }

        public DirectoryNode(DirectoryNode parent, string name, DirectoryInfo info)
        {
            this.parent = parent;
            this.name = name;
            this.info = info;

            this.properties = new PropertyList(new List<Property>()
            {
                new Property("Name", info.Name),
                new Property("FullName", info.FullName),
                new Property("Extension", info.Extension),
                new Property("CreationTime", info.CreationTime),
                new Property("CreationTimeUtc", info.CreationTimeUtc),
                new Property("LastAccessTime", info.LastAccessTime),
                new Property("LastAccessTimeUtc", info.LastAccessTimeUtc),
                new Property("LastWriteTime", info.LastWriteTime),
                new Property("LastWriteTimeUtc", info.LastWriteTimeUtc)
            });
        }

        public string Name { get { return this.name; } }

        public INode Parent { get { return this.parent; } }

        public PropertyList Properties { get { return this.properties; } }

        public NodeList ChildNodes
        {
            get
            {
                NodeList nodes = new NodeList();

                foreach (var di in this.info.GetDirectories())
                    nodes.AddNode(new DirectoryNode(this, di.Name, di));

                foreach (var fi in this.info.GetFiles())
                    nodes.AddNode(new FileNode(this, fi.Name, fi));

                return nodes;
            }
        }
    }
}


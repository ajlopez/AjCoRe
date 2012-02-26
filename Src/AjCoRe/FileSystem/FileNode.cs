namespace AjCoRe.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class FileNode : INode
    {
        private static NodeList emptyChildNodes = new NodeList();

        private string name;
        private FileInfo info;
        private INode parent;
        private PropertyList properties;

        public FileNode(DirectoryNode parent, string name, FileInfo info)
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

        public Guid? Id { get { return null; } }

        public string Name { get { return this.name; } }

        public INode Parent { get { return this.parent; } }

        public PropertyList Properties { get { return this.properties; } }

        public NodeList ChildNodes { get { return emptyChildNodes; } }

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

        public Session Session { get { throw new NotSupportedException(); } }

        public object this[string name]
        {
            get
            {
                return this.properties[name].Value;
            }
            set
            {
                this.Session.SetPropertyValue(this, name, value);
            }
        }
    }
}


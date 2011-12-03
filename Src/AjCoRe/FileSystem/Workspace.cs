namespace AjCoRe.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class Workspace : IWorkspace
    {
        private string name;
        private INode root;

        public Workspace(string name, string directoryName)
        {
            this.name = name;
            this.root = new DirectoryNode(new DirectoryInfo(directoryName));
        }

        public string Name { get { return this.name; } }

        public INode RootNode { get { return this.root; } }
    }
}

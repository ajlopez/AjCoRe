namespace AjCoRe.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Workspace : IWorkspace
    {
        private string name;
        private INode root;

        public Workspace(string name, INode root)
        {
            this.name = name;
            this.root = root;
        }

        public string Name { get { return this.name; } }

        public INode RootNode { get { return this.root; } }
    }
}

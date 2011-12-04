namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class WorkspaceRegistry
    {
        private IList<IWorkspace> workspaces = new List<IWorkspace>();

        public IEnumerable<IWorkspace> Workspaces { get { return this.workspaces; } }

        public IWorkspace this[string name]
        {
            get
            {
                return this.workspaces.Where(ws => ws.Name == name).SingleOrDefault();
            }
        }

        public void RegisterWorkspace(IWorkspace workspace)
        {
            workspaces.Add(workspace);
        }
    }
}

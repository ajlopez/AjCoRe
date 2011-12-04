using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCoRe
{
    public class Session
    {
        private IWorkspace workspace;

        public Session(IWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public IWorkspace Workspace { get { return this.workspace; } }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCoRe
{
    public class SessionFactory
    {
        private WorkspaceRegistry registry;

        public SessionFactory(WorkspaceRegistry registry)
        {
            this.registry = registry;
        }

        public Session OpenSession(string wsname)
        {
            return new Session(registry[wsname]);
        }
    }
}

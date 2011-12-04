using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.Base;

namespace AjCoRe.Tests
{
    [TestClass]
    public class WorkspaceRegistryTests
    {
        [TestMethod]
        public void RegisterAndRetrieveWorkspace()
        {
            WorkspaceRegistry registry = new WorkspaceRegistry();
            INode root = new Node(null);
            IWorkspace workspace = new Workspace("ws1", root);
            registry.RegisterWorkspace(workspace);

            Assert.IsNotNull(registry.Workspaces);
            Assert.AreEqual(1, registry.Workspaces.Count());

            var ws = registry["ws1"];
            Assert.IsNotNull(ws);
            Assert.AreEqual("ws1", ws.Name);
            Assert.AreEqual(workspace, ws);

            Assert.IsNull(registry["ws2"]);
        }
    }
}

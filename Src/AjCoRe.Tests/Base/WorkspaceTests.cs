using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.Base;

namespace AjCoRe.Tests.Base
{
    [TestClass]
    public class WorkspaceTests
    {
        [TestMethod]
        public void CreateWorkspaceWithRootNode()
        {
            Node root = new Node(null);
            Workspace workspace = new Workspace("repository", root);

            Assert.AreEqual(root, workspace.RootNode);
            Assert.AreEqual("repository", workspace.Name);
        }
    }
}

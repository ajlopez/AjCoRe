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
            Workspace workspace = new Workspace("repository", null);

            Assert.IsNotNull(workspace.RootNode);
            Assert.AreEqual("repository", workspace.Name);
        }
    }
}

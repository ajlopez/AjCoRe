using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.FileSystem;

namespace AjCoRe.Tests.FileSystem
{
    [TestClass]
    public class WorkspaceTests
    {
        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void CreateWorkspace()
        {
            Workspace workspace = new Workspace("fs", "fs");

            Assert.AreEqual("fs", workspace.Name);
            Assert.IsNotNull(workspace.RootNode);
            Assert.AreEqual(string.Empty, workspace.RootNode.Name);
            Assert.IsNull(workspace.RootNode.Parent);
        }
    }
}

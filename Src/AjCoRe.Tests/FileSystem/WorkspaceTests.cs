using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.FileSystem;
using System.IO;

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

        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void RootNodeProperties()
        {
            Workspace workspace = new Workspace("fs", "fs");
            INode root = workspace.RootNode;
            DirectoryInfo info = new DirectoryInfo("fs");

            Assert.AreEqual(info.Extension, root.Properties["Extension"].Value);
            Assert.AreEqual(info.FullName, root.Properties["FullName"].Value);
            Assert.AreEqual(info.Name, root.Properties["Name"].Value);
            Assert.AreEqual(info.CreationTime, root.Properties["CreationTime"].Value);
            Assert.AreEqual(info.CreationTimeUtc, root.Properties["CreationTimeUtc"].Value);
            Assert.AreEqual(info.LastAccessTime, root.Properties["LastAccessTime"].Value);
            Assert.AreEqual(info.LastAccessTimeUtc, root.Properties["LastAccessTimeUtc"].Value);
            Assert.AreEqual(info.LastWriteTime, root.Properties["LastWriteTime"].Value);
            Assert.AreEqual(info.LastWriteTimeUtc, root.Properties["LastWriteTimeUtc"].Value);
            Assert.AreEqual("fs", workspace.Name);
            Assert.IsNotNull(workspace.RootNode);
            Assert.AreEqual(string.Empty, workspace.RootNode.Name);
            Assert.IsNull(workspace.RootNode.Parent);
        }

        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void GetFilesFromRoot()
        {
            Workspace workspace = new Workspace("fs", "fs");
            INode root = workspace.RootNode;

            Assert.IsNotNull(root.ChildNodes["TextFile1.txt"]);
            Assert.IsNotNull(root.ChildNodes["TextFile1.txt"]);
        }
    }
}

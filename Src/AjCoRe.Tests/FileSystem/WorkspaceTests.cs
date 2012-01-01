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

        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void GetFileProperties()
        {
            Workspace workspace = new Workspace("fs", "fs");
            INode root = workspace.RootNode;
            INode file = root.ChildNodes["TextFile1.txt"];
            FileInfo info = new FileInfo("fs/TextFile1.txt");

            Assert.IsNull(file.Id);
            Assert.AreEqual(info.Extension, file.Properties["Extension"].Value);
            Assert.AreEqual(info.FullName, file.Properties["FullName"].Value);
            Assert.AreEqual(info.Name, file.Properties["Name"].Value);
            Assert.AreEqual(info.CreationTime, file.Properties["CreationTime"].Value);
            Assert.AreEqual(info.CreationTimeUtc, file.Properties["CreationTimeUtc"].Value);
            Assert.AreEqual(info.LastAccessTime, file.Properties["LastAccessTime"].Value);
            Assert.AreEqual(info.LastAccessTimeUtc, file.Properties["LastAccessTimeUtc"].Value);
            Assert.AreEqual(info.LastWriteTime, file.Properties["LastWriteTime"].Value);
            Assert.AreEqual(info.LastWriteTimeUtc, file.Properties["LastWriteTimeUtc"].Value);
        }

        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void GetDirectoriesFromRoot()
        {
            Workspace workspace = new Workspace("fs", "fs");
            INode root = workspace.RootNode;

            Assert.IsNull(root.Id);
            Assert.IsNotNull(root.ChildNodes["Subfolder1"]);
            Assert.IsNotNull(root.ChildNodes["Subfolder2"]);
        }

        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void GetFilesFromSubdirectories()
        {
            Workspace workspace = new Workspace("fs", "fs");
            INode root = workspace.RootNode;
            INode subfolder1 = root.ChildNodes["Subfolder1"];
            INode subfolder2 = root.ChildNodes["Subfolder2"];

            Assert.IsNotNull(subfolder1.ChildNodes["TextFile3.txt"]);
            Assert.IsNotNull(subfolder2.ChildNodes["TextFile4.txt"]);

            Assert.IsInstanceOfType(subfolder1.ChildNodes["TextFile3.txt"], typeof(FileNode));
            Assert.IsInstanceOfType(subfolder2.ChildNodes["TextFile4.txt"], typeof(FileNode));
        }

        [TestMethod]
        [DeploymentItem("Files/FileSystem", "fs")]
        public void GetPaths()
        {
            Workspace workspace = new Workspace("fs", "fs");
            INode root = workspace.RootNode;
            INode subfolder1 = root.ChildNodes["Subfolder1"];
            INode subfolder2 = root.ChildNodes["Subfolder2"];

            Assert.AreEqual("/Subfolder1", subfolder1.Path);
            Assert.AreEqual("/Subfolder2", subfolder2.Path);
            Assert.AreEqual("/Subfolder1/TextFile3.txt", subfolder1.ChildNodes["TextFile3.txt"].Path);
            Assert.AreEqual("/Subfolder2/TextFile4.txt", subfolder2.ChildNodes["TextFile4.txt"].Path);
        }
    }
}

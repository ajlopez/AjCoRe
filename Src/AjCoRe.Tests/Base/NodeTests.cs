using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.Base;

namespace AjCoRe.Tests.Base
{
    [TestClass]
    public class NodeTests
    {
        private IWorkspace workspace;
        private Session session;

        [TestInitialize]
        public void Setup()
        {
            this.workspace = new Workspace("ws1", null);
            this.session = new Session(this.workspace);
        }

        [TestMethod]
        public void CreateNode()
        {
            INode root = this.workspace.RootNode;
            INode node = this.session.CreateNode(root, "person", new List<Property>()
            {
                new Property("Name", "Adam"),
                new Property("Age", 800)
            });

            Assert.AreEqual(root, node.Parent);
            Assert.AreEqual("person", node.Name);
            Assert.IsNotNull(node.Properties);
            Assert.IsNotNull(node.Properties.Where(p => p.Name == "Name").SingleOrDefault());
            Assert.AreEqual("Adam", node.Properties.Where(p => p.Name == "Name").SingleOrDefault().Value);
            Assert.IsNotNull(node.Properties.Where(p => p.Name == "Age").SingleOrDefault());
            Assert.AreEqual(800, node.Properties.Where(p => p.Name == "Age").SingleOrDefault().Value);
        }

        [TestMethod]
        public void GetProperty()
        {
            INode root = this.workspace.RootNode;
            INode node = this.session.CreateNode(root, "person", new List<Property>()
            {
                new Property("Name", "Adam"),
                new Property("Age", 800)
            });

            Assert.AreEqual(root, node.Parent);
            Assert.AreEqual("Adam", node.Properties["Name"].Value);
            Assert.AreEqual(800, node.Properties["Age"].Value);
            Assert.IsNull(node.Properties["Foo"]);
        }

        [TestMethod]
        public void CreateRootNode()
        {
            Workspace workspace = new Workspace("ws2", new List<Property>()
            {
                new Property("Name", "Eve"),
                new Property("Age", 600)
            });

            Assert.IsNull(workspace.RootNode.Parent);
            Assert.AreEqual(string.Empty, workspace.RootNode.Name);
        }

        [TestMethod]
        public void GetChildNodes()
        {
            INode root = this.workspace.RootNode;
            INode node1 = session.CreateNode(root, "person1", null);
            INode node2 = session.CreateNode(root, "person2", null);

            Assert.IsTrue(root.ChildNodes.Contains(node1));
            Assert.IsTrue(root.ChildNodes.Contains(node2));

            Assert.AreEqual(node1, root.ChildNodes["person1"]);
            Assert.AreEqual(node2, root.ChildNodes["person2"]);
            Assert.IsNull(root.ChildNodes["person3"]);
        }

        [TestMethod]
        public void GetPaths()
        {
            INode root = this.workspace.RootNode;
            INode node1 = session.CreateNode(root, "person1", null);
            INode node2 = session.CreateNode(root, "person2", null);

            Assert.AreEqual("/", root.Path);
            Assert.AreEqual("/person1", node1.Path);
            Assert.AreEqual("/person2", node2.Path);
        }
    }
}

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
        [TestMethod]
        public void CreateNode()
        {
            Node root = new Node(null);
            Node node = new Node(root, "person", new List<Property>()
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
            Node root = new Node(null);
            Node node = new Node(root, "person", new List<Property>()
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
            Node node = new Node(new List<Property>()
            {
                new Property("Name", "Eve"),
                new Property("Age", 600)
            });

            Assert.IsNull(node.Parent);
            Assert.AreEqual(string.Empty, node.Name);
        }

        [TestMethod]
        public void GetChildNodes()
        {
            Node root = new Node(null);
            Node node1 = new Node(root, "person1", null);
            Node node2 = new Node(root, "person2", null);

            Assert.IsTrue(root.ChildNodes.Contains(node1));
            Assert.IsTrue(root.ChildNodes.Contains(node2));

            Assert.AreEqual(node1, root.ChildNodes["person1"]);
            Assert.AreEqual(node2, root.ChildNodes["person2"]);
            Assert.IsNull(root.ChildNodes["person3"]);
        }
    }
}

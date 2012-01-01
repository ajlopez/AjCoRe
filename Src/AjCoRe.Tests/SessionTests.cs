using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.Base;

namespace AjCoRe.Tests
{
    [TestClass]
    public class SessionTests
    {
        private IWorkspace workspace;
        private WorkspaceRegistry registry;
        private SessionFactory factory;

        [TestInitialize]
        public void Setup()
        {
            this.workspace = new Workspace("ws1", null);
            this.registry = new WorkspaceRegistry();
            this.registry.RegisterWorkspace(this.workspace);
            this.factory = new SessionFactory(this.registry);
        }

        [TestMethod]
        public void CreateSession()
        {
            Session session = new Session(this.workspace);

            Assert.IsNotNull(session.Workspace);
            Assert.AreEqual(this.workspace, session.Workspace);
        }

        [TestMethod]
        public void CreateSessionFromFactory()
        {
            Session session = this.factory.OpenSession("ws1");

            Assert.IsNotNull(session.Workspace);
            Assert.AreEqual(this.workspace, session.Workspace);
        }

        [TestMethod]
        public void SetPropertyValue()
        {
            Session session = this.factory.OpenSession("ws1");
            INode node = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(node, "Name", "Adam");

                Assert.AreEqual("Adam", node.Properties["Name"].Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseWhenNameIsNullInSetPropertyValue()
        {
            Session session = this.factory.OpenSession("ws1");
            INode node = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(node, null, "Adam");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseWhenNameStartsWithUnderscoreInSetPropertyValue()
        {
            Session session = this.factory.OpenSession("ws1");
            INode node = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(node, "_Id", "Adam");
            }
        }

        [TestMethod]
        public void SetAndRemoveProperty()
        {
            Session session = this.factory.OpenSession("ws1");
            INode node = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(node, "Name", "Adam");
                session.SetPropertyValue(node, "Name", null);
                Assert.IsNull(node.Properties["Name"]);
            }
        }

        [TestMethod]
        public void CreateNode()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                INode node = session.CreateNode(root, "person1", new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

                Assert.IsTrue(root.ChildNodes.Contains(node));
                Assert.AreEqual(root, node.Parent);
                Assert.AreEqual("Adam", node.Properties["Name"].Value);
                Assert.AreEqual(800, node.Properties["Age"].Value);
            }
        }

        [TestMethod]
        public void CreateNodeAndCommit()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;
            INode node = null;

            using (var tr = session.OpenTransaction())
            {
                node = session.CreateNode(root, "person1", new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

                tr.Complete();
            }

            Assert.IsTrue(root.ChildNodes.Contains(node));
            Assert.AreEqual(root, node.Parent);
            Assert.AreEqual("Adam", node.Properties["Name"].Value);
            Assert.AreEqual(800, node.Properties["Age"].Value);
        }

        [TestMethod]
        public void CreateNodeAndRollback()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;
            INode node = null;

            using (var tr = session.OpenTransaction())
            {
                node = session.CreateNode(root, "person1", new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });
            }

            Assert.IsFalse(root.ChildNodes.Contains(node));
            Assert.IsNull(node.Parent);
        }

        [TestMethod]
        public void SetPropertiesAndCommit()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(root, "Name", "Adam");
                session.SetPropertyValue(root, "Age", 800);

                tr.Complete();
            }

            Assert.AreEqual("Adam", root.Properties["Name"].Value);
            Assert.AreEqual(800, root.Properties["Age"].Value);
        }

        [TestMethod]
        public void SetPropertiesAndRollback()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(root, "Name", "Adam");
                session.SetPropertyValue(root, "Age", 800);
            }

            Assert.IsNull(root.Properties["Name"]);
            Assert.IsNull(root.Properties["Age"]);
        }

        [TestMethod]
        public void SetPropertiesCommitResetAndRollback()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(root, "Name", "Adam");
                session.SetPropertyValue(root, "Age", 800);
                tr.Complete();
            }

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(root, "Name", "Eve");
                session.SetPropertyValue(root, "Age", 600);
            }

            Assert.AreEqual("Adam", root.Properties["Name"].Value);
            Assert.AreEqual(800, root.Properties["Age"].Value);
        }

        [TestMethod]
        public void CreateAndRemoveNode()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                INode node = session.CreateNode(root, "person1", new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

                session.RemoveNode(node);

                Assert.IsFalse(root.ChildNodes.Contains(node));
                Assert.AreNotEqual(root, node.Parent);
            }
        }

        [TestMethod]
        public void CreateRemoveNodeAndCommit()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;
            INode node = null;

            using (var tr = session.OpenTransaction())
            {
                node = session.CreateNode(root, "person1", new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

                session.RemoveNode(node);

                tr.Complete();
            }

            Assert.IsFalse(root.ChildNodes.Contains(node));
            Assert.AreNotEqual(root, node.Parent);
        }

        [TestMethod]
        public void CreateCommitRemoveNodeAndRollback()
        {
            Session session = this.factory.OpenSession("ws1");
            INode root = session.Workspace.RootNode;
            INode node = null;

            using (var tr = session.OpenTransaction())
            {
                node = session.CreateNode(root, "person1", new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

                tr.Complete();
            }

            using (var tr = session.OpenTransaction())
            {
                session.RemoveNode(node);
            }

            Assert.IsTrue(root.ChildNodes.Contains(node));
            Assert.AreEqual(root, node.Parent);
        }
    }
}



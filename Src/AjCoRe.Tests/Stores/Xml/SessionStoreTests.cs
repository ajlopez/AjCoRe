using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.Stores.Xml;
using AjCoRe.Base;

namespace AjCoRe.Tests.Stores.Xml
{
    [TestClass]
    public class SessionStoreTests
    {
        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs1")]
        public void SetPropertyValueAndCommit()
        {
            Store store = new Store("xmlfs1");
            Workspace workspace = new Workspace(store, "ws");
            Session session = new Session(workspace);

            INode node = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(node, "Name", "Adam");

                Assert.AreEqual("Adam", node.Properties["Name"].Value);

                tr.Complete();
            }

            PropertyList properties = store.LoadProperties("/");
            Assert.AreEqual("Adam", properties["Name"].Value);
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs2")]
        public void SetPropertyValueAndRollback()
        {
            Store store = new Store("xmlfs2");
            Workspace workspace = new Workspace(store, "ws");
            Session session = new Session(workspace);

            INode node = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                session.SetPropertyValue(node, "Name", "Adam");

                Assert.AreEqual("Adam", node.Properties["Name"].Value);
            }

            PropertyList properties = store.LoadProperties("/");
            Assert.AreEqual("Root", properties["Name"].Value);
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs3")]
        public void RemoveExistingNode()
        {
            Store store = new Store("xmlfs3");
            Workspace workspace = new Workspace(store, "ws");
            Session session = new Session(workspace);

            INode node = session.Workspace.RootNode;

            Assert.IsTrue(Directory.Exists("xmlfs3/father"));
            Assert.IsTrue(File.Exists("xmlfs3/father.xml"));

            using (var tr = session.OpenTransaction())
            {
                session.RemoveNode(node.ChildNodes["father"]);

                tr.Complete();
            }

            Assert.IsFalse(Directory.Exists("xmlfs3/father"));
            Assert.IsFalse(File.Exists("xmlfs3/father.xml"));
        }
    }
}

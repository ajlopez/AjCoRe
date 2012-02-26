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
                node["Name"] = "Adam";

                Assert.AreEqual("Adam", node["Name"]);

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
                node["Name"] = "Adam";

                Assert.AreEqual("Adam", node["Name"]);
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

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs4")]
        public void CreateNodes()
        {
            Store store = new Store("xmlfs4");
            Workspace workspace = new Workspace(store, "ws");
            Session session = new Session(workspace);

            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                for (int k = 1; k <= 10; k++)
                    session.CreateNode(root, "node" + k, new Property[] {
                        new Property("Value", k)
                    });

                tr.Complete();
            }

            for (int k = 1; k <= 10; k++)
            {
                Assert.IsTrue(File.Exists("xmlfs4/node" + k + ".xml"));
                Assert.AreEqual(k, store.LoadProperties("/node" + k)["Value"].Value);
            }
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs5")]
        public void CreateNodesAndSubnodes()
        {
            Store store = new Store("xmlfs5");
            Workspace workspace = new Workspace(store, "ws");
            Session session = new Session(workspace);

            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                for (int k = 1; k <= 10; k++)
                {
                    INode node = session.CreateNode(root, "node" + k, new Property[] {
                        new Property("Value", k)
                    });

                    for (int j = 1; j <= 10; j++)
                        session.CreateNode(node, "subnode" + j, new Property[] {
                            new Property("ParentValue", k),
                            new Property("Value", j)
                        });
                }

                tr.Complete();
            }

            for (int k = 1; k <= 10; k++)
            {
                Assert.IsTrue(File.Exists("xmlfs5/node" + k + ".xml"));
                Assert.AreEqual(k, store.LoadProperties("/node" + k)["Value"].Value);

                for (int j = 1; j <= 10; j++)
                {
                    Assert.IsTrue(File.Exists("xmlfs5/node" + k + "/subnode" + j + ".xml"));
                    var properties = store.LoadProperties("/node" + k + "/subnode" + j);

                    Assert.AreEqual(k, properties["ParentValue"].Value);
                    Assert.AreEqual(j, properties["Value"].Value);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs6")]
        public void CreateNodesAndSubnodesRemoveNodes()
        {
            Store store = new Store("xmlfs6");
            Workspace workspace = new Workspace(store, "ws");
            Session session = new Session(workspace);

            INode root = session.Workspace.RootNode;

            using (var tr = session.OpenTransaction())
            {
                for (int k = 1; k <= 10; k++)
                {
                    INode node = session.CreateNode(root, "node" + k, new Property[] {
                        new Property("Value", k)
                    });

                    for (int j = 1; j <= 10; j++)
                        session.CreateNode(node, "subnode" + j, new Property[] {
                            new Property("ParentValue", k),
                            new Property("Value", j)
                        });
                }

                tr.Complete();
            }

            using (var tr = session.OpenTransaction())
            {
                for (int k = 1; k <= 10; k++)
                    session.RemoveNode(root.ChildNodes["node" + k]);

                tr.Complete();
            }

            for (int k = 1; k <= 10; k++)
                Assert.IsFalse(File.Exists("xmlfs6/node" + k + ".xml"));
        }
    }
}

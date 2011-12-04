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
            this.workspace = new Workspace("ws1", new Node(null));
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
    }
}



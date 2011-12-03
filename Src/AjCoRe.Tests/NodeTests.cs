using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjCoRe.Tests
{
    [TestClass]
    public class NodeTests
    {
        public void CreateNode()
        {
            Node node = new Node("person", new List<Property>()
            {
                new Property("Name", "Adam"),
                new Property("Age", 800)
            });

            Assert.IsNotNull(node.Properties);
            Assert.IsNotNull(node.Properties.Where(p => p.Name == "Name").SingleOrDefault());
            Assert.AreEqual("Adam", node.Properties.Where(p => p.Name == "Name").SingleOrDefault().Value);
            Assert.IsNotNull(node.Properties.Where(p => p.Name == "Age").SingleOrDefault());
            Assert.AreEqual(800, node.Properties.Where(p => p.Name == "Age").SingleOrDefault().Value);
        }
    }
}

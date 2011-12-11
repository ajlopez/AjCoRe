using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjCoRe.Stores.Xml;
using System.IO;

namespace AjCoRe.Tests.Stores.Xml
{
    [TestClass]
    public class StoreTests
    {
        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs")]
        public void SaveSimpleProperties()
        {
            PropertyList properties = new PropertyList(new List<Property>()
                {
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

            Store store = new Store("xmlfs");

            store.SaveProperties("/adam", properties);

            Assert.IsTrue(File.Exists("xmlfs/adam.xml"));
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs")]
        public void SaveAndSimpleProperties()
        {
            PropertyList properties = new PropertyList(new List<Property>()
                {
                    new Property("Name", "Abel"),
                    new Property("Age", 400)
                });

            Store store = new Store("xmlfs");

            store.SaveProperties("/abel", properties);

            var props = store.LoadProperties("/abel");

            Assert.AreEqual(2, props.Count());
            Assert.AreEqual("Abel", props["Name"].Value);
            Assert.AreEqual(400, props["Age"].Value);
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs")]
        public void SaveAndPropertiesWithSimpleTypes()
        {
            PropertyList properties = new PropertyList(new List<Property>()
                {
                    new Property("Name", "Eve"),
                    new Property("Age", 600),
                    new Property("Male", false),
                    new Property("Hired", new DateTime(2000, 1, 1)),
                    new Property("Height", (double) 10.2),
                    new Property("Salary", (decimal) 200.50)
                });

            Store store = new Store("xmlfs");

            store.SaveProperties("/eve", properties);

            var props = store.LoadProperties("/eve");

            Assert.AreEqual(6, props.Count());
            Assert.AreEqual("Eve", props["Name"].Value);
            Assert.AreEqual(600, props["Age"].Value);
            Assert.AreEqual(false, props["Male"].Value);
            Assert.AreEqual(new DateTime(2000, 1, 1), props["Hired"].Value);
            Assert.AreEqual((double)10.2, props["Height"].Value);
            Assert.AreEqual((decimal)200.50, props["Salary"].Value);
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs")]
        public void GetChildNames()
        {
            Store store = new Store("xmlfs");
            var names = store.GetChildNames("/father");

            Assert.IsNotNull(names);
            Assert.AreEqual(3, names.Count());
            Assert.IsTrue(names.Contains("child1"));
            Assert.IsTrue(names.Contains("child2"));
            Assert.IsTrue(names.Contains("child3"));
        }

        [TestMethod]
        [DeploymentItem("Files/XmlFileSystem", "xmlfs")]
        public void GetRootProperties()
        {
            Store store = new Store("xmlfs");
            var properties = store.LoadProperties("/");

            Assert.IsNotNull(properties);
            Assert.IsNotNull(properties["Name"]);
            Assert.AreEqual("Root", properties["Name"].Value);
        }
    }
}

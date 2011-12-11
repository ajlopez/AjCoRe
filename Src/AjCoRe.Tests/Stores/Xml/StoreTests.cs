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
                    new Property("Name", "Adam"),
                    new Property("Age", 800)
                });

            Store store = new Store("xmlfs");

            store.SaveProperties("/adam", properties);

            var props = store.LoadProperties("/adam");

            Assert.AreEqual(2, props.Count());
        }
    }
}

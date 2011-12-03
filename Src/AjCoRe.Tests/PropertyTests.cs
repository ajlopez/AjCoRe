using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjCoRe.Tests
{
    [TestClass]
    public class PropertyTests
    {
        [TestMethod]
        public void CreateProperty()
        {
            Property prop = new Property("Name", "John");

            Assert.AreEqual("Name", prop.Name);
            Assert.AreEqual("John", prop.Value);
        }
    }
}

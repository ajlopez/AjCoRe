namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Node
    {
        string id;
        string name;
        IList<Property> properties;

        public IEnumerable<Property> Properties { get { return this.properties; } }

        public Node(string name, IEnumerable<Property> properties)
        {
            this.name = name;

            this.properties = new List<Property>(properties);
        }
    }
}

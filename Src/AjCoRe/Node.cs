namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Node
    {
        string name;
        PropertyList properties;

        public PropertyList Properties { get { return this.properties; } }

        public Node(string name, IEnumerable<Property> properties)
        {
            this.name = name;

            this.properties = new PropertyList(properties);
        }
    }
}

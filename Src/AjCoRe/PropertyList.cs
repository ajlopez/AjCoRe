﻿namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PropertyList : IEnumerable<Property>
    {
        private List<Property> properties;

        public PropertyList(IEnumerable<Property> properties)
        {
            this.properties = new List<Property>(properties);
        }

        public Property this[string name]
        {
            get
            {
                return properties.Where(p => p.Name == name).SingleOrDefault();
            }
        }

        public IEnumerator<Property> GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }
    }
}

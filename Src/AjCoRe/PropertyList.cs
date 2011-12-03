namespace AjCoRe
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
            if (properties == null)
                this.properties = new List<Property>();
            else
                this.properties = new List<Property>(properties);
        }

        public Property this[string name]
        {
            get
            {
                return this.properties.Where(p => p.Name == name).SingleOrDefault();
            }
        }

        public IEnumerator<Property> GetEnumerator()
        {
            return this.properties.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.properties.GetEnumerator();
        }
    }
}

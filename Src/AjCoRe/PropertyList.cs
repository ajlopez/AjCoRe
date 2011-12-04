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

        internal void SetPropertyValue(string name, object value)
        {
            Property property = this[name];

            if (property == null)
            {
                if (value == null)
                    return;

                property = new Property(name, value);
                this.properties.Add(property);
            }
            else if (value == null)
            {
                this.properties.Remove(property);
            }
            else
            {
                property.SetValue(value);
            }
        }
    }
}

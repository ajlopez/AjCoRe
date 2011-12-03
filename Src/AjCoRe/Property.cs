namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Property
    {
        private string name;
        private object value;

        public string Name { get { return this.name; } }

        public object Value { get { return this.value; } }

        public Property(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}

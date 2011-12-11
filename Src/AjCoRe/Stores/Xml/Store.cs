namespace AjCoRe.Stores.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.IO;

    public class Store
    {
        private string rootpath;

        public Store(string rootpath)
        {
            this.rootpath = rootpath;
        }

        public void SaveProperties(string path, PropertyList properties)
        {
            string filename = GetFileNameFromPath(path);
            XmlWriter writer = XmlWriter.Create(filename);
            writer.WriteStartDocument();
            writer.WriteStartElement("Properties");

            foreach (Property property in properties)
            {
                writer.WriteStartElement(property.Name);
                writer.WriteValue(property.Value);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }

        public PropertyList LoadProperties(string path)
        {
            string filename = GetFileNameFromPath(path);
            XmlReader reader = XmlReader.Create(filename);

            IList<Property> properties = new List<Property>();

            bool started = false;

            string name = null;
            object value = null;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (!started && reader.Name == "Properties")
                            started = true;
                        else
                            name = reader.Name;
                        break;

                    case XmlNodeType.Text:
                        value = reader.Value;
                        if (started && name != null)
                        {
                            properties.Add(new Property(name, value));
                            name = null;
                        }
                        break;

                    default:
                        break;
                }
            }

            return new PropertyList(properties);
        }

        private string GetFileNameFromPath(string path)
        {
            return Path.Combine(rootpath, path.Substring(1)) + ".xml";
        }
    }
}

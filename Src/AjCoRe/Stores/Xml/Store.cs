﻿namespace AjCoRe.Stores.Xml
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
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(filename, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Properties");

            foreach (Property property in properties)
            {
                writer.WriteStartElement(property.Name);

                if (property.Value is int)
                    writer.WriteAttributeString("type", "int");
                else if (property.Value is DateTime)
                    writer.WriteAttributeString("type", "datetime");
                else if (property.Value is decimal)
                    writer.WriteAttributeString("type", "decimal");
                else if (property.Value is double)
                    writer.WriteAttributeString("type", "double");
                else if (property.Value is bool)
                    writer.WriteAttributeString("type", "bool");

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
            string type = null;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (!started && reader.Name == "Properties")
                        {
                            started = true;
                            continue;
                        }

                        name = reader.Name;

                        if (reader.HasAttributes && reader.MoveToAttribute("type"))
                            type = reader.Value;

                        break;

                    case XmlNodeType.Text:
                        if (!started)
                            continue;

                        if (type == "int")
                            value = XmlConvert.ToInt32(reader.Value);
                        else if (type == "datetime")
                            value = XmlConvert.ToDateTime(reader.Value, XmlDateTimeSerializationMode.Unspecified);
                        else if (type == "bool")
                            value = XmlConvert.ToBoolean(reader.Value);
                        else if (type == "decimal")
                            value = XmlConvert.ToDecimal(reader.Value);
                        else if (type == "double")
                            value = XmlConvert.ToDouble(reader.Value);
                        else
                            value = reader.Value;

                        if (started && name != null)
                        {
                            properties.Add(new Property(name, value));
                            name = null;
                            type = null;
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

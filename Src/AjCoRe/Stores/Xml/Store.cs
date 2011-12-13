namespace AjCoRe.Stores.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.IO;

    public class Store : IStore
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

            if (!File.Exists(filename))
                return new PropertyList(null);

            IList<Property> properties = new List<Property>();

            XmlReader reader = XmlReader.Create(filename);

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

            reader.Close();

            return new PropertyList(properties);
        }

        public IEnumerable<string> GetChildNames(string path)
        {
            IList<string> names = new List<string>();
            string dirname = GetDirectoryNameFromPath(path);

            if (!Directory.Exists(dirname))
                return names;

            DirectoryInfo di = new DirectoryInfo(dirname);

            foreach (FileInfo fi in di.GetFiles("*.xml"))
                names.Add(fi.Name.Substring(0, fi.Name.Length - 4));

            return names;
        }

        public void RemoveNode(string path)
        {
            string filename = GetFileNameFromPath(path);
            string dirname = GetDirectoryNameFromPath(path);

            if (File.Exists(filename))
                File.Delete(filename);

            if (Directory.Exists(dirname))
                Directory.Delete(dirname, true);
        }

        private string GetFileNameFromPath(string path)
        {
            string subpath = path.Substring(1);
            if (subpath == String.Empty)
                subpath = "_";

            subpath += ".xml";

            return Path.Combine(rootpath, subpath);
        }

        private string GetDirectoryNameFromPath(string path)
        {
            return Path.Combine(rootpath, path.Substring(1));
        }
    }
}

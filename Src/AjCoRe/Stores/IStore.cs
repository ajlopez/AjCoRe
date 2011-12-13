namespace AjCoRe.Stores
{
    using System;
    using System.Collections.Generic;

    public interface IStore
    {
        IEnumerable<string> GetChildNames(string path);

        PropertyList LoadProperties(string path);

        void SaveProperties(string path, PropertyList properties);

        void RemoveNode(string path);
    }
}

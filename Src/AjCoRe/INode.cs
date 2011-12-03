namespace AjCoRe
{
    using System;

    public interface INode
    {
        string Name { get; }

        INode Parent { get; }

        PropertyList Properties { get; }

        NodeList ChildNodes { get; }
    }
}

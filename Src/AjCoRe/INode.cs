﻿namespace AjCoRe
{
    using System;

    public interface INode
    {
        Guid? Id { get; }

        string Name { get; }

        INode Parent { get; }

        PropertyList Properties { get; }

        NodeList ChildNodes { get; }

        string Path { get; }

        Session Session { get; }

        object this[string name] { get; set; } 
    }
}

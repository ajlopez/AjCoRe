namespace AjCoRe
{
    using System;
    using AjCoRe.Stores;

    public interface IWorkspace
    {
        string Name { get; }

        INode RootNode { get; }
    }
}

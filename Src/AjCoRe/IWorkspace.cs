namespace AjCoRe
{
    using System;

    public interface IWorkspace
    {
        string Name { get; }

        INode RootNode { get; }
    }
}

namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    interface INodeCreator
    {
        INode CreateNode(INode parent, string name, IEnumerable<Property> Properties);
    }
}

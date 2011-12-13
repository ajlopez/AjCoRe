namespace AjCoRe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCoRe.Stores;

    interface IStorable
    {
        IStore Store { get; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCoRe.Transactions
{
    abstract class Operation
    {
        internal abstract INode Node { get; }
        internal abstract void Commit();
        internal abstract void Rollback();
    }
}

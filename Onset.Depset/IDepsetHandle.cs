using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Depset
{
    public interface IDepsetHandle
    {
        string Identifier { get; }

        List<string> Dependencies { get; }
    }
}

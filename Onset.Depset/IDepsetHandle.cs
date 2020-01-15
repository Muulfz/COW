using System.Collections.Generic;

namespace Onset.Depset
{
    public interface IDepsetHandle
    {
        string Identifier { get; }

        List<string> Dependencies { get; }
    }
}

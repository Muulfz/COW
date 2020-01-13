using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Runtime.Garbage
{
    internal interface IGarbageHandler<in T>
    {
        void Handle(T obj);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Interfaces
{
    public interface IAgroGarbageCollection : IDisposable 
    {
		bool IsDisposed { get; }
		int NoReferences { get; }
        
        void AddReference();
        void RemoveReference();
    }
}

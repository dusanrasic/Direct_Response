using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.Utility
{
    public interface IViewModel<T> : INotifyPropertyChanged
    {
        T Model { get; }
    }
    
}

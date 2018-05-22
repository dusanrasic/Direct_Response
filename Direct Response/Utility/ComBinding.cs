using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Direct_Response.Utility
{
    public class ComBinding : Binding
    {
        private static RelayCommand nullCommand = new RelayCommand
            (
                delegate (object o)
                {

                },
                o => false
            );

        public ComBinding()
        {
            Initialize();
        }

        public ComBinding(string path)
            : base(path)
        {
            Initialize();
        }

        public void Initialize()
        {
            this.FallbackValue = nullCommand;
        }
    }
}

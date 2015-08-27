using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLibrary.Library.Interfaces.Hardware
{
    public interface CardReaderInterface
    {
        bool Enabled
        {
            get;
            set;
        }

        CardReaderListenerInterface Listener
        {
            get;
            set;
        }

    }
}

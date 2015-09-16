using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library.Interfaces.Hardware
{


    public interface IDisplay
    {
        UserControl Display
        {
            get;
            set;
        }
    }
}

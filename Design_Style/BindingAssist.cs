using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Style
{
    public class BindingAssist : System.Windows.Data.Binding
    {
        public BindingAssist()
        {
        }

        public BindingAssist(System.Windows.PropertyPath path) : base()
        {
            this.Path = path;
        }
    }
}

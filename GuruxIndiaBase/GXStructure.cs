using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurux_Testing
{
    /// <summary>
    /// This class is used to save arrays.
    /// </summary>
    /// <remarks>
    /// This class is added because we want to save collection inforamation (array/structure).
    /// </remarks>
    public class GXStructure : List<object>
    {
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

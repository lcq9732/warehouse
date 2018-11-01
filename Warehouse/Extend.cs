using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public static class Extend
    {
        public static DateTime ClearTime(this DateTime dtDateTime)
        {
            return Convert.ToDateTime(dtDateTime.ToShortDateString());
        }
    }
}

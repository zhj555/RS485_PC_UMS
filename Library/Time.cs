using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class Time
    {
        public Time()
        {
        }

        public Int64 NowTimeMS()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
    }
}

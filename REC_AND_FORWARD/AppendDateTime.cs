using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class AppendDateTime
    {
        public string ApendDateandTimeToFront(string String_To_Append)
        {
            return DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + String_To_Append;
        }
    }
}

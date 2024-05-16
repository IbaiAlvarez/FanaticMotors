using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FanaticMotors.Shared
{
    public class Internal
    {        
        //Gets the caller methods name
        public static string Method()
        {
            string name = (new StackTrace()).GetFrame(1).GetMethod().Name;

            name = Regex.Replace(name, "set_", string.Empty);
            name = Regex.Replace(name, "get_", string.Empty);

            return name;
        }
    }
}

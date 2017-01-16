using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XDI.Code.DbFirst.Discovery
{
    public class CodeParameter
    {
        public string Name { get; set; }
        public Type ParameterType { get; set; }
        public string TestWith { get; set; }
    }   
}

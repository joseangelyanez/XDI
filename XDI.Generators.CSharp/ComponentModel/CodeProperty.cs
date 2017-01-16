using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XDI.Code.DbFirst.Discovery
{
    public class CodeProperty
    {
        public string Name { get; set; }
        public string BoundTo { get; set; }
        public Type PropertyType { get; set; }
    }   
}

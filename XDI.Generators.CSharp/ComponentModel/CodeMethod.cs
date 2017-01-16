using System;
using System.Collections.Generic;
using XDI.CSharp.ComponentModel;

namespace XDI.Code.DbFirst.Discovery
{
    public class CodeMethod
    {
        public virtual IDictionary<string, CodeParameter> Parameters { get; set; } = new Dictionary<string, CodeParameter>();
        public IEnumerable<CodeProperty> ResultProperties { get; set; }
		public IEnumerable<CodeProperty> UnboundProperties { get; set; }
        public Type ReturnType { get; set; }
        public string Name { get; set; }
        public MethodTypes MethodType { get; set; }
        public string Comments { get; set; }
        public string BoundTo { get; set; }
    }
}
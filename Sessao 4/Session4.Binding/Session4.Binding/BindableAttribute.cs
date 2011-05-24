using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Session4.Binding
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BindableAttribute : Attribute
    {
        public bool Required { get; set; }
        public string Name { get; set; }
    }
}
using System;

namespace WebReflectorContracts.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HandlerAttribute : Attribute
    {
        public string Pattern { get; set; }

        public HandlerAttribute(string pattern)
        {
            this.Pattern = pattern;
        }
    }
}
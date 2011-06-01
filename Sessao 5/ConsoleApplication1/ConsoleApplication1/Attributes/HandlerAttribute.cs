using System;

namespace ConsoleApplication1.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    class HandlerAttribute : Attribute
    {
        public string RoutePattern { get; set; }

        public HandlerAttribute(string routePattern)
        {
            this.RoutePattern = routePattern;
        }
    }
}
using System;

namespace WebReflector.Handlers
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
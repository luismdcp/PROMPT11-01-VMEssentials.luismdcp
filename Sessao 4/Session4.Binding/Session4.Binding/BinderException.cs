using System;
using System.Reflection;

namespace Session4.Binding
{
    public class BinderException : Exception
    {
        public BinderException(string message) : base(message)
        {
            
        }
    }

    public class InvalidMemberTypeException : BinderException
    {
        public MemberInfo MemberInfo { get; private set; }

        public InvalidMemberTypeException(MemberInfo mi) : base(String.Format("Member {0} of type {1} is not primitive.", mi.Name, mi.DeclaringType.Name))
        {
            this.MemberInfo = mi;
        }
    }
}
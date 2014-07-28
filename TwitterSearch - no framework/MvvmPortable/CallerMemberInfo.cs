using System;

namespace System.Runtime.CompilerServices
{
    // Summary:
    //     Allows you to obtain the method or property name of the caller to the method.
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    sealed class CallerMemberNameAttribute : Attribute
    {
        // Summary:
        //     Initializes a new instance of the System.Runtime.CompilerServices.CallerMemberNameAttribute
        //     class.
        public CallerMemberNameAttribute()
        {
        }
    }
}
using System;

namespace SeagullDK.Inspector {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class IgnoreInInspectorAttribute : Attribute {
        
    }
}
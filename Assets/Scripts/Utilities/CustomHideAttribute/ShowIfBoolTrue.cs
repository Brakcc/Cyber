using System;
using UnityEngine;

namespace Utilities.CustomHideAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ShowIfBoolTrue : PropertyAttribute
    {
        public readonly string kapaProperty;
        public readonly bool kapaProperSecurity;
        
        public ShowIfBoolTrue(string kapaProperty)
        {
            this.kapaProperty = kapaProperty;
        }
    }
}
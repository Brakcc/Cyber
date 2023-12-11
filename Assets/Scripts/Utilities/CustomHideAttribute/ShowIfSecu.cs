using System;
using UnityEngine;

namespace Utilities.CustomHideAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ShowIfSecu : PropertyAttribute
    {
        public readonly string kapaProperSecurity;
        
        public ShowIfSecu(string kapaProperty)
        {
            kapaProperSecurity = kapaProperty;
        }
    }
}
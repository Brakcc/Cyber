using UnityEngine;
using System;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class HideIfFalse : PropertyAttribute
    {
        public string kapaProperty;
        public KapaFunctionType kapaF;

        public HideIfFalse(string kapaProperty, KapaFunctionType kapa)
        {
            this.kapaProperty = kapaProperty;
            this.kapaF = kapa;
        }
    }
}


using System;
using UnityEngine;

namespace Utilities.CustomHideAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ShowIfTrue : PropertyAttribute
    {
        public string kapaProperty;
        public int kapaF;

        public ShowIfTrue(string kapaProperty, int kapa)
        {
            this.kapaProperty = kapaProperty;
            kapaF = kapa;
        }
    }
}
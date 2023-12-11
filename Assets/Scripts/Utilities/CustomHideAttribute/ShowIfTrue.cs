using System;
using UnityEngine;

namespace Utilities.CustomHideAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ShowIfTrue : PropertyAttribute
    {
        public string kapaProperty;
        public int[] kapaFs;
        
        public ShowIfTrue(string kapaProperty, int[] kapas)
        {
            this.kapaProperty = kapaProperty;
            kapaFs = kapas;
        }
    }
}
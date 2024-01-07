using System;
using System.Runtime.Serialization;

namespace Utilities
{
    public abstract class CustomExceptions
    {
        public class CustomException : Exception
        {
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
            
            }
        }
    }
}
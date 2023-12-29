using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

//Script written by Brak

namespace Utilities
{
    public static class Fonctions
    {
        /// <summary>
        /// Return the Hit Point of the smallest Ray among a list of ray hit points and the list of their start points
        /// </summary>
        /// <param name="rayDists"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static Vector2 SmallestRayToHitPoint(Vector2[] rayDists, Vector2[] origin)
        {
            var theSmallest = 8f;
            var theSmallestRay = Vector2.zero;
            for (var i = 0; i < rayDists.Length; i++)
            {
                if (!(Vector2.Distance(rayDists[i], origin[i]) < theSmallest))
                    continue;
                
                theSmallest = Vector2.Distance(rayDists[i], origin[i]);
                theSmallestRay = rayDists[i];
            }
            return theSmallestRay;
        }

        /// <summary>
        /// Return the smallest element of a list of floats
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float SmallestElement(IEnumerable<float> floats)
        {
            var theSmallest = float.MaxValue;
            foreach (var t in floats)
            {
                if (t <= theSmallest)
                {
                    theSmallest = t;
                }
            }
            return theSmallest;
        }

        /// <summary>
        /// Return the biggest element of a list of floats
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float BiggestElement(IEnumerable<float> floats)
        {
            var theBiggest = float.MinValue;
            foreach (var t in floats)
            {
                if (t >= theBiggest)
                {
                    theBiggest = t;
                }
            }
            return theBiggest;
        }

        /// <summary>
        /// Convert a String written in the form (X.xx, Y.yy, Z.zz) as a Unity Vector3 
        /// </summary>
        /// <param name="stringVector">"(X.xx, Y.yy, Z.zz)"</param>
        /// <returns>Unity Vector3</returns>
        public static Vector3 StringToVector3(string stringVector)
        {
            if (stringVector.StartsWith("(") && stringVector.EndsWith(")"))
            {
                stringVector = stringVector.Substring(1, stringVector.Length - 1);
            }

            var sArray = stringVector.Split(',');
            for (var i = 0; i < sArray.Length; i++)
            {
                var s = sArray[i];
                s = s.Replace(")", "");
                sArray[i] = s;
            }

            var a = float.Parse(sArray[0], CultureInfo.InvariantCulture);
            var b = float.Parse(sArray[1], CultureInfo.InvariantCulture);
            var c = float.Parse(sArray[2], CultureInfo.InvariantCulture);

            Vector3 result = new(a, b, c);

            return result;
        }

        public static string NumbersToString<T>(IEnumerable<T> nbs) where T : struct
        {
            var newStr = "[";
            foreach (var i in nbs)
            {
                newStr += $"{i},";
            }
            newStr = newStr[..^1];
            newStr += "]";
            return newStr;
        }

        public static int[] StringToInts(string str)
        {
            if (str.StartsWith("[") && str.EndsWith("]"))
            {
                str = str.Substring(1, str.Length - 1);
            }

            var sArray = str.Split(",");

            for(var i = 0; i < sArray.Length; i++)
            {
                var sMono = sArray[i];
                sMono = sMono.Replace("]", "");
                sArray[i] = sMono;
            }

            var a = int.Parse(sArray[0], CultureInfo.InvariantCulture);
            var b = int.Parse(sArray[1], CultureInfo.InvariantCulture);
            var c = int.Parse(sArray[2], CultureInfo.InvariantCulture);
            var d = int.Parse(sArray[3], CultureInfo.InvariantCulture);

            return new[] { a, b, c, d };
        }

        /// <summary>
        /// Cut the data list (single string) in strings array to be used in other methodes 
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        public static string[] UnpackData(string dataString)
        {
            var dataList = dataString.Split(';');
            return dataList;
        }

        /// <summary>
        /// Check if an object is considered as in front of another for a 2D game Top Down Camera
        /// </summary>
        /// <param name="movingObjectPosition"></param>
        /// <param name="selfPosition"></param>
        /// <returns></returns>
        public static bool IsInFront(Vector2 movingObjectPosition, Vector2 selfPosition)
        {
            var inFront = selfPosition.y < movingObjectPosition.y;
            return inFront;
        }

        /// <summary>
        /// Convert a time in seconds.milliseconds to a string in format "hour,minutes,seconds,miliseconds"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FloatToHourClock(float time)
        {
            //string millisecondes;
            var hours = ((int)time / 3600).ToString();
            var minutes = (((int)time % 3600) / 60).ToString();
            var secondes = ((int)time % 60).ToString();
            //millisecondes = (((int)time * 1000) % 1000).ToString();
            /*if (time.ToString().Split(",")[1] != null)
        {
            millisecondes = time.ToString().Split(",")[1][..2] ?? time.ToString().Split(",")[1][..1] + "0";
        }
        else { millisecondes = "00"; }*/
            var clockTime = hours + "h " + minutes + "m " + secondes + "s " /*+ millisecondes + "ms"*/;
            return clockTime;
        }
    }
}

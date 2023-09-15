using UnityEngine;
using System.Globalization;
using System;

//Script written by Brak

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
        float theSmallest = 8f;
        Vector2 theSmallestRay = Vector2.zero;
        for (int i = 0; i < rayDists.Length; i++)
        {
            if (Vector2.Distance(rayDists[i], origin[i]) < theSmallest)
            {
                theSmallest = Vector2.Distance(rayDists[i], origin[i]);
                theSmallestRay = rayDists[i];
            }
        }
        return theSmallestRay;
    }

    /// <summary>
    /// Return the smallest element of a list of floats
    /// </summary>
    /// <param name="floats"></param>
    /// <returns></returns>
    public static float SmallestElement(float[] floats)
    {
        float theSmallest = float.MaxValue;
        for (int i = 0; i < floats.Length; i++)
        {
            if (floats[i] <= theSmallest)
            {
                theSmallest = floats[i];
            }
        }
        return theSmallest;
    }

    /// <summary>
    /// Return the biggest element of a list of floats
    /// </summary>
    /// <param name="floats"></param>
    /// <returns></returns>
    public static float BiggestElement(float[] floats)
    {
        float theBiggest = float.MinValue;
        for (int i = 0; i < floats.Length; i++)
        {
            if (floats[i] >= theBiggest)
            {
                theBiggest = floats[i];
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

        string[] sArray = stringVector.Split(',');
        for (int i = 0; i < sArray.Length; i++)
        {
            string s = sArray[i];
            s = s.Replace(")", "");
            sArray[i] = s;
        }

        var a = float.Parse(sArray[0], CultureInfo.InvariantCulture);
        var b = float.Parse(sArray[1], CultureInfo.InvariantCulture);
        var c = float.Parse(sArray[2], CultureInfo.InvariantCulture);

        Vector3 result = new(a, b, c);

        return result;
    }

    /// <summary>
    /// Cut the data list (single string) in strings array to be used in other methodes 
    /// </summary>
    /// <param name="dataString"></param>
    /// <returns></returns>
    public static string[] UnpackData(string dataString)
    {
        string[] dataList = dataString.Split(';');
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
        bool inFront;
        if (selfPosition.y < movingObjectPosition.y)
        {
            inFront = true;
        }
        else
        {
            inFront = false;
        }
        return inFront;
    }

    /// <summary>
    /// Convert a time in seconds.milliseconds to a string in format "hour,minutes,seconds,miliseconds"
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
   public static string FloatToHourClock(float time)
    {
        string clockTime;
        string hours;
        string minutes;
        string secondes;
        //string millisecondes;

        hours = ((int)time / 3600).ToString();
        minutes = (((int)time % 3600) / 60).ToString();
        secondes = ((int)time % 60).ToString();
        //millisecondes = (((int)time * 1000) % 1000).ToString();
        /*if (time.ToString().Split(",")[1] != null)
        {
            millisecondes = time.ToString().Split(",")[1][..2] ?? time.ToString().Split(",")[1][..1] + "0";
        }
        else { millisecondes = "00"; }*/
        clockTime = hours + "h " + minutes + "m " + secondes + "s " /*+ millisecondes + "ms"*/;
        return clockTime;
    }
}

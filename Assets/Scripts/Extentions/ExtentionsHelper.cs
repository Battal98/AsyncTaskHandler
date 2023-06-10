using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionsHelper
{
    #region Variables

    private const string DATA_FORMAT = ".json";

    #endregion

    #region Transform Jobs

    public static Transform SetZeroPosition(this Transform transform)
    {
        transform.position = Vector3.zero;
        return transform;
    }

    public static Transform SetPosX(this Transform transform, float xPos)
    {
        var pos = transform.position;
        pos.x = xPos;
        transform.position = pos;
        return transform;
    }

    public static Transform SetPosY(this Transform transform, float yPos)
    {
        var pos = transform.position;
        pos.y = yPos;
        transform.position = pos;
        return transform;
    }

    public static Transform SetPosZ(this Transform transform, float zPos)
    {
        var pos = transform.position;
        pos.z = zPos;
        transform.position = pos;
        return transform;
    }

    public static Transform SetLocalPosX(this Transform transform, float localPosX)
    {
        var pos = transform.localPosition;
        pos.x = localPosX;
        transform.localPosition = pos;
        return transform;
    }

    public static Transform SetLocalPosY(this Transform transform, float localPosY)
    {
        var pos = transform.localPosition;
        pos.y = localPosY;
        transform.localPosition = pos;
        return transform;
    }

    public static Transform SetLocalPosZ(this Transform transform, float localPosZ)
    {
        var pos = transform.localPosition;
        pos.z = localPosZ;
        transform.localPosition = pos;
        return transform;
    }

    public static Vector3 SetRandomPoint(this Transform transform, Vector3 size)
    {
        var RandomX = UnityEngine.Random.Range(-size.x / 2, size.x / 2);
        var RandomY = UnityEngine.Random.Range(-size.y / 2, size.y / 2);
        var RandomZ = UnityEngine.Random.Range(-size.z / 2, size.z / 2);
        var pos = transform.position + new Vector3(RandomX, RandomY, RandomZ);
        return pos;
    }

    public static Transform SetScaleX(this Transform transform, float ScaleXFactor)
    {
        var scale = transform.localScale;
        scale.x = ScaleXFactor;
        transform.localScale = scale;
        return transform;
    }

    #endregion

    #region List Jobs
    public static void CloseAllListElements(this List<GameObject> lists)
    {
        for (int i = 0; i < lists.Count; i++)
            lists[i].SetActive(false);
    }

    public static void OpenAllListElements(this List<GameObject> lists)
    {
        for (int i = 0; i < lists.Count; i++)
            lists[i].SetActive(true);
    }

    #endregion

    #region Enum Jobs

    /// <summary>
    /// return random Enum type 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static T RandomEnumValue<T>(this T enumType)
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }

    #endregion

    #region Func Jobs
    
    //TODO: When string const holder created,carry this const
    public static string GetMethodName<T>(this Func<T> action)
    {
        // Check if the taskFunc is an async lambda expression
        if (action.Method.Name.StartsWith("<") && action.Method.Name.Contains(">"))
        {
            // Extract the task name from the lambda expression
            var lambdaExpression = action.Method.Name.Substring(1, action.Method.Name.IndexOf(">") - 1);
            return lambdaExpression;
        }

        // Default case: use the method name as the task name
        return action.Method.Name;
    }

    #endregion
}

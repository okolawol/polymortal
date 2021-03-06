﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static T FindComponentInChildWithTag<T>(GameObject parent, string tag) where T : Component
    {
        Transform[] t = parent.GetComponentsInChildren<Transform>();
        foreach (Transform tr in t)
        {
            if (tr.CompareTag(tag))
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static bool vector3Approximately(Vector3 a, Vector3 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
    }

    public static bool vector3Approximately(Vector3 a, Vector3 b, float threshold)
    {
        return fastApproximately(a.x, b.x, threshold) && fastApproximately(a.y, b.y, threshold) && fastApproximately(a.z, b.z, threshold);
    }

    private static bool fastApproximately(float a, float b, float threshold)
    {
        if (threshold > 0f)
        {
            return Mathf.Abs(a - b) <= threshold;
        }
        else
        {
            return Mathf.Approximately(a, b);
        }
    }
}

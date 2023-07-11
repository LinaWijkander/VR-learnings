using System.Collections;
using UnityEngine;

public static class ExtensionMethods
{
    public static float Map(this float x, float in_min, float in_max, float out_min, float out_max, bool clamp = false)
    {
        if (clamp)
        {
            float normal = Mathf.InverseLerp(in_min, in_max, x);
            float bValue = Mathf.Lerp(out_min, out_max, normal);
            return bValue;
        }
        return Unity.Mathematics.math.remap(in_min, in_max, out_min, out_max, x);
    }

}

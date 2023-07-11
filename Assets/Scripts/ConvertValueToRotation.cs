using UnityEngine;

/// <summary>
/// This script takes a value and converts it to a rotation along the specified axis.
/// </summary>
public class ConvertValueToRotation : MonoBehaviour
{
    public void SetRotation(float value)
    {
       // float newValue = value.Map(in_max, in_min, 0, 360, true);
        transform.localEulerAngles = new Vector3(0, value, 0);
    }
}

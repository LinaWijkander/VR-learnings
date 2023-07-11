
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;
/// <summary>
/// This script plays an event at end of slider drag. Requires a Slider component.
/// </summary>
public class ReleaseSliderEvent : MonoBehaviour
{
    [Serializable] public class SliderEndEvent : UnityEvent { }
    public SliderEndEvent OnSliderEnd = new SliderEndEvent();
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Sliding finished");  
        OnSliderEnd.Invoke();
    }
}

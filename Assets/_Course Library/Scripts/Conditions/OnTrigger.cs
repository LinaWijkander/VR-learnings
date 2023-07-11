using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Calls functionality when a trigger occurs
/// </summary>
public class OnTrigger : MonoBehaviour
{
    public string requiredTag = string.Empty;
    [SerializeField] private bool onlyTriggerOnce;
    private bool triggered;
    [Serializable] public class TriggerEvent : UnityEvent<Collider> { }

    // When the object enters a collision
    public TriggerEvent OnEnter = new TriggerEvent();

    // When the object exits a collision
    public TriggerEvent OnExit = new TriggerEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;
        
        if (CanTrigger(other.gameObject))
        {
            // Activate Triggered behaviour on the object in trigger through ITriggerable Interface
            /*if(other.gameObject.TryGetComponent<ITriggerable>(out var triggeredObj))
            {
                if(!triggeredObj.BeenTriggered)
                    triggeredObj.Triggered();
            }*/
            // todo UI gets updated anyway ofc because they are not connected box and trigger
            
            OnEnter?.Invoke(other);
            
            if (onlyTriggerOnce)
            {
                triggered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CanTrigger(other.gameObject))
            OnExit?.Invoke(other);
    }

    private bool CanTrigger(GameObject otherGameObject)
    {
        if(requiredTag != string.Empty)
        {
            return otherGameObject.CompareTag(requiredTag);
        }
        else
        {
            return true;
        }
    }

    private void OnValidate()
    {
        if (TryGetComponent(out Collider collider))
            collider.isTrigger = true;
    }
}

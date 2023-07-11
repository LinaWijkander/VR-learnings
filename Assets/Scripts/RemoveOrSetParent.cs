using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOrSetParent : MonoBehaviour
{
    [SerializeField] private Transform parent;
    
    public void ChangeParent()
    {
        transform.parent = parent ? parent : null;
    }
}

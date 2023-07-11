using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The players only mean for ranged attack at this moment
// Limited amount in belt
public class Potion : MonoBehaviour
{
    
    [SerializeField] private LayerMask layersToTrigger;
    [SerializeField] private float impactRadius = 2;

    private void OnCollisionEnter(Collision other)
    {
        if ((layersToTrigger & (1 << other.gameObject.layer)) != 0)
        {
            // slå ut en area collider thing. for each enemy hit
            //enemy.TakeDamage(1)
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, impactRadius);
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log(hitCollider.gameObject.name + " is hit");
                //hitCollider.SendMessage("AddDamage");
            }
        }
    }
}

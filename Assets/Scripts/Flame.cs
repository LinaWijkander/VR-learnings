using System;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(ToggleParticle))]
public class Flame : MonoBehaviour
{
  [Serializable] public class IgniteEvent : UnityEvent<MonoBehaviour> { }
  
  //private ToggleParticle particleScript;
  public IgniteEvent OnIgnite = new IgniteEvent();

  /*private void Awake()
  {
    particleScript = GetComponent<ToggleParticle>();
  }*/

  [SerializeField] private LayerMask layersToTrigger;
  private void OnTriggerEnter(Collider other)
  {
    if ((layersToTrigger & (1 << other.gameObject.layer)) != 0)
    {
      OnIgnite.Invoke(this);
      //particleScript.Play();
    }
  }
}

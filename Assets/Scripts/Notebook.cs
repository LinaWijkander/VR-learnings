
using UnityEngine;

public class Notebook : MonoBehaviour
{
    private bool bookOpen;
  public void OpenNotebook(HingeJoint hj)
  {
    var motor = hj.motor;
    motor.targetVelocity = bookOpen ? -200 : 200;
    motor.force = 100;
    hj.useMotor = true;
    bookOpen = !bookOpen;
    hj.motor = motor; // Needed or the motor.targetVelocity is set (can debug it), but not actually
                      // active in the hingejoint (not changing in inspector)
  }
  
}

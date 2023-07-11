using UnityEngine;

public class ContinuousRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private Vector3 rotateVector = new (0,1f,0);
   
    void Update()
    {
        transform.Rotate(rotateVector * rotateSpeed);
    }
}

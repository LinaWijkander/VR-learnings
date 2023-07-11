using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]public class RecordPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private float roateSpeed = 20f;
    private GameObject objectInSocket;
    private bool isObjectInSocket;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


  

    // Accessed via Socket interactor's event in inspector when putting Record in socket
    public void OnRecordAdded()
    {
        objectInSocket = socket.GetOldestInteractableSelected().transform.gameObject;
        isObjectInSocket = true;
        PlayMusic();
        StartCoroutine((RotateRecord(objectInSocket)));
    }
    
    // Accessed via Socket interactor's event in inspector when removing Record from socket
    public void OnRecordRemoved()
    {
        isObjectInSocket = false;
        audioSource.Stop();
    }
    
    private void PlayMusic()
    {
        AudioClip song = objectInSocket.GetComponent<Record>().song;
        audioSource.clip = song;
        audioSource.Play();
    }

    private IEnumerator RotateRecord(GameObject record)
    {
        while (isObjectInSocket)
        {
            // Rotating socket and not the actual Record since socket interactor is preventing movement
            socket.transform.Rotate(Vector3.up, roateSpeed *Time.deltaTime);/*record.transform.rotation.x, Space.Self*/;
            yield return null;
        }
    }
}

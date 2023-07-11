using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sound when dropping sword on ground
// Sound when hitting things. Different pitch depending on velocity
// Sword activate effect
public class Sword : MonoBehaviour
{
    [SerializeField] private LayerMask layersToTrigger;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((layersToTrigger & (1 << other.gameObject.layer)) != 0)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}



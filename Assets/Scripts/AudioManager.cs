using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] birds;
    [SerializeField] private float minTimeBetweenChirps;
    [SerializeField] private float maxTimeBetweenChirps;


    void Start()
    {
        StartCoroutine(PlayRandomBirdChirp());
    }


    private IEnumerator PlayRandomBirdChirp()
    {
        yield return new WaitForSeconds(1.5f);
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenChirps, maxTimeBetweenChirps));
            int birdToChirpIndex = Random.Range(0, birds.Length-1);
            birds[birdToChirpIndex].Play();
        }
    }
}

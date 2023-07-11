using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class PlaySoundOnImpact : MonoBehaviour
{
   // add pitch variance
    private Rigidbody rb;
    private AudioSource audioSource;
    private float defaultPitch = 1.0f;

    [SerializeField] private AnimationCurve falloffCurve;
    
    [Tooltip("The volume of the sound")]
    [SerializeField] private float volume = 0.5f;
    
    [Tooltip("The sound that is played")]
    [SerializeField] private AudioClip sound = null;

    [Tooltip("The range of pitch the sound is played at (-pitch, pitch)")]
    [Range(0, 1)] public float randomPitchVariance = 0.0f;

    [SerializeField] private float minVelocity = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Play()
    {
        float randomVariance = Random.Range(-randomPitchVariance, randomPitchVariance);
        randomVariance += defaultPitch;

        audioSource.pitch = randomVariance;
        float calculatedVolume = falloffCurve.Evaluate(rb.velocity.magnitude) * volume;
        audioSource.PlayOneShot(sound, calculatedVolume);
        audioSource.pitch = defaultPitch;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.magnitude >= minVelocity)
        {
            Play();
        }
    }
    
    private void OnValidate()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
}

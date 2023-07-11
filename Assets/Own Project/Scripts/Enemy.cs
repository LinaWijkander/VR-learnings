using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private float closeEnoughDist = 1.0f;
    private int wpIndex = 0;
    private int hitPoints = 2;
    private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private LayerMask layersToTrigger;
    public static event Action<Enemy> EnemyInFountain = delegate{};
    [SerializeField] private AnimationCurve bounceCurve;
    [SerializeField] private float immunityDuration = 0.5f;
    private bool immune;
    private float bounceVariance;
    private AudioSource audioSource;
    [Tooltip("FountainReachedSound = 0, HitSound = 1, BounceSound = 2, DeathSound = 3")]
    [SerializeField] private AudioClip[] hoppbollAudio;

    [SerializeField] private Material immunityMaterial;
    private Material originalMaterial;
    private Material currentMaterial;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem splashEffect;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        originalMaterial = GetComponentInChildren<Renderer>().materials[0];
    }

    void Start()
    {
        waypoints = EnemyManager.Instance.waypoints;
        bounceVariance = Random.Range(0, 0.7f);
        currentMaterial = originalMaterial;
    }

    void Update()
    {
        FollowWaypoints();
        Bounce();
    }

    private void FollowWaypoints()
    {
        float step =  moveSpeed * Time.deltaTime;
        //waypoints[wpIndex].position = new Vector3 (waypoints[wpIndex].position.x, 0, waypoints[wpIndex].position.z);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[wpIndex].position, step);
        
        if (Vector3.Distance(transform.position, waypoints[wpIndex].position) <= closeEnoughDist)
        {
            wpIndex++;
            
            // reached destination
            if (wpIndex == waypoints.Length)
            {
                EnemyInFountain(this);
                audioSource.PlayOneShot(hoppbollAudio[0]);
                splashEffect.Play();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((layersToTrigger & (1 << other.gameObject.layer)) != 0)
        {
            if (!immune)
            {
                TakeDamage(1);
                audioSource.PlayOneShot(hoppbollAudio[1], 0.3f);
                hitEffect.transform.position = other.GetContact(0).point;
                hitEffect.Play();
                StartCoroutine(nameof(ImmunityTimer)); 
            }
        }
        // Bounce on ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            audioSource.PlayOneShot(hoppbollAudio[2], 0.25f);
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        
        if (hitPoints <= 0)
        {
            EnemyManager.Instance.EnemyKilled();
            deathEffect.Play();
            audioSource.PlayOneShot(hoppbollAudio[3], 0.4f);
            Destroy(gameObject);
        }
    }

    private IEnumerator ImmunityTimer()
    {
        currentMaterial = immunityMaterial;
        immune = true;
        yield return new WaitForSeconds(immunityDuration);
        currentMaterial = originalMaterial;
        immune = false;
    }

    private void Bounce()
    {
        transform.position = new Vector3(transform.position.x, 1 * bounceCurve.Evaluate(Time.time-bounceVariance), transform.position.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y+
        // bounceCurve.Evaluate(Time.time), transform.position.z);
    }
}

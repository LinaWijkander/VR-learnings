using UnityEngine;

/// <summary>
/// Toggles particle system
/// </summary>
//[RequireComponent(typeof(ParticleSystem))]
public class ToggleParticle : MonoBehaviour
{
    private ParticleSystem particleToToggle = null;
    private MonoBehaviour currentOwner = null;
    private bool isOn = false;

    private void Awake()
    {
        particleToToggle = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        particleToToggle.Play();
    }

    public void Stop()
    {
        particleToToggle.Stop();
    }
    
    public void Toggle()
    {
        particleToToggle.Stop();
        isOn = !isOn;
        if(isOn)
            particleToToggle.Play();
        else
            particleToToggle.Stop();
    }

    public void PlayWithExclusivity(MonoBehaviour owner)
    {
        if(currentOwner == null)
        {
            currentOwner = this;
            Play();
        }
    }

    public void StopWithExclusivity(MonoBehaviour owner)
    {
        if(currentOwner == this)
        {
            currentOwner = null;
            Stop();
        }
    }

    /*private void OnValidate()
    {
        if(currentParticleSystem)
        {
            ParticleSystem.MainModule main = currentParticleSystem.main;
            main.playOnAwake = false;
        }
    }*/
}

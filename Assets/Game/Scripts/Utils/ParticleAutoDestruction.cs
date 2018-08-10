using UnityEngine;

public class ParticleAutoDestruction : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    public bool isDestroyByTime;
    public ParticleSystem particle;
    public float time;

    private void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        particle.gameObject.SetActive(true);
        particle.Play();
        if (isDestroyByTime)
            Invoke("DestroySelf", time);
    }

    public void Play(Color startColor, float period)
    {
        var pm = particle.main;
        pm.startColor = startColor;
        particle.gameObject.SetActive(true);
        particle.Simulate(period);
        particle.Play();
        if (isDestroyByTime)
            Invoke("DestroySelf", time);
    }

    public void PlayAtPeriod(float period)
    {
        particle.gameObject.SetActive(true);
        particle.Simulate(period);
        particle.Play();
        if (isDestroyByTime)
            Invoke("DestroySelf", time);
    }

    private void Update()
    {
        if (!isDestroyByTime)
        {
            bool allStopped = true;

            foreach (ParticleSystem ps in particleSystems)
            {
                if (!ps.isStopped)
                {
                    allStopped = false;
                }
            }

            if (allStopped)
                Destroy(gameObject);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
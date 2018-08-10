using UnityEngine;

public static class ParticleExtension
{
    public static void SetStartColor(this ParticleSystem particle, Color color)
    {
        var m = particle.main;
        m.startColor = color;
    }

    public static void SetStartColor(this ParticleSystem particle, ParticleSystem.MinMaxGradient color)
    {
        var m = particle.main;
        m.startColor = color;
    }

    public static void SetLoop(this ParticleSystem particle, bool loop)
    {
        var m = particle.main;
        m.loop = loop;
    }
}
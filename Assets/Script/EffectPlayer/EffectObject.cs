using System;
using UnityEngine;
using System.Collections;

public class EffectPlayerObject : MonoBehaviour
{
    public ParticleSystem particle = null;
    public float begineTime = 0.0f;
    public float stayTime = 0.0f;

    public void EffectActive(string particleName, float begine, float stay)
    {
        PlayerParticleLoad(particleName);
        begineTime = begine;
        stayTime = stay;
    }

    public void PlayerParticleLoad(string particleName)
    {
        GameObject instance = Resources.Load("Particle/" + particleName, typeof(GameObject)) as GameObject;
        particle = GameObject.Instantiate(instance).GetComponent<ParticleSystem>();
        particle.transform.localPosition = instance.transform.localPosition;
        particle.transform.localScale = instance.transform.localScale;
    }

    public void EffectStart()
    {
        Invoke("EffectPlay", begineTime);
    }

    void EffectPlay()
    {
        particle.Play();

        Invoke("EffectStop", stayTime);
    }

    void EffectStop()
    {
        particle.Stop();
    }
}
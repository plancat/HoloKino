using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class RecoderParticle
{
    [HideInInspector]
    public ParticleSystem particle;
    // 미리 저장되어 있는것 사용
    public string particleName;

    public Vector3 particlePosition = Vector3.zero;

    // Load Particle
    public void RecoderParticleLoad()
    {
        GameObject instance = Resources.Load("Particle/" + particleName, typeof(GameObject)) as GameObject;
        particle = GameObject.Instantiate(instance).GetComponent<ParticleSystem>();
        particle.transform.localPosition = instance.transform.localPosition;
        particle.transform.localScale = instance.transform.localScale;
        particle.transform.position = particlePosition;
    }
}
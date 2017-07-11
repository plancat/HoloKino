using System;
using System.Xml;
using UnityEngine;
using System.Collections;

[Serializable]
public class RecoderQueue
{
    public RecoderParticle[] particles;
    public float beginTime = 0.0f;
    public float stayTime = 0.0f;

    public RecoderQueue(float beginTime, float stayTime, RecoderParticle[] particles)
    {
        this.beginTime = beginTime;
        this.stayTime = stayTime;
        this.particles = new RecoderParticle[particles.Length];
        for (int i = 0; i < particles.Length; i++)
        {
            this.particles[i] = new RecoderParticle();
            this.particles[i].particle = particles[i].particle;
            this.particles[i].particleName = particles[i].particleName;
            this.particles[i].particlePosition = particles[i].particlePosition;
        }
    }

    // 경로 지정 후 저장
    public void Save(XmlDocument document, XmlElement movie)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            XmlElement data = document.CreateElement("Data");
            data.SetAttribute("ParticleName", particles[i].particleName);
            data.SetAttribute("Begine", beginTime.ToString());
            data.SetAttribute("Stay", stayTime.ToString());

            data.SetAttribute("x", particles[i].particlePosition.x.ToString());
            data.SetAttribute("y", particles[i].particlePosition.y.ToString());
            data.SetAttribute("z", particles[i].particlePosition.z.ToString());

            Debug.Log(particles[i].particlePosition);

            movie.AppendChild(data);
        }
    }
}
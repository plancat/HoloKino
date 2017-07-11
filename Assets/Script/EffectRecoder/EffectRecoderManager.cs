using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EffectRecodeManager : MonoBehaviour
{
    public RecoderObject[] objects;

    [SerializeField]
    private string MovieName;
    // 모든 파티클이 끝난 후 저장
    private Queue<RecoderQueue> recoderQueue;

    // 파티클 로딩
    void Start()
    {
        recoderQueue = new Queue<RecoderQueue>();

        Debug.Log("Object : " + objects.Length);

        for (int i = 0; i < objects.Length; i++)
        {
            Debug.Log("Particle : " + objects[i].particles.Length);

            foreach (var ij in objects[i].particles)
            {
                ij.RecoderParticleLoad();
            }
        }
    }

    // 파티클 입력
    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (Input.GetKey(objects[i].key))
            {
                if (!objects[i].start)
                {
                    objects[i].start = true;
                    objects[i].beginTime = Time.time;

                    foreach (var particle in objects[i].particles)
                    {
                        particle.particle.Play();
                    }
                }
            }

            if (Input.GetKeyUp(objects[i].key))
            {
                if (objects[i].start)
                {
                    objects[i].start = false;

                    objects[i].endTime = Time.time;

                    foreach (var particle in objects[i].particles)
                    {
                        particle.particle.Stop();
                        particle.particlePosition = particle.particle.gameObject.transform.position;
                    }

                    recoderQueue.Enqueue(new RecoderQueue(objects[i].beginTime, objects[i].endTime - objects[i].beginTime, objects[i].particles));

                    objects[i].beginTime = 0.0f;
                    objects[i].endTime = 0.0f;
                }
            }
        }
    }

    // 파티클 저장
    void OnApplicationQuit()
    {
        string path = Application.streamingAssetsPath + "/" + MovieName;

        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
        if (!di.Exists)
            di.Create();

        System.Xml.XmlDocument Document = new System.Xml.XmlDocument();
        System.Xml.XmlElement MovieDataList = Document.CreateElement("MovieDataList");
        Document.AppendChild(MovieDataList);

        foreach (var it in recoderQueue)
        {
            // 경로 지정
            it.Save(Document, MovieDataList);
        }
        recoderQueue.Clear();

        path = Application.streamingAssetsPath + "/Movie/" + MovieName + ".xml";

#if !NETFX_CORE
        Document.Save(path);
#endif
    }
}
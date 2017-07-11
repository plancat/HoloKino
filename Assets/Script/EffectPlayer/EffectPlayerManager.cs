using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class EffectPlayerManager : MonoBehaviour
{
    [SerializeField]
    private string MovieName = "null";
    string path;
    string master;

    void Awake()
    {
        path = MovieName + ".xml";
        master = Application.streamingAssetsPath + "/Movie/" + path;

        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        WWW www = new WWW(master);
        
        yield return www;
        
        Interpret(www.text);
    }

    private void Interpret(string _strSource)
    {
        // 인코딩 문제 예외처리.
        // 읽은 데이터의 앞 2바이트 제거(BOM제거)
        // 혹시 오류나시면 BOM제거 부분 코드 없애고 해보시길 바랍니다~!
        
        System.Xml.XmlDocument Document = new System.Xml.XmlDocument();

#if NETFX_CORE
        StringReader stringReader = new StringReader(_strSource);

        Document.Load(stringReader);
#else
        Document.Load(master);
#endif
        System.Xml.XmlElement MovieDataList = Document["MovieDataList"];

        Queue<EffectPlayerObject> effectPlayers = new Queue<EffectPlayerObject>();

        EffectPlayerObject playerObject = null;

        foreach (System.Xml.XmlElement data in MovieDataList.ChildNodes)
        {
            string particleName = data.GetAttribute("ParticleName");
            float begine = Convert.ToSingle(data.GetAttribute("Begine"));
            float stay = Convert.ToSingle(data.GetAttribute("Stay"));

            Vector3 pos;
            pos.x = Convert.ToSingle(data.GetAttribute("x"));
            pos.y = Convert.ToSingle(data.GetAttribute("y"));
            pos.z = Convert.ToSingle(data.GetAttribute("z"));

            //Debug.LogError(particleName);
            //Debug.LogError(begine);
            //Debug.LogError(stay);
            //Debug.LogError(pos);

            playerObject = gameObject.AddComponent(typeof(EffectPlayerObject)) as EffectPlayerObject;
            playerObject.EffectActive(particleName, begine, stay);
            playerObject.particle.transform.localPosition = pos;
            
            playerObject.EffectStart();
            
            effectPlayers.Enqueue(playerObject);
            playerObject = null;
        }
    }
}
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class EffectObjectPlayerManager : MonoBehaviour
{
    public GameObject objectParents;

    Dictionary<string, GameObject> effects;

    [SerializeField]
    private string[] effectNames;
    [SerializeField]
    private GameObject[] effectGameObjects;

    private List<GameObject> allEffects;

    [SerializeField]
    private string MovieName = "null";
    string path;
    string master;

    void Awake()
    {
        path = MovieName + ".xml";
        master = Application.streamingAssetsPath + "/Movie/" + path;

        effects = new Dictionary<string, GameObject>();

        allEffects = new List<GameObject>();

        // 이펙트를 불러옴
        if (effectNames.Length != effectGameObjects.Length)
        {
            Debug.LogError("Not Equal Array Size");
        }
        else
        {
            for (int i = 0; i < effectGameObjects.Length; i++)
            {
                effects.Add(effectNames[i], effectGameObjects[i]);
            }
        }

        CreateSnowRock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateSnowRock();
        }
    }

    IEnumerator Process()
    {
        WWW www = new WWW(master);

        yield return www;

        Interpret(www.text);
    }

    void OtherProcess()
    {
        Interpret(master);
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

        foreach (System.Xml.XmlElement data in MovieDataList.ChildNodes)
        {
            string objectName = data.GetAttribute("ObjectName");

            float begine = Convert.ToSingle(data.GetAttribute("Begine"));

            Vector3 pos;
            pos.x = Convert.ToSingle(data.GetAttribute("x"));
            pos.y = Convert.ToSingle(data.GetAttribute("y"));
            pos.z = Convert.ToSingle(data.GetAttribute("z"));

            // Mono를 상속받아 생성자 호출 X
            EffectObjectPlayerObject playerObject = new GameObject().AddComponent<EffectObjectPlayerObject>();
            playerObject.DataLoad(begine, pos);
            playerObject.PlayerObjectLoad(effects[objectName], objectParents);

            allEffects.Add(playerObject.gameObject);
            allEffects.Add(playerObject._object);
        }
    }

    public void CreateSnowRock()
    {
        StopCoroutine(Process());

        if (allEffects.Count > 0)
        {
            for (int i = 0; i < allEffects.Count; i++)
            {
                Destroy(allEffects[i]);
            }
        }

        allEffects.Clear();

        // OtherProcess();

        StartCoroutine(Process());
    }
}
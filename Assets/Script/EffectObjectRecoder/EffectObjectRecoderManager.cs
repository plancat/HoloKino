using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectObjectRecoderManager : MonoBehaviour
{
    public EffectObjectObject[] objects;

    public string MovieName = "";

    public GameObject parent;

    private List<ObjectRecoderQueue> effectRecoderObjects;

    void Start()
    {
        effectRecoderObjects = new List<ObjectRecoderQueue>();
    }

    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (Input.GetKeyDown(objects[i].key))
            {
                for (int t = 0; t < objects[i]._position.Length; t++)
                {
                    objects[i].beginTime = Time.time;

                    GameObject temp = Instantiate(objects[i]._object);
                    temp.SetActive(true);

                    Vector3 backScale = temp.transform.localScale;
                    Vector3 backPos = temp.transform.localPosition;
                    Quaternion backRot = temp.transform.localRotation;

                    temp.transform.parent = parent.transform;

                    temp.transform.localScale = backScale;
                    temp.transform.localPosition = backPos;
                    temp.transform.localRotation = backRot;

                    temp.GetComponent<FracturedObject>().StartStatic = false;

                    // -----

                    Destroy(temp.gameObject, 4.0f);

                    Transform[] allChildren = temp.GetComponentsInChildren<Transform>();

                    for (int j = 0; j < allChildren.Length; j++)
                    {
                        Rigidbody rigid = allChildren[j].GetComponent<Rigidbody>();
                        if (rigid != null)
                        {
                            rigid.isKinematic = false;
                        }

                        Destroy(allChildren[j].gameObject, 4.0f);
                    }


                    effectRecoderObjects.Add(new ObjectRecoderQueue(objects[i].beginTime, objects[i].objectName, objects[i]._position[t]));

                    objects[i].beginTime = 0.0f;
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

        for (int i = 0; i < effectRecoderObjects.Count; i++)
        {
            // 경로 지정
            effectRecoderObjects[i].Save(Document, MovieDataList);
        }
        effectRecoderObjects.Clear();

        path = Application.streamingAssetsPath + "/Movie/" + MovieName + "Object.xml";

#if !NETFX_CORE
        Document.Save(path);
#endif
    }
}
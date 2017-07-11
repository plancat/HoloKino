using System;
using UnityEngine;
using System.Collections;
using System.Xml;

[Serializable]
public class ObjectRecoderQueue : MonoBehaviour
{
    public string objectName = "null";
    public float beginTime = 0.0f;
    public Vector3 position;

    public ObjectRecoderQueue(float beginTime, string objectName, Vector3 position)
    {
        this.beginTime = beginTime;
        this.objectName = objectName;
        this.position = position;
    }

    // 경로 지정 후 저장
    public void Save(XmlDocument document, XmlElement movie)
    {
        XmlElement data = document.CreateElement("Data");

        data.SetAttribute("ObjectName", objectName);
        data.SetAttribute("Begine", beginTime.ToString());

        data.SetAttribute("x", position.x.ToString());
        data.SetAttribute("y", position.y.ToString());
        data.SetAttribute("z", position.z.ToString());

        movie.AppendChild(data);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectObjectPlayerObject : MonoBehaviour
{
    public GameObject _object = null;
    public float begineTime = 0.0f;
    public Vector3 position;

    public List<Transform> allTransform;

    private bool onStart = false;

    public void DataLoad(float begine, Vector3 position)
    {
        this.begineTime = begine;
        this.position = position;
    }

    public void PlayerObjectLoad(GameObject instantiate, GameObject parent)
    {
        _object = Instantiate(instantiate);

        Vector3 backScale = _object.transform.localScale;
        Vector3 backPos = _object.transform.localPosition;
        Quaternion backRot = _object.transform.localRotation;

        _object.transform.parent = parent.transform;

        _object.transform.localScale = backScale;
        _object.transform.localPosition = backPos;
        _object.transform.localRotation = backRot;

        _object.SetActive(false);
    }

    void EffectPlay()
    {
        _object.SetActive(true);

        allTransform = new List<Transform>();

        Transform[] allChildren = _object.GetComponentsInChildren<Transform>();
        for (int j = 0; j < allChildren.Length; j++)
        {
            Rigidbody rigid = allChildren[j].GetComponent<Rigidbody>();

            if (rigid != null)
            {
                rigid.isKinematic = false;

                allTransform.Add(rigid.transform);
            }
        }

        Invoke("EffectDestroy", 4.0f);
    }

    void Update()
    {
        if(Time.time >= begineTime)
        {
            if (!onStart)
            {
                onStart = true;

                EffectPlay();
            }
        }
    }

    void EffectDestroy()
    {
        _object.SetActive(false);

        for(int i=0;i< allTransform.Count; i++)
        {
            allTransform[i].gameObject.SetActive(false);
        }
    }
}

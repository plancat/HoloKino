using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class EffectObjectObject
{
    [SerializeField]
    public KeyCode key;
    // 파티클은 미리 저장되어있는것을 쓴다.
    [SerializeField]
    public GameObject _object;
    [SerializeField]
    public string objectName;
    [SerializeField]
    public Vector3[] _position;

    [HideInInspector]
    public float beginTime = 0.0f;
}
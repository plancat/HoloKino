using System;
using UnityEngine;
using System.Collections;

// 키 지정, 파티클 지정
// 저장시 [ 시작, 지속시간(끝 - 시작) ] 저장

[Serializable]
public class RecoderObject
{
    [SerializeField]
    public KeyCode key;
    // 파티클은 미리 저장되어있는것을 쓴다.
    [SerializeField]
    public RecoderParticle[] particles;

    [HideInInspector]
    public float beginTime = 0.0f;
    [HideInInspector]
    public float endTime = 0.0f;
    [HideInInspector]
    public bool start = false;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 몬스터 HPUI가 항상 카메라를 바라보기

public class WorldCameraSet : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private Camera thecamera;

    void Start()
    {
        thecamera = FindObjectOfType<Camera>();
        canvas.worldCamera = thecamera;
    }

    private void Update()
    {
        this.transform.LookAt(thecamera.transform.position);
    }
}

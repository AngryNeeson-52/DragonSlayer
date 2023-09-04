using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� HPUI�� �׻� ī�޶� �ٶ󺸱�

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

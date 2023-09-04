using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 소품 LOD 대체 레이어 컬링

public class LayerSet : MonoBehaviour
{
    private Camera mCamera;
    [SerializeField]
    private float siuu;


    private void Start()
    {
        mCamera = FindObjectOfType<Camera>();
        float[] distances = new float[32];
        distances[12] = siuu;
        mCamera.layerCullDistances = distances;
    }
}

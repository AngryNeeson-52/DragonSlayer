using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǰ LOD ��ü ���̾� �ø�

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

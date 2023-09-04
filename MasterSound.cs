using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ü �Ҹ� ũ�� ����

public class MasterSound : MonoBehaviour
{
    public float effectVol, fieldVol;

    private SoundDic[] soundDics;

    private void Start()
    {
        SetVol();
    }

    public void SetVol()
    {
        soundDics = FindObjectsOfType<SoundDic>();

        for (int i = 0; i < soundDics.Length; i++)
        {
            soundDics[i].SetVol(effectVol, fieldVol);
        }
    }
}

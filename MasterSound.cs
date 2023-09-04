using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전체 소리 크기 관리

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 傈眉 家府 农扁 包府

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

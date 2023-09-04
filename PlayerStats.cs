using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어 스탯 적용

public class PlayerStats : Stats
{
    public float ATK;

    [SerializeField]
    private Image HPUI, MPUI;
    
    void Start()
    {
        HPUpdate();
        MPUpdate();
    }

    public void HPUpdate()
    {
        HPUI.fillAmount = CurrentHP / MaxHP;
    }
    public void MPUpdate()
    {
        MPUI.fillAmount = CurrentMP / MaxMP;
    }


}

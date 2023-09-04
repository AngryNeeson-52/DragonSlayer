using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾� ���� ����

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

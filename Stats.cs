using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾�, ������ ���� ����

public class Stats : MonoBehaviour
{
    //����
    public float MaxHP, MaxMP, CurrentHP, CurrentMP, Wallet;

    // �� ��ȭ �Լ�
    #region
    public bool MaxHPChange(float HP)    // �ִ� ü�� ��ȭ
    {
        if (MaxHP + HP > 0)
        {
            MaxHP += HP;
            return true;
        }
        else
        {
            MaxHP = 1;
            return false;
        }
    }
    public bool MaxMPChange(float MP)    // �ִ� ���� ��ȭ
    {
        if (MaxMP + MP > 0)
        {
            MaxMP += MP;
            return true;
        }
        else
        {
            MaxMP = 0;
            return false;
        }
    }
    public bool CurrentHPChange(float HP)    // ���� ü�� ��ȭ
    {
        if (CurrentHP + HP > 0)
        {
            if (CurrentHP + HP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
            else
            {
                CurrentHP += HP;
            }
            return true;
        }
        else
        {
            CurrentHP = 1;
            return false;
        }
    }
    public bool CurrentMPChange(float MP)    // ���� ���� ��ȭ
    {
        if (CurrentMP + MP >= 0)
        {
            if (CurrentMP + MP > MaxMP)
            {
                CurrentMP = MaxMP;
            }
            else
            {
                CurrentMP += MP;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool WalletChange(float coin)  // ������ ��ȭ
    {
        if (Wallet + coin >= 0)
        {
            Wallet += coin;
            return true;
        }
        else 
        {
            return false;
        }
    }
    #endregion

}

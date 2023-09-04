using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어, 몬스터의 개인 스텟

public class Stats : MonoBehaviour
{
    //스텟
    public float MaxHP, MaxMP, CurrentHP, CurrentMP, Wallet;

    // 값 변화 함수
    #region
    public bool MaxHPChange(float HP)    // 최대 체력 변화
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
    public bool MaxMPChange(float MP)    // 최대 마나 변화
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
    public bool CurrentHPChange(float HP)    // 현재 체력 변화
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
    public bool CurrentMPChange(float MP)    // 현재 마나 변화
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
    public bool WalletChange(float coin)  // 소지금 변화
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
